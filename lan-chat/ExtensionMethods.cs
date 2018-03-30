using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public static class ExtensionMethods
	{
		public static string[] Split(this string value, string splitter)
		{
			return value.Split(new[] { splitter }, StringSplitOptions.None);
		}
	}
}
