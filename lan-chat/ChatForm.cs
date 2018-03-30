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

		private volatile bool controlPressed = false;

		private readonly bool removeParticipantWhenOffline;

		public ChatForm(bool removeParticipantWhenOffline = true)
		{
			this.InitializeComponent();
			this.removeParticipantWhenOffline = removeParticipantWhenOffline;
		}

		public ChatForm(IEnumerable<string> participants, bool removeParticipantWhenOffline = true) : this(removeParticipantWhenOffline)
		{
			foreach (var participant in participants)
			{
				var index = this.participantsListBox.Items.Add(participant);
			}
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
					this.publicChatTextBox.ScrollToCaret();
				});
		}

		public void AddParticipant(ServiceInformation information)
		{
			Tools.SafeInvoke(this.participantsListBox, () =>
			{
				this.participantsListBox.Items.Add(information.ServiceId);
				this.participantsListBox.Refresh();
			});
		}

		public void RemoveParticipant(ServiceInformation information)
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
			var text = this.messageTextBox.Text.Trim();
			this.ShowMessage("self", text);
			this.SendMessage?.Invoke(text);
			this.messageTextBox.ResetText();
		}

		private void messageTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (!this.controlPressed && e.KeyChar == (char)Keys.Enter)
			{
				this.sendButton_Click(this.sendButton, new EventArgs());
				e.Handled = true;
			}
		}

		private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				this.controlPressed = true;
			}
		}

		private void messageTextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.ControlKey)
			{
				this.controlPressed = false;
			}
		}
	}
}
