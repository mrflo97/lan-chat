using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
	class TimedBuffer<T>
	{
		private T value;

		private readonly Stopwatch stopWatch;

		private readonly int refreshInterval;

		private readonly Func<T> refreshFunc;

		private readonly object lockObj = new object();

		public T Value
		{
			get
			{
				lock (this.lockObj)
				{
					if (this.stopWatch.ElapsedMilliseconds > this.refreshInterval || this.value == null)
					{
						this.value = this.refreshFunc();
						this.stopWatch.Restart();
					}
					return this.value;
				}
			}
		}


		public TimedBuffer(Func<T> refreshValue, int refreshInterval)
		{
			this.refreshFunc = refreshValue;
			this.refreshInterval = refreshInterval;
			this.stopWatch = new Stopwatch();
			this.stopWatch.Start();
		}
	}
}
