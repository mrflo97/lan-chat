using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	interface IConnectionHandler
	{
		event Action<string, string> PublicMessageReceived;

		event Action<ParticipantList, string, string> PrivateMessageReceived;

		void SendMessage(string receivingService, string message);
	}
}
