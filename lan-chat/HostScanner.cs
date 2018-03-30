using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NetTools;

namespace lan_chat
{
	class HostScanner
	{
		private CountdownEvent countdown;

		private List<IPAddress> upHosts;

		private readonly object getUpHostsLock = new object();

		private readonly object upHostsLock = new object();

		private ManualResetEvent finishedSendPing;

		public HostScanner()
		{
			this.upHosts = new List<IPAddress>();
		}

		public IEnumerable<IPAddress> GetUpHosts(IEnumerable<IPAddress> hostsToCheck, int timeout)
		{
			lock (this.getUpHostsLock)
			{
				this.finishedSendPing = new ManualResetEvent(false);
				this.countdown = new CountdownEvent(1);
				foreach (var hostToCheck in hostsToCheck)
				{
					this.countdown.AddCount();
					var ping = new Ping();
					ping.PingCompleted += this.onPingCompleted;
					ping.SendAsync(hostToCheck, 500, null);
				}
				this.finishedSendPing.Set();
				this.countdown.Signal();
				if (this.countdown.Wait(timeout))
				{
					return this.upHosts;
				}
				return null;
			}
		}

		private void onPingCompleted(object sender, PingCompletedEventArgs e)
		{
			this.finishedSendPing.WaitOne(1000);
			if (e.Reply != null && e.Reply.Status == IPStatus.Success)
			{
				this.upHosts.Add(e.Reply.Address);
			}
			this.countdown.Signal();
		}
	}
}