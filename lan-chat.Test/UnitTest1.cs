using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using lan_chat;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lan_chat.Test
{
	[TestClass]
	public class UnitTest1
	{
		private Config mainConfig;

		[TestInitialize]
		public void TestInit()
		{
			this.mainConfig = new Config();
		}

		[TestMethod]
		public void TestMessageBuffer()
		{
			var resultList = new List<string> { "asf", "fbfdsa1", "123" };
			var buffer = new MessageBuffer("!|");
			var resultCount = 0;
			buffer.MessageReady += s =>
			{
				Assert.AreEqual(resultList[resultCount++], s);
			};
			buffer.AddString("asf!");
			buffer.AddString("|fbfdsa");
			buffer.AddString("1!|123");
		}

		[TestMethod]
		public void TestUdpReceiver()
		{
			var port = 1234;
			using (var receiver = new UdpReceiver(this.mainConfig, port, "!"))
			{
				using (var sender = new UdpClient(AddressFamily.InterNetwork))
				{
					var receivedEvent = new ManualResetEvent(false);
					var msg = "123";
					receiver.MessageReceived += (point, s) =>
					{
						Assert.AreEqual(msg, s);
						receivedEvent.Set();
					};
					receiver.Start();
					var msgBytes = Encoding.UTF8.GetBytes(msg + "!");
					sender.Send(msgBytes, msgBytes.Length, new IPEndPoint(IPAddress.Parse("127.0.0.1"), port));
					Assert.IsTrue(receivedEvent.WaitOne(2000));
				}
			}
		}

		[TestMethod]
		public void TestServiceDiscovery()
		{
			var port = 3254;
			var config = this.mainConfig.Copy();
			config.DiscoveryPort = port;
			using (var discovery = new ServiceDiscovery(config))
			{
				using (var sender = new UdpClient(AddressFamily.InterNetwork))
				{
					var received = new ManualResetEvent(false);
					discovery.AvailableServices.CollectionChanged += (o, args) =>
					{
						if (args.Action == NotifyCollectionChangedAction.Add)
						{
							var info = (ServiceInformation)args.NewItems[0];
							Assert.AreEqual(info.Host.ToString(), "127.0.0.1");
							Assert.AreEqual(info.ServiceId, "flo");
							received.Set();
						}
					};
					discovery.Start();
					var msg = Tools.BuildMessage(config, "service-announce", "flo");
					msg = Tools.FinalizeMessage(config, msg);
					var msgBytes = this.mainConfig.MessageEncoding.GetBytes(msg);
					var receiver = new IPEndPoint(IPAddress.Parse("127.0.0.1"), port);
					sender.Send(msgBytes, msgBytes.Length, receiver);
					Assert.IsTrue(received.WaitOne(5000));
				}
			}
		}

		[TestMethod]
		public void TestServiceRequest()
		{
			int port1 = 1312;
			int port2 = 3421;

			var config1 = this.mainConfig.Copy();
			config1.DiscoveryPort = port1;

			var config2 = this.mainConfig.Copy();
			config2.DiscoveryPort = port2;
			using (var discovery1 = new ServiceDiscovery(config1))
			{
				using (var discovery2 = new ServiceDiscovery(config2))
				{
					var received1 = new ManualResetEvent(false);
					discovery1.AvailableServices.CollectionChanged += (o, args) =>
					{
						if (args.Action == NotifyCollectionChangedAction.Add)
						{
							var info = (ServiceInformation)args.NewItems[0];
							Assert.AreEqual(IPAddress.Loopback.ToString(), info.Host.ToString());
							Assert.AreEqual(ServiceInformations.UserName, info.ServiceId);
							received1.Set();
						}
					};
					var received2 = new ManualResetEvent(false);
					discovery2.AvailableServices.CollectionChanged += (o, args) =>
					{
						if (args.Action == NotifyCollectionChangedAction.Add)
						{
							var info = (ServiceInformation)args.NewItems[0];
							Assert.AreEqual(IPAddress.Loopback.ToString(), info.Host.ToString());
							Assert.AreEqual(ServiceInformations.UserName, info.ServiceId);
							received2.Set();
						}
					};
					discovery1.Start();
					discovery2.Start();
					discovery1.SendServiceRequest(new IPEndPoint(IPAddress.Loopback, port2));

					Assert.IsTrue(received1.WaitOne(4000));
					Assert.IsTrue(received2.WaitOne(100));
				}
			}
		}

		[TestMethod]
		public void TestServiceRecheck()
		{
			var port1 = this.mainConfig.DiscoveryPort;
			var port2 = this.mainConfig.DiscoveryPort + 1;

			var config1 = this.mainConfig.Copy();
			config1.DiscoveryPort = port1;
			config1.ForeignDiscoveryPort = port2;
			config1.CheckServiceStateInterval = 500;

			var config2 = this.mainConfig.Copy();
			config2.DiscoveryPort = port2;
			config2.ForeignDiscoveryPort = port1;
			config2.CheckServiceStateInterval = 500;

			using (var discovery1 = new ServiceDiscovery(config1))
			{
				using (var discovery2 = new ServiceDiscovery(config2))
				{
					var discovered = new ManualResetEvent(false);
					var discoveredDown = new ManualResetEvent(false);
					discovery2.ServiceDiscovered += information =>
					{
						discovered.Set();
					};
					discovery2.ServiceOffline += information =>
					{
						discoveredDown.Set();
					};
					discovery2.Start();
					discovery1.SendServiceAnnouncement(new IPEndPoint(IPAddress.Loopback, config2.DiscoveryPort));
					discovery1.Dispose();
					Assert.IsTrue(discovered.WaitOne(4000), "Timeout for service discovered event has been reached");
					Assert.IsTrue(discoveredDown.WaitOne(2000), "Timeout for service state down event has been reached"); 
				}
			}
		}
	}
}