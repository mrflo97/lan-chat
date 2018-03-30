using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lan_chat
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			
			var config = new Config();

			var serviceDiscovery = new ServiceDiscovery(config);
			var connectionHandler = new UdpConnectionHandler(config);
			var mainForm = new ChatForm();
			var chatWindowManager = new ChatWindowManager(mainForm);

			connectionHandler.PublicMessageReceived += mainForm.ShowMessage;
			connectionHandler.PrivateMessageReceived += chatWindowManager.NewPrivateMessage;

			serviceDiscovery.ServiceDiscovered += connectionHandler.ServiceDiscovered;
			serviceDiscovery.ServiceDiscovered += mainForm.AddParticipant;
			serviceDiscovery.ServiceOffline += connectionHandler.ServiceOffline;
			serviceDiscovery.ServiceOffline += mainForm.RemoveParticipant;

			mainForm.SendMessage += connectionHandler.SendMessage;
			mainForm.OpenPrivateChat += chatWindowManager.OpenChat;
			mainForm.Closing += (sender, args) => chatWindowManager.CloseChatForms();

			chatWindowManager.SendMessage += connectionHandler.SendMessage;

			serviceDiscovery.Start();
			serviceDiscovery.StartIntervalScan(1000);
			connectionHandler.Start();

			Application.Run(mainForm);
		}
	}
}
