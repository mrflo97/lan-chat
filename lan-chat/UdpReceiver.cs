using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public class UdpReceiver : IDisposable
	{
		public event Action<IPEndPoint, string> MessageReceived;

		private UdpClient receiver;

		private Dictionary<string, MessageBuffer> buffer;

		private readonly string separator;

		private bool disposing;

		private readonly Config config;

		private readonly object bufferLock = new object();

		private readonly int port;

		public UdpReceiver(Config config, int port, string separator)
		{
			this.port = port;
			this.separator = separator;
			this.buffer = new Dictionary<string, MessageBuffer>();
			this.disposing = false;
			this.config = config;
		}

		public void Start()
		{
			this.receiver = new UdpClient(port);
			this.receiver.BeginReceive(this.bytesReceived, null);
		}

		private void bytesReceived(IAsyncResult ar)
		{
			try
			{
				IPEndPoint senderEndPoint = null;
				byte[] receivedBytes = this.receiver.EndReceive(ar, ref senderEndPoint);
				var receivedString = this.config.MessageEncoding.GetString(receivedBytes);
				this.receiver.BeginReceive(this.bytesReceived, null);
				this.rawMessageReceived(senderEndPoint, receivedString);
			}
			catch (ObjectDisposedException e)
			{
				if (!this.disposing)
				{
					this.Dispose();
				}
			}
		}

		private void rawMessageReceived(IPEndPoint sender, string rawMessage)
		{
			lock (this.bufferLock)
			{
				var senderString = sender.Address.ToString();
				MessageBuffer tempBuffer;
				if (this.buffer.TryGetValue(senderString, out tempBuffer))
				{
					tempBuffer.AddString(rawMessage);
				}
				else
				{
					var newBuffer = new MessageBuffer(this.separator);
					newBuffer.MessageReady += s =>
					{
						this.MessageReceived?.Invoke(sender, s);
					};
					newBuffer.AddString(rawMessage);
					this.buffer.Add(senderString, newBuffer);
				}
			}
		}

		public void Dispose()
		{
			this.disposing = true;
			this.receiver?.Client?.Dispose();
		}
	}
}
