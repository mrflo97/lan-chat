using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lan_chat
{
	public partial class ChatForm : Form
	{
		public event Action<ParticipantList> OpenPrivateChat; public event Action<string> SendMessage;

		public ChatForm()
		{
			this.InitializeComponent();
		}
		public void ShowMessage(string username, string message)
		{
			Tools.SafeInvoke(this.publicChatTextBox,
				() =>
				{
					this.publicChatTextBox.DeselectAll();
					this.publicChatTextBox.SelectionFont = new Font(this.publicChatTextBox.SelectionFont, FontStyle.Bold);
					this.publicChatTextBox.AppendText(username);
					this.publicChatTextBox.SelectionFont = new Font(this.publicChatTextBox.SelectionFont, FontStyle.Regular);
					this.publicChatTextBox.AppendText(": ");
					this.publicChatTextBox.AppendText(message);
					this.publicChatTextBox.AppendText("\r\n");
				});
		}

		public ChatForm(IEnumerable<string> participants) : this()
		{
			foreach (var participant in participants)
			{
				this.participantsListBox.Items.Add(participant);
			}
		}

		public void AddParticipant(ServiceInformation information)
		{
			Tools.SafeInvoke(this.participantsListBox, () =>
			{
				this.participantsListBox.Items.Add(information.ServiceId);
				this.participantsListBox.Refresh();
			});
		}

		public void RemoveParticiapnt(ServiceInformation information)
		{
			this.participantsListBox.Items.Remove(information.ServiceId);
			this.participantsListBox.Refresh();
		}

		private void openPrivateChat_Click(object sender, EventArgs e)
		{
			if (this.participantsListBox.SelectedItems.Count > 0)
			{
				string[] participants = new string[this.participantsListBox.SelectedItems.Count + 1];
				this.participantsListBox.SelectedItems.CopyTo(participants, 0);
				participants[participants.Length - 1] = ServiceInformations.UserName;
				var participansList = new ParticipantList(participants);
				this.OpenPrivateChat?.Invoke(participansList);
			}
		}

		private void sendButton_Click(object sender, EventArgs e)
		{
			this.ShowMessage("self", this.messageTextBox.Text);
			this.SendMessage?.Invoke(this.messageTextBox.Text);
			this.messageTextBox.ResetText();
		}
	}
}
