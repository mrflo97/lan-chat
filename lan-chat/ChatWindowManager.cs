using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lan_chat
{
	class ChatWindowManager : IDisposable
	{
		public event Action<ParticipantList, string> SendMessage;

		private readonly Dictionary<ParticipantList, ChatForm> chats;

		private readonly object chatsLock = new object();

		private Form mainForm;

		private volatile bool closing = false;

		public ChatWindowManager(Form mainForm)
		{
			this.chats = new Dictionary<ParticipantList, ChatForm>();
			this.mainForm = mainForm;
		}

		public void OpenChat(ParticipantList list)
		{
			this.getChatWindow(list);
		}

		private ChatForm getChatWindow(ParticipantList list)
		{
			ChatForm form;
			lock (this.chatsLock)
			{
				ChatForm existingChatForm;
				if (this.chats.TryGetValue(list, out existingChatForm))
				{
                    /*Tools.SafeInvoke(this.mainForm, () =>
					{
						existingChatForm.Activate();
					});*/
                    form = existingChatForm;
				}
				else
				{
					form = this.createChatForm(list);
					this.chats.Add(list, form);
				}
			}
			return form;
		}

		public void NewPrivateMessage(ParticipantList list, string sender, string message)
		{
			var form = this.getChatWindow(list);
			form.ShowMessage(sender, message);
		}

		private ChatForm createChatForm(ParticipantList list)
		{
			var chatForm = new ChatForm(list);
			Tools.SafeInvoke(this.mainForm, () =>
			{
				chatForm.SendMessage += msg => this.SendMessage?.Invoke(list, msg);
				chatForm.OpenPrivateChat += this.OpenChat;
				//chatForm.Show(this.mainForm);
				chatForm.Show();
				chatForm.Closed += (sender, args) => this.chatFormOnClosed(list);
			});
			return chatForm;
		}

	    private void chatFormOnClosed(ParticipantList list)
		{
			if (!this.closing)
			{
				lock (this.chats)
				{
					this.chats.Remove(list);
				}
			}
		}

		public void Dispose()
		{
			this.mainForm?.Dispose();
			foreach (var chatWindow in this.chats.Values)
			{
				chatWindow?.Dispose();
			}
		}

		public void CloseChatForms()
		{
			this.closing = true;
			foreach (var chatWindow in this.chats.Values)
			{
				chatWindow.Close();
			}
		}
	}
}
