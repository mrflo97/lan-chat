namespace lan_chat
{
	partial class ChatForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.openPrivateChatButton = new System.Windows.Forms.Button();
            this.participantsListBox = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.publicChatSplitContainer = new System.Windows.Forms.SplitContainer();
            this.publicChatTextBox = new System.Windows.Forms.RichTextBox();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.sendButton = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.settingsGroupBox = new System.Windows.Forms.GroupBox();
            this.localNotificationCheckbox = new System.Windows.Forms.CheckBox();
            this.globalNotificationCheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.publicChatSplitContainer)).BeginInit();
            this.publicChatSplitContainer.Panel1.SuspendLayout();
            this.publicChatSplitContainer.Panel2.SuspendLayout();
            this.publicChatSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.settingsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.openPrivateChatButton);
            this.groupBox1.Controls.Add(this.participantsListBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(259, 415);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Participants";
            // 
            // openPrivateChatButton
            // 
            this.openPrivateChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.openPrivateChatButton.Location = new System.Drawing.Point(6, 377);
            this.openPrivateChatButton.Name = "openPrivateChatButton";
            this.openPrivateChatButton.Size = new System.Drawing.Size(247, 32);
            this.openPrivateChatButton.TabIndex = 2;
            this.openPrivateChatButton.Text = "Open Chat With Selected";
            this.openPrivateChatButton.UseVisualStyleBackColor = true;
            this.openPrivateChatButton.Click += new System.EventHandler(this.openPrivateChat_Click);
            // 
            // participantsListBox
            // 
            this.participantsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.participantsListBox.FormattingEnabled = true;
            this.participantsListBox.Location = new System.Drawing.Point(6, 19);
            this.participantsListBox.Name = "participantsListBox";
            this.participantsListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.participantsListBox.Size = new System.Drawing.Size(247, 342);
            this.participantsListBox.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.publicChatSplitContainer);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(514, 487);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chat";
            // 
            // publicChatSplitContainer
            // 
            this.publicChatSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.publicChatSplitContainer.Location = new System.Drawing.Point(7, 19);
            this.publicChatSplitContainer.Name = "publicChatSplitContainer";
            this.publicChatSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // publicChatSplitContainer.Panel1
            // 
            this.publicChatSplitContainer.Panel1.Controls.Add(this.publicChatTextBox);
            // 
            // publicChatSplitContainer.Panel2
            // 
            this.publicChatSplitContainer.Panel2.Controls.Add(this.messageTextBox);
            this.publicChatSplitContainer.Panel2.Controls.Add(this.sendButton);
            this.publicChatSplitContainer.Size = new System.Drawing.Size(501, 462);
            this.publicChatSplitContainer.SplitterDistance = 385;
            this.publicChatSplitContainer.SplitterWidth = 10;
            this.publicChatSplitContainer.TabIndex = 5;
            // 
            // publicChatTextBox
            // 
            this.publicChatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.publicChatTextBox.Location = new System.Drawing.Point(0, 0);
            this.publicChatTextBox.Name = "publicChatTextBox";
            this.publicChatTextBox.ReadOnly = true;
            this.publicChatTextBox.Size = new System.Drawing.Size(498, 382);
            this.publicChatTextBox.TabIndex = 0;
            this.publicChatTextBox.Text = "";
            this.publicChatTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.publicChatTextBox_LinkClicked);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(1, 3);
            this.messageTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(443, 63);
            this.messageTextBox.TabIndex = 1;
            this.messageTextBox.Text = "";
            this.messageTextBox.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.messageTextBox_LinkClicked);
            this.messageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.messageTextBox_KeyDown);
            // 
            // sendButton
            // 
            this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendButton.Location = new System.Drawing.Point(450, 3);
            this.sendButton.Margin = new System.Windows.Forms.Padding(1);
            this.sendButton.Name = "sendButton";
            this.sendButton.Size = new System.Drawing.Size(48, 63);
            this.sendButton.TabIndex = 2;
            this.sendButton.Text = "Send";
            this.sendButton.UseVisualStyleBackColor = true;
            this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 12);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.settingsGroupBox);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(795, 493);
            this.splitContainer1.SplitterDistance = 265;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 3;
            // 
            // settingsGroupBox
            // 
            this.settingsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsGroupBox.Controls.Add(this.localNotificationCheckbox);
            this.settingsGroupBox.Controls.Add(this.globalNotificationCheckbox);
            this.settingsGroupBox.Location = new System.Drawing.Point(9, 424);
            this.settingsGroupBox.Name = "settingsGroupBox";
            this.settingsGroupBox.Size = new System.Drawing.Size(247, 66);
            this.settingsGroupBox.TabIndex = 2;
            this.settingsGroupBox.TabStop = false;
            this.settingsGroupBox.Text = "Settings";
            // 
            // localNotificationCheckbox
            // 
            this.localNotificationCheckbox.AutoSize = true;
            this.localNotificationCheckbox.Checked = true;
            this.localNotificationCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.localNotificationCheckbox.Location = new System.Drawing.Point(6, 42);
            this.localNotificationCheckbox.Name = "localNotificationCheckbox";
            this.localNotificationCheckbox.Size = new System.Drawing.Size(190, 17);
            this.localNotificationCheckbox.TabIndex = 1;
            this.localNotificationCheckbox.Text = "enable notifications for this window";
            this.localNotificationCheckbox.UseVisualStyleBackColor = true;
            // 
            // globalNotificationCheckbox
            // 
            this.globalNotificationCheckbox.AutoSize = true;
            this.globalNotificationCheckbox.Checked = true;
            this.globalNotificationCheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.globalNotificationCheckbox.Location = new System.Drawing.Point(6, 19);
            this.globalNotificationCheckbox.Name = "globalNotificationCheckbox";
            this.globalNotificationCheckbox.Size = new System.Drawing.Size(155, 17);
            this.globalNotificationCheckbox.TabIndex = 0;
            this.globalNotificationCheckbox.Text = "enable notifications globally";
            this.globalNotificationCheckbox.UseVisualStyleBackColor = true;
            this.globalNotificationCheckbox.CheckedChanged += new System.EventHandler(this.globalNotificationCheckbox_CheckedChanged);
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 517);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Text = "Public Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.publicChatSplitContainer.Panel1.ResumeLayout(false);
            this.publicChatSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.publicChatSplitContainer)).EndInit();
            this.publicChatSplitContainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.settingsGroupBox.ResumeLayout(false);
            this.settingsGroupBox.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ListBox participantsListBox;
		private System.Windows.Forms.Button openPrivateChatButton;
		private System.Windows.Forms.SplitContainer publicChatSplitContainer;
		private System.Windows.Forms.RichTextBox publicChatTextBox;
		private System.Windows.Forms.RichTextBox messageTextBox;
		private System.Windows.Forms.Button sendButton;
        private System.Windows.Forms.GroupBox settingsGroupBox;
        private System.Windows.Forms.CheckBox localNotificationCheckbox;
        private System.Windows.Forms.CheckBox globalNotificationCheckbox;
    }
}

