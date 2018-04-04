using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
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

        private NotifyIcon notify;

		private readonly bool removeParticipantWhenOffline;

	    private readonly MessageStorage sentMsgs;

        private Stopwatch lastSentStopwatch;

        private string lastSentMessage;

        private static bool _enableGlobalNotifications = true;

	    private static readonly List<ChatForm> instances = new List<ChatForm>();

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

        public ChatForm(bool removeParticipantWhenOffline = true)
		{
            this.lastSentStopwatch = new Stopwatch();            
            this.InitializeComponent();
			this.removeParticipantWhenOffline = removeParticipantWhenOffline;
		    this.sentMsgs = new MessageStorage(30);
		    ChatForm.instances.Add(this);
		    this.globalNotificationCheckbox.Checked = ChatForm.enableGlobalNotfications;
            this.Shown += (sender, e) =>
            {
                var resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
                this.BeginInvoke(new Action(() =>
                {
                    this.notify = new NotifyIcon();
                    this.notify.Text = this.Text;
                    this.notify.Icon = ((Icon)(resources.GetObject("$this.Icon")));
                    this.notify.Visible = true;
                    this.notify.BalloonTipClicked += (sender1, e1) =>
                    {
                        this.toFront();
                    };
                }));
            };
		}

        public ChatForm(IEnumerable<string> participants, bool removeParticipantWhenOffline = true) : this(removeParticipantWhenOffline)
		{
			foreach (var participant in participants)
			{
				var index = this.participantsListBox.Items.Add(participant);
			}

		    this.Text = $"Chat with {string.Join(", ", participants.Where(participant => participant != ServiceInformations.UserName).Select(x => x.Split('\\').Last()))}";

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
			    if (this.globalNotificationCheckbox.Checked && this.localNotificationCheckbox.Checked && !this.isActive(this.Handle))
			    {
                    this.showToast((this.Text == "Public Chat" ? "public: " : "private: ") + username.Split('\\').Last(), message);
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

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        private bool isActive(IntPtr handle)
        {
            IntPtr activeHandle = GetForegroundWindow();
            return (activeHandle == handle);
        }

        private void toFront()
        {
            this.WindowState = FormWindowState.Minimized;
            this.Show();
            this.WindowState = FormWindowState.Normal;
        }

        private void showToast(string title, string content)
        {
            this.notify.ShowBalloonTip(2, title, content, ToolTipIcon.Info);
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
            if(text == this.lastSentMessage && this.lastSentStopwatch.ElapsedMilliseconds < 5000)
            {
                this.lastSentStopwatch.Restart();
                MessageBox.Show("Spam nit so gschissn");
                return;
            }
            if (text != string.Empty)
            {
                this.lastSentMessage = text;
                this.lastSentStopwatch.Restart();
                this.ShowMessage("self", text);
                this.SendMessage?.Invoke(text);
                this.sentMsgs.AddMsg(text);
            }
            this.messageTextBox.ResetText();
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
            this.notify.Dispose();
        }
    }
}
