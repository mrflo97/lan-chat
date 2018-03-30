using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lan_chat
{
	public class Tools
	{
		public static void SafeInvoke(Control control, Action action)
		{
			if (control.InvokeRequired)
			{
				control.Invoke(action);
			}
			else
			{
				action();
			}
		}

		public static string BuildMessage(Config config, params string[] parts)
		{
			return string.Join(config.PartSeparator, parts);
		}

		public static string FinalizeMessage(Config config, string message)
		{
			return message + config.MessageSeparator;
		}
	}
}
