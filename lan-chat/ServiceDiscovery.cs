using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace lan_chat
{
	public class ServiceDiscovery : IDisposable
	{
		public event Action<ServiceInformation> ServiceDiscovered;

		public event Action<ServiceInformation> ServiceOffline;

		public List<ServiceInformation> AvailableServices;

		public static ServiceDiscovery Instance { get; private set; }

		private Timer rescanTimer;

		private readonly UdpReceiver receiver;

		private readonly UdpClient sender;

		private readonly Config config;

		private readonly Dictionary<string, Timer> serviceRequestTimeoutTimers;

		private readonly object lockRequestTimeoutTimers = new object();

		public ServiceDiscovery(Config config)
		{
			this.AvailableServices = new List<ServiceInformation>();
			this.receiver = new UdpReceiver(config, config.DiscoveryPort, config.MessageSeparator);
			this.receiver.MessageReceived += this.onMessageReceived;
			this.sender = new UdpClient(AddressFamily.InterNetwork);
			this.config = config;
			this.serviceRequestTimeoutTimers = new Dictionary<string, Timer>();
		}

		public void Start()
		{
			this.receiver.Start();
		}

		public void StartIntervalScan(int intervalMillis)
		{
			this.rescanTimer?.Change(Timeout.Infinite, Timeout.Infinite);
			this.rescanTimer = this.rescanTimer ?? new Timer(state => this.Rescan());
			this.rescanTimer.Change(0, intervalMillis);
		}

		public void Rescan(bool rescanHosts = false)
		{
			var hostsToScan = ServiceInformations.GetAvailableHosts(rescanHosts);
			foreach (var hostToScan in hostsToScan)
			{
				this.SendServiceRequest(new IPEndPoint(hostToScan, this.config.ForeignDiscoveryPort));
			}
		}

		public void Dispose()
		{
			this.rescanTimer?.Dispose();
			this.receiver?.Dispose();
			((IDisposable)this.sender)?.Dispose();
		}

		public void SendServiceAnnouncement(IPEndPoint endPoint)
		{
			var msg = Tools.BuildMessage(this.config, "service-announce", ServiceInformations.UserName);
			msg = Tools.FinalizeMessage(this.config, msg);
			this.send(endPoint, msg);
		}

		public void SendServiceRequest(IPEndPoint endPoint)
		{
			var msg = Tools.BuildMessage(this.config, "service-request", ServiceInformations.UserName, this.config.DiscoveryPort.ToString());
			msg = Tools.FinalizeMessage(this.config, msg);
			this.send(endPoint, msg);
			lock (this.lockRequestTimeoutTimers)
			{
				if (!this.serviceRequestTimeoutTimers.ContainsKey(endPoint.Address.ToString()))
				{
					var timer = new Timer(obj => this.gotNoServiceRequestAnswer(endPoint));
					timer.Change(this.config.ServiceRequestTimeout, Timeout.Infinite);
					this.serviceRequestTimeoutTimers.Add(endPoint.Address.ToString(), timer);
				}
			}
		}

		public string GetUserNameToHost(IPEndPoint endPoint)
		{
			return this.AvailableServices
				.Where(info => info.Host.ToString() == endPoint.ToString())
				.Select(info => info.ServiceId)
				.First();
		}

		public void AddService(ServiceInformation info)
		{
			this.ServiceDiscovered?.Invoke(info);
			this.AvailableServices.Add(info);
		}

		public void RemoveService(ServiceInformation info)
		{
			this.ServiceOffline?.Invoke(info);
			this.AvailableServices.Remove(info);
		}

		private void gotNoServiceRequestAnswer(IPEndPoint endPoint)
		{
			ServiceInformation info = this.AvailableServices.FirstOrDefault(x => x.Host.ToString() == endPoint.Address.ToString());
			info?.Down();
		}

		private void serviceDiscovered(IPEndPoint endPoint, string serviceId)
		{
			var info = new ServiceInformation(endPoint.Address, serviceId, this, this.config);
			info.ServiceDown += () => this.RemoveService(info);
			this.AddService(info);
		}

		private void onMessageReceived(IPEndPoint ipEndPoint, string msg)
		{
			var parts = msg.Split(this.config.PartSeparator);
			var msgType = parts[0];
			if (msgType == "service-announce" || msgType == "service-request")
			{
				lock (this.lockRequestTimeoutTimers)
				{
					Timer timer;
					if (this.serviceRequestTimeoutTimers.TryGetValue(ipEndPoint.Address.ToString(), out timer))
					{
						timer.Change(Timeout.Infinite, Timeout.Infinite);
						timer.Dispose();
						this.serviceRequestTimeoutTimers.Remove(ipEndPoint.Address.ToString());
					}
				}
				if (!this.AvailableServices.Any(info => info.Host.ToString() == ipEndPoint.Address.ToString() && parts[1] == info.ServiceId))
				{
					this.serviceDiscovered(ipEndPoint, parts[1]);
				}
				if (msgType == "service-request")
				{
					this.SendServiceAnnouncement(new IPEndPoint(ipEndPoint.Address, int.Parse(parts[2])));
				}
			}
		}

		private void send(IPEndPoint endPoint, string msg)
		{
			var bytes = this.config.MessageEncoding.GetBytes(msg);
			this.sender.Send(bytes, bytes.Length, endPoint);
		}
	}
}