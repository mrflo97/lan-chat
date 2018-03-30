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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.openPrivateChatButton = new System.Windows.Forms.Button();
			this.participantsListBox = new System.Windows.Forms.ListBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.publicChatSplitContainer = new System.Windows.Forms.SplitContainer();
			this.publicChatTextBox = new System.Windows.Forms.RichTextBox();
			this.messageTextBox = new System.Windows.Forms.RichTextBox();
			this.sendButton = new System.Windows.Forms.Button();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
			this.groupBox1.Size = new System.Drawing.Size(259, 487);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Participants";
			// 
			// openPrivateChatButton
			// 
			this.openPrivateChatButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.openPrivateChatButton.Location = new System.Drawing.Point(6, 449);
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
			this.participantsListBox.Size = new System.Drawing.Size(247, 420);
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
			// 
			// messageTextBox
			// 
			this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.messageTextBox.Location = new System.Drawing.Point(0, 3);
			this.messageTextBox.Name = "messageTextBox";
			this.messageTextBox.Size = new System.Drawing.Size(444, 61);
			this.messageTextBox.TabIndex = 1;
			this.messageTextBox.Text = "";
			this.messageTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.messageTextBox_KeyPress);
			// 
			// sendButton
			// 
			this.sendButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.sendButton.Location = new System.Drawing.Point(450, 3);
			this.sendButton.Name = "sendButton";
			this.sendButton.Size = new System.Drawing.Size(48, 61);
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
			// ChatForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(819, 517);
			this.Controls.Add(this.splitContainer1);
			this.Name = "ChatForm";
			this.Text = "Public Chat";
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
	}
}

