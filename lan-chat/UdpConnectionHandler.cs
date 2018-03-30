using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	class UdpConnectionHandler : IConnectionHandler
	{
		public event Action<string, string> PublicMessageReceived;

		public event Action<ParticipantList, string, string> PrivateMessageReceived;

		private readonly Config config;

		private readonly List<IPAddress> whiteList;

		private readonly UdpReceiver receiver;

		private readonly Dictionary<string, IPAddress> userHostMap;

		private UdpClient sender;

		public UdpConnectionHandler(Config config)
		{
			this.config = config;
			this.whiteList = new List<IPAddress>();
			this.userHostMap = new Dictionary<string, IPAddress>();
			this.receiver = new UdpReceiver(this.config, this.config.MessagePort, this.config.MessageSeparator);
			this.receiver.MessageReceived += this.onReceiverOnMessageReceived;
			this.sender = new UdpClient(AddressFamily.InterNetwork);
		}

		public void Start()
		{
			this.receiver.Start();
		}

		public void ServiceDiscovered(ServiceInformation information)
		{
			this.whiteList.Add(information.Host);
			this.userHostMap.Add(information.ServiceId, information.Host);
		}

		public void ServiceOffline(ServiceInformation information)
		{
			this.whiteList.Remove(information.Host);
			this.userHostMap.Remove(information.ServiceId);
		}

		private void onReceiverOnMessageReceived(IPEndPoint endPoint, string s)
		{
			string username;
			try
			{
				username = this.userHostMap.First(x => x.Value.ToString() == endPoint.Address.ToString()).Key;
			}
			catch (Exception)
			{
				username = "UNKNOWN";
			}
			var parts = s.Split(this.config.PartSeparator);
			if (parts.Length > 1)
			{
				var participants = new ParticipantList(parts[1]);
				if (participants.Contains(ServiceInformations.UserName))
				{
					this.PrivateMessageReceived?.Invoke(participants, username, parts[0]);
				}
			}
			else
			{
				this.PublicMessageReceived?.Invoke(username, s);
			}
		}

		public void SendMessage(string receivingService, string message)
		{
			message = Tools.FinalizeMessage(this.config, message);
			var bytes = this.config.MessageEncoding.GetBytes(message);
			var endPoint = new IPEndPoint(this.userHostMap[receivingService], this.config.MessagePort);
			this.sender.Send(bytes, bytes.Length, endPoint);
		}

		public void SendMessage(string message)
		{
			message = Tools.FinalizeMessage(this.config, message);
			var bytes = this.config.MessageEncoding.GetBytes(message);
			foreach (var host in this.userHostMap.Values)
			{
				var endPoint = new IPEndPoint(host, this.config.MessagePort);
				this.sender.Send(bytes, bytes.Length, endPoint);
			}
		}

		public void SendMessage(ParticipantList list, string message)
		{
			var msg = Tools.BuildMessage(this.config, message, list.GetCompareString());
			foreach (var participant in list.Where(part => ServiceInformations.UserName != part))
			{
				this.SendMessage(participant, msg);
			}
		}
	}
}
