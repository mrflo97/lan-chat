using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public class ServiceInformations
	{
		private static IEnumerable<IPAddress> availableHosts;

		private static readonly HostScanner scanner = new HostScanner();

		public static string LoggedInUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name.Split('\\').Last();

		public static string HostName = System.Environment.MachineName;

		public static string UserName = $"{ServiceInformations.HostName}\\{ServiceInformations.LoggedInUser}";

		public static IEnumerable<IPAddress> GetAvailableHosts(bool forceRescan = false)
		{
			if (forceRescan || ServiceInformations.availableHosts == null)
			{
				ServiceInformations.availableHosts = ServiceInformations.scanner.GetUpHosts(NetworkTools.GetPossibleHosts(), 5000);
			}
			return ServiceInformations.availableHosts;
		}
	}
}
