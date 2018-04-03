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
using System.Windows.Forms.VisualStyles;

namespace lan_chat
{
	public partial class ChatForm : Form
	{
		public event Action<ParticipantList> OpenPrivateChat;

	    public event Action<string> SendMessage;

		private readonly bool removeParticipantWhenOffline;

	    private readonly MessageStorage sentMsgs;

	    private static bool _enableGlobalNotifications = true;

	    private static bool enableGlobalNotfications
	    {
	        get => _enableGlobalNotifications;
	        set
	        {
	            if (value != _enableGlobalNotifications)
	            {
	                _enableGlobalNotifications = value;
                    instances.ForEach(instance => instance.SetGlobalNotificationState(value));
	            }
	        }
	    }

	    private static readonly List<ChatForm> instances = new List<ChatForm>();

		public ChatForm(bool removeParticipantWhenOffline = true)
		{
			this.InitializeComponent();
			this.removeParticipantWhenOffline = removeParticipantWhenOffline;
		    this.sentMsgs = new MessageStorage(30);
		    ChatForm.instances.Add(this);
		    this.globalNotificationCheckbox.Checked = ChatForm.enableGlobalNotfications;
		}

		public ChatForm(IEnumerable<string> participants, bool removeParticipantWhenOffline = true) : this(removeParticipantWhenOffline)
		{
			foreach (var participant in participants)
			{
				var index = this.participantsListBox.Items.Add(participant);
			}

		    this.Text = $"Private Chat with {string.Join(", ", participants.Select(x => x.Split('\\').Last()))}";

		}

		public void ShowMessage(string username, string message)
		{
			Tools.SafeInvoke(this.publicChatTextBox, () =>
			{
				this.publicChatTextBox.DeselectAll();
				this.publicChatTextBox.SelectionFont = new Font(this.publicChatTextBox.SelectionFont, FontStyle.Bold);
				this.publicChatTextBox.AppendText(username);
				this.publicChatTextBox.SelectionFont = new Font(this.publicChatTextBox.SelectionFont, FontStyle.Regular);
				this.publicChatTextBox.AppendText(": ");
				this.publicChatTextBox.AppendText(message);
				this.publicChatTextBox.AppendText("\r\n");
				this.publicChatTextBox.ScrollToCaret();
			    if (this.globalNotificationCheckbox.Checked && this.localNotificationCheckbox.Checked)
			    {
			        this.Activate();
                }
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
            Tools.SafeInvoke(this.participantsListBox, () =>
            {
                this.participantsListBox.Items.Remove(information.ServiceId);
                this.participantsListBox.Refresh();
            });
		}

	    public void SetGlobalNotificationState(bool state)
	    {
            Tools.SafeInvoke(this.globalNotificationCheckbox, () =>
            {
                this.globalNotificationCheckbox.CheckedChanged -= this.globalNotificationCheckbox_CheckedChanged;
                this.globalNotificationCheckbox.Checked = state;
                this.globalNotificationCheckbox.CheckedChanged += this.globalNotificationCheckbox_CheckedChanged;
            });
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
            this.sentMsgs.AddMsg(text);
		}

		private void messageTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (!e.Control && e.KeyCode == Keys.Enter)
			{
			    this.sendButton_Click(this.sendButton, new EventArgs());
			    e.Handled = true;
            }
			else if (e.Control && e.KeyCode == Keys.Up)
			{
			    this.messageTextBox.Text = this.sentMsgs.Last();
			    e.Handled = true;
            }
			else if (e.Control && e.KeyCode == Keys.Down)
			{
			    this.messageTextBox.Text = this.sentMsgs.Next();
			    e.Handled = true;
            }
		    
        }

        private void publicChatTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            this.linkClicked(e);
        }

        private void messageTextBox_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            this.linkClicked(e);
        }

	    private void linkClicked(LinkClickedEventArgs e)
	    {
	        try
	        {
	            Process.Start(e.LinkText);
	        }
	        catch (Exception exception)
	        {
	            MessageBox.Show($"Couldn't open link, {exception.GetType()} occured: {exception.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
	        }
        }

        private void globalNotificationCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            ChatForm.enableGlobalNotfications = this.globalNotificationCheckbox.Checked;
        }

        private void ChatForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ChatForm.instances.Remove(this);
        }
    }
}
