using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lan_chat
{
	public class ServiceInformation
	{
		public event Action ServiceDown;

		public IPAddress Host { get; set; }

		public string ServiceId { get; set; }

		private Timer recheckStateTimer;

		private ServiceDiscovery owningServiceDiscovery;

		private Config config;

		public ServiceInformation(IPAddress host, string serviceId, ServiceDiscovery owningServiceDiscovery, Config config)
		{
			this.Host = host;
			this.ServiceId = serviceId;
			this.owningServiceDiscovery = owningServiceDiscovery;
			this.config = config;
			this.recheckStateTimer = new Timer(state => this.CheckState());
			this.recheckStateTimer.Change(config.CheckServiceStateInterval, config.CheckServiceStateInterval);
		}

		public void Down()
		{
			this.ServiceDown?.Invoke();
		}

		public void CheckState()
		{
			this.owningServiceDiscovery.SendServiceRequest(new IPEndPoint(this.Host, this.config.ForeignDiscoveryPort));
		}
	}
}
