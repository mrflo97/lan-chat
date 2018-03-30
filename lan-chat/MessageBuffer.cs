using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	public class MessageBuffer
	{
		public event Action<string> MessageReady;

		public string Separator { get; private set; }

		private string buffer;

		public MessageBuffer(string separator)
		{
			this.Separator = separator;
		}

		public void AddString(string messagePart)
		{
			this.buffer += messagePart;
			for (int separatorIndex = this.getSeparatorIndex(); separatorIndex >= 0; separatorIndex = this.getSeparatorIndex())
			{
				var msg = this.buffer.Remove(separatorIndex);
				this.buffer = this.buffer.Remove(0, separatorIndex + this.Separator.Length);
				this.MessageReady?.Invoke(msg);
			}
		}

		private int getSeparatorIndex()
		{
			return this.buffer.IndexOf(this.Separator, StringComparison.Ordinal);
		}
	}
}