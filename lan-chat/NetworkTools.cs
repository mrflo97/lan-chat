using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TakeIo.NetworkAddress;
using IPAddressRange = NetTools.IPAddressRange;

namespace lan_chat
{
	class NetworkTools
	{
		private static readonly TimedBuffer<IEnumerable<IPAddress>> localAdresses;

		public static IEnumerable<IPAddress> LocalAdresses => NetworkTools.localAdresses.Value;

		static NetworkTools()
		{
			NetworkTools.localAdresses = new TimedBuffer<IEnumerable<IPAddress>>(() => Dns.GetHostAddresses(Dns.GetHostName()), 2000);
		}

		public static IEnumerable<UnicastIPAddressInformation> GetSubnetMasks()
		{
			var interfaces = NetworkInterface.GetAllNetworkInterfaces()
				.Where(x => x.OperationalStatus == OperationalStatus.Up)
				.Where(x => x.NetworkInterfaceType != NetworkInterfaceType.Loopback)
				.Where(x => x.NetworkInterfaceType != NetworkInterfaceType.Tunnel);
			foreach (var intf in interfaces)
			{
				var addresses = intf.GetIPProperties().UnicastAddresses.Where(x => x.Address.AddressFamily == AddressFamily.InterNetwork);
				foreach (var address in addresses)
				{
					yield return address;
				}
			}
		}

		public static IEnumerable<IPAddress> GetPossibleHosts(bool includeSelf = false)
		{
			var list = new List<IPAddress>();
			foreach (var info in NetworkTools.GetSubnetMasks())
			{
				var adress = new IPNetworkAddress(info.Address, info.IPv4Mask);
				var range = new IPAddressRange(adress.MinAddress, adress.MaxAddress);
				var tempList = new List<IPAddress>();
				foreach (var addr in range)
				{
					if (includeSelf || !NetworkTools.LocalAdresses.Contains(addr)) 
					{
						tempList.Add(addr);
					}
				}
				if (tempList.Count <= 256)
				{
					list.AddRange(tempList);
				}
			}
			return list;
		}
	}
}
