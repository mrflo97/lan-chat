using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public class Config
	{
		public int DiscoveryPort = 54322;

		public int ForeignDiscoveryPort = 54322;

		public int MessagePort = 3424;

		public int HostScanTimeout = 5000;

		public string EncodingString = "utf-8";

		public string MessageSeparator = "x";

		public string PartSeparator = "y";

		public int CheckServiceStateInterval = 2000;

		public int ServiceRequestTimeout = 1000;

		public Encoding MessageEncoding => Encoding.GetEncoding(this.EncodingString);

		public Config Copy()
		{
			return (Config)this.MemberwiseClone();
		}
	}
}
