namespace ForumClientGui
{
    partial class ClientFormUI
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
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            this.loggedInTitle = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.registerLinkLabel = new System.Windows.Forms.LinkLabel();
            this.postsGrid = new System.Windows.Forms.DataGridView();
            this.postPanel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.addReply = new System.Windows.Forms.LinkLabel();
            this.postDateLabel = new System.Windows.Forms.Label();
            this.postUserLabel = new System.Windows.Forms.Label();
            this.postBody = new System.Windows.Forms.RichTextBox();
            this.postTitleLabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.repliesIndicator = new System.Windows.Forms.LinkLabel();
            this.repliesGrid = new System.Windows.Forms.DataGridView();
            this.subforumsGrid = new System.Windows.Forms.DataGridView();
            this.newPostPanel = new System.Windows.Forms.Panel();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.backPostLabel = new System.Windows.Forms.LinkLabel();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.postBodyTextBox = new System.Windows.Forms.RichTextBox();
            this.postTitleTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.replyToTitle = new System.Windows.Forms.Label();
            this.sendPostButton = new System.Windows.Forms.LinkLabel();
            this.loginPanel = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.passTextBox = new System.Windows.Forms.TextBox();
            this.subforumsComboBox = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).BeginInit();
            this.postPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.repliesGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.subforumsGrid)).BeginInit();
            this.newPostPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.loginPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(282, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sagioto Forum";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.loginPanel);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.loggedInTitle);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.registerLinkLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.newPostPanel);
            this.splitContainer1.Panel2.Controls.Add(this.postsGrid);
            this.splitContainer1.Panel2.Controls.Add(this.postPanel);
            this.splitContainer1.Panel2.Controls.Add(this.subforumsGrid);
            this.splitContainer1.Size = new System.Drawing.Size(805, 451);
            this.splitContainer1.SplitterDistance = 83;
            this.splitContainer1.TabIndex = 4;
            // 
            // linkLabel3
            // 
            this.linkLabel3.AutoSize = true;
            this.linkLabel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel3.Location = new System.Drawing.Point(12, 47);
            this.linkLabel3.Name = "linkLabel3";
            this.linkLabel3.Size = new System.Drawing.Size(97, 15);
            this.linkLabel3.TabIndex = 5;
            this.linkLabel3.TabStop = true;
            this.linkLabel3.Text = "New Message";
            this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
            // 
            // loggedInTitle
            // 
            this.loggedInTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.loggedInTitle.AutoSize = true;
            this.loggedInTitle.Location = new System.Drawing.Point(623, 32);
            this.loggedInTitle.Name = "loggedInTitle";
            this.loggedInTitle.Size = new System.Drawing.Size(71, 13);
            this.loggedInTitle.TabIndex = 4;
            this.loggedInTitle.Text = "Logged in as:";
            // 
            // linkLabel2
            // 
            this.linkLabel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(739, 10);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(53, 15);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "LogOut";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(688, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(43, 15);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "LogIn";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // registerLinkLabel
            // 
            this.registerLinkLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.registerLinkLabel.AutoSize = true;
            this.registerLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLinkLabel.Location = new System.Drawing.Point(621, 9);
            this.registerLinkLabel.Name = "registerLinkLabel";
            this.registerLinkLabel.Size = new System.Drawing.Size(61, 15);
            this.registerLinkLabel.TabIndex = 1;
            this.registerLinkLabel.TabStop = true;
            this.registerLinkLabel.Text = "Register";
            this.registerLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.registerLinkLabel_LinkClicked);
            // 
            // postsGrid
            // 
            this.postsGrid.AllowUserToAddRows = false;
            this.postsGrid.AllowUserToDeleteRows = false;
            this.postsGrid.BackgroundColor = System.Drawing.Color.DarkGray;
            this.postsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.postsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.postsGrid.ColumnHeadersVisible = false;
            this.postsGrid.Location = new System.Drawing.Point(12, 166);
            this.postsGrid.Name = "postsGrid";
            this.postsGrid.ReadOnly = true;
            this.postsGrid.Size = new System.Drawing.Size(238, 155);
            this.postsGrid.TabIndex = 2;
            this.postsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.postsGrid_CellContentClick);
            // 
            // postPanel
            // 
            this.postPanel.Controls.Add(this.splitContainer2);
            this.postPanel.Location = new System.Drawing.Point(465, 15);
            this.postPanel.Name = "postPanel";
            this.postPanel.Size = new System.Drawing.Size(337, 287);
            this.postPanel.TabIndex = 1;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.addReply);
            this.splitContainer2.Panel1.Controls.Add(this.postDateLabel);
            this.splitContainer2.Panel1.Controls.Add(this.postUserLabel);
            this.splitContainer2.Panel1.Controls.Add(this.postBody);
            this.splitContainer2.Panel1.Controls.Add(this.postTitleLabel);
            this.splitContainer2.Panel1.Controls.Add(this.label3);
            this.splitContainer2.Panel1.Controls.Add(this.linkLabel4);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.AutoScroll = true;
            this.splitContainer2.Panel2.Controls.Add(this.repliesIndicator);
            this.splitContainer2.Panel2.Controls.Add(this.repliesGrid);
            this.splitContainer2.Size = new System.Drawing.Size(337, 287);
            this.splitContainer2.SplitterDistance = 188;
            this.splitContainer2.TabIndex = 0;
            // 
            // addReply
            // 
            this.addReply.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.addReply.AutoSize = true;
            this.addReply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addReply.Location = new System.Drawing.Point(196, 164);
            this.addReply.Name = "addReply";
            this.addReply.Size = new System.Drawing.Size(71, 15);
            this.addReply.TabIndex = 9;
            this.addReply.TabStop = true;
            this.addReply.Text = "Add Reply";
            this.addReply.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.addReply_LinkClicked);
            // 
            // postDateLabel
            // 
            this.postDateLabel.AutoSize = true;
            this.postDateLabel.Location = new System.Drawing.Point(7, 68);
            this.postDateLabel.Name = "postDateLabel";
            this.postDateLabel.Size = new System.Drawing.Size(33, 13);
            this.postDateLabel.TabIndex = 8;
            this.postDateLabel.Text = "Date:";
            // 
            // postUserLabel
            // 
            this.postUserLabel.AutoSize = true;
            this.postUserLabel.Location = new System.Drawing.Point(5, 49);
            this.postUserLabel.Name = "postUserLabel";
            this.postUserLabel.Size = new System.Drawing.Size(32, 13);
            this.postUserLabel.TabIndex = 7;
            this.postUserLabel.Text = "User:";
            // 
            // postBody
            // 
            this.postBody.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.postBody.BackColor = System.Drawing.Color.Gainsboro;
            this.postBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.postBody.Location = new System.Drawing.Point(8, 89);
            this.postBody.Name = "postBody";
            this.postBody.Size = new System.Drawing.Size(326, 107);
            this.postBody.TabIndex = 5;
            this.postBody.Text = "";
            // 
            // postTitleLabel
            // 
            this.postTitleLabel.AutoSize = true;
            this.postTitleLabel.Location = new System.Drawing.Point(5, 29);
            this.postTitleLabel.Name = "postTitleLabel";
            this.postTitleLabel.Size = new System.Drawing.Size(51, 13);
            this.postTitleLabel.TabIndex = 4;
            this.postTitleLabel.Text = "Post Title";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 6);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Title:";
            // 
            // linkLabel4
            // 
            this.linkLabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.Location = new System.Drawing.Point(296, 6);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(38, 15);
            this.linkLabel4.TabIndex = 2;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Back";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
            // 
            // repliesIndicator
            // 
            this.repliesIndicator.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.repliesIndicator.AutoSize = true;
            this.repliesIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repliesIndicator.Location = new System.Drawing.Point(185, 31);
            this.repliesIndicator.Name = "repliesIndicator";
            this.repliesIndicator.Size = new System.Drawing.Size(95, 15);
            this.repliesIndicator.TabIndex = 6;
            this.repliesIndicator.TabStop = true;
            this.repliesIndicator.Text = "Show Replies";
            this.repliesIndicator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.repliesIndicator_LinkClicked);
            // 
            // repliesGrid
            // 
            this.repliesGrid.AllowUserToAddRows = false;
            this.repliesGrid.AllowUserToDeleteRows = false;
            this.repliesGrid.BackgroundColor = System.Drawing.Color.White;
            this.repliesGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.repliesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.repliesGrid.ColumnHeadersVisible = false;
            this.repliesGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.repliesGrid.Location = new System.Drawing.Point(0, 0);
            this.repliesGrid.Name = "repliesGrid";
            this.repliesGrid.ReadOnly = true;
            this.repliesGrid.Size = new System.Drawing.Size(337, 95);
            this.repliesGrid.TabIndex = 3;
            // 
            // subforumsGrid
            // 
            this.subforumsGrid.AllowUserToAddRows = false;
            this.subforumsGrid.AllowUserToDeleteRows = false;
            this.subforumsGrid.BackgroundColor = System.Drawing.Color.White;
            this.subforumsGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.subforumsGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.subforumsGrid.ColumnHeadersVisible = false;
            this.subforumsGrid.Location = new System.Drawing.Point(12, 24);
            this.subforumsGrid.Name = "subforumsGrid";
            this.subforumsGrid.ReadOnly = true;
            this.subforumsGrid.Size = new System.Drawing.Size(244, 129);
            this.subforumsGrid.TabIndex = 0;
            this.subforumsGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.subforumsGrid_CellClick);
            // 
            // newPostPanel
            // 
            this.newPostPanel.Controls.Add(this.splitContainer3);
            this.newPostPanel.Location = new System.Drawing.Point(61, 34);
            this.newPostPanel.Name = "newPostPanel";
            this.newPostPanel.Size = new System.Drawing.Size(337, 287);
            this.newPostPanel.TabIndex = 3;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.splitContainer4);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.replyToTitle);
            this.splitContainer3.Panel2.Controls.Add(this.backPostLabel);
            this.splitContainer3.Size = new System.Drawing.Size(337, 287);
            this.splitContainer3.SplitterDistance = 253;
            this.splitContainer3.TabIndex = 0;
            // 
            // backPostLabel
            // 
            this.backPostLabel.AutoSize = true;
            this.backPostLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.backPostLabel.Location = new System.Drawing.Point(151, 7);
            this.backPostLabel.Name = "backPostLabel";
            this.backPostLabel.Size = new System.Drawing.Size(38, 15);
            this.backPostLabel.TabIndex = 2;
            this.backPostLabel.TabStop = true;
            this.backPostLabel.Text = "Back";
            this.backPostLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.backPostLabel_LinkClicked);
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            this.splitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.label8);
            this.splitContainer4.Panel1.Controls.Add(this.subforumsComboBox);
            this.splitContainer4.Panel1.Controls.Add(this.sendPostButton);
            this.splitContainer4.Panel1.Controls.Add(this.label4);
            this.splitContainer4.Panel1.Controls.Add(this.label7);
            this.splitContainer4.Panel1.Controls.Add(this.postTitleTextBox);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.postBodyTextBox);
            this.splitContainer4.Size = new System.Drawing.Size(337, 253);
            this.splitContainer4.SplitterDistance = 78;
            this.splitContainer4.TabIndex = 11;
            // 
            // postBodyTextBox
            // 
            this.postBodyTextBox.BackColor = System.Drawing.SystemColors.Window;
            this.postBodyTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.postBodyTextBox.Location = new System.Drawing.Point(0, 0);
            this.postBodyTextBox.Name = "postBodyTextBox";
            this.postBodyTextBox.Size = new System.Drawing.Size(337, 171);
            this.postBodyTextBox.TabIndex = 6;
            this.postBodyTextBox.Text = "";
            // 
            // postTitleTextBox
            // 
            this.postTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.postTitleTextBox.Location = new System.Drawing.Point(5, 26);
            this.postTitleTextBox.Name = "postTitleTextBox";
            this.postTitleTextBox.Size = new System.Drawing.Size(326, 20);
            this.postTitleTextBox.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(4, 6);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(30, 13);
            this.label7.TabIndex = 11;
            this.label7.Text = "Title:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Message:";
            // 
            // replyToTitle
            // 
            this.replyToTitle.AutoSize = true;
            this.replyToTitle.Location = new System.Drawing.Point(4, 9);
            this.replyToTitle.Name = "replyToTitle";
            this.replyToTitle.Size = new System.Drawing.Size(53, 13);
            this.replyToTitle.TabIndex = 14;
            this.replyToTitle.Text = "Reply To:";
            // 
            // sendPostButton
            // 
            this.sendPostButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.sendPostButton.AutoSize = true;
            this.sendPostButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sendPostButton.Location = new System.Drawing.Point(262, 4);
            this.sendPostButton.Name = "sendPostButton";
            this.sendPostButton.Size = new System.Drawing.Size(72, 15);
            this.sendPostButton.TabIndex = 4;
            this.sendPostButton.TabStop = true;
            this.sendPostButton.Text = "Send Post";
            this.sendPostButton.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.sendPostButton_LinkClicked);
            // 
            // loginPanel
            // 
            this.loginPanel.Controls.Add(this.passTextBox);
            this.loginPanel.Controls.Add(this.usernameTextBox);
            this.loginPanel.Controls.Add(this.label6);
            this.loginPanel.Controls.Add(this.label5);
            this.loginPanel.Location = new System.Drawing.Point(624, 26);
            this.loginPanel.Name = "loginPanel";
            this.loginPanel.Size = new System.Drawing.Size(166, 54);
            this.loginPanel.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 8);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "UserName:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 35);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Password:";
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(66, 5);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(97, 20);
            this.usernameTextBox.TabIndex = 6;
            // 
            // passTextBox
            // 
            this.passTextBox.Location = new System.Drawing.Point(66, 30);
            this.passTextBox.Name = "passTextBox";
            this.passTextBox.PasswordChar = '*';
            this.passTextBox.Size = new System.Drawing.Size(97, 20);
            this.passTextBox.TabIndex = 7;
            // 
            // subforumsComboBox
            // 
            this.subforumsComboBox.FormattingEnabled = true;
            this.subforumsComboBox.Location = new System.Drawing.Point(210, 55);
            this.subforumsComboBox.Name = "subforumsComboBox";
            this.subforumsComboBox.Size = new System.Drawing.Size(121, 21);
            this.subforumsComboBox.TabIndex = 14;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(151, 58);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "Subforum:";
            // 
            // ClientFormUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(805, 451);
            this.Controls.Add(this.splitContainer1);
            this.Name = "ClientFormUI";
            this.Text = "ClientFormUI";
            this.Load += new System.EventHandler(this.ClientFormUI_Load_1);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).EndInit();
            this.postPanel.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.repliesGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.subforumsGrid)).EndInit();
            this.newPostPanel.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            this.splitContainer3.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel1.PerformLayout();
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.loginPanel.ResumeLayout(false);
            this.loginPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label loggedInTitle;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.LinkLabel registerLinkLabel;
        private System.Windows.Forms.DataGridView subforumsGrid;
        private System.Windows.Forms.Panel postPanel;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.LinkLabel repliesIndicator;
        private System.Windows.Forms.RichTextBox postBody;
        private System.Windows.Forms.Label postTitleLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.LinkLabel linkLabel4;
        private System.Windows.Forms.Label postDateLabel;
        private System.Windows.Forms.Label postUserLabel;
        private System.Windows.Forms.DataGridView postsGrid;
        private System.Windows.Forms.DataGridView repliesGrid;
        private System.Windows.Forms.LinkLabel addReply;
        private System.Windows.Forms.LinkLabel linkLabel3;
        private System.Windows.Forms.Panel newPostPanel;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.LinkLabel backPostLabel;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox postTitleTextBox;
        private System.Windows.Forms.RichTextBox postBodyTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label replyToTitle;
        private System.Windows.Forms.LinkLabel sendPostButton;
        private System.Windows.Forms.Panel loginPanel;
        private System.Windows.Forms.TextBox passTextBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox subforumsComboBox;
    }
}