using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	abstract class ACommunicator
	{
		public event Action<IPEndPoint, string, string> MessageReceived;

		public abstract void SendMessage(string message);
	}
}
