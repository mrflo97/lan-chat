using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lan_chat
{
    class MessageStorage
    {
        private string[] oldMsgs;

        private int nextIndex;

        private int outputIndex;

        public MessageStorage(int length)
        {
            this.oldMsgs = new string[length];
            this.nextIndex = 0;
        }

        public void AddMsg(string msg)
        {
            if (this.nextIndex == 0 || this.oldMsgs[this.nextIndex - 1] != msg)
            {
                if (this.nextIndex == oldMsgs.Length)
                {
                    var buffer = new string[this.oldMsgs.Length];
                    Array.Copy(this.oldMsgs, 1, buffer, 0, oldMsgs.Length - 1);
                    buffer[buffer.Length - 1] = msg;
                }
                else
                {
                    this.oldMsgs[nextIndex++] = msg;
                    this.outputIndex = nextIndex;
                }
            }
        }

        public string Last()
        {
            if (this.outputIndex - 1 >= 0)
            {
                this.outputIndex--;
            }

            return this.oldMsgs[this.outputIndex];
        }

        public string Next()
        {
            if (this.outputIndex + 1 <= this.nextIndex - 1)
            {
                this.outputIndex++;
            }

            return this.oldMsgs[this.outputIndex];
        }
    }
}
