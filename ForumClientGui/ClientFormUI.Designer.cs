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
            this.label2 = new System.Windows.Forms.Label();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.registerLinkLabel = new System.Windows.Forms.LinkLabel();
            this.subforumsGrid = new System.Windows.Forms.DataGridView();
            this.postPanel = new System.Windows.Forms.Panel();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.linkLabel4 = new System.Windows.Forms.LinkLabel();
            this.label3 = new System.Windows.Forms.Label();
            this.postTitleLabel = new System.Windows.Forms.Label();
            this.postBody = new System.Windows.Forms.RichTextBox();
            this.repliesIndicator = new System.Windows.Forms.LinkLabel();
            this.postUserLabel = new System.Windows.Forms.Label();
            this.postDateLabel = new System.Windows.Forms.Label();
            this.postsGrid = new System.Windows.Forms.DataGridView();
            this.repliesGrid = new System.Windows.Forms.DataGridView();
            this.addReply = new System.Windows.Forms.LinkLabel();
            this.linkLabel3 = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.subforumsGrid)).BeginInit();
            this.postPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repliesGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(282, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(259, 57);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sagioto Forum";
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
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel3);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel2);
            this.splitContainer1.Panel1.Controls.Add(this.linkLabel1);
            this.splitContainer1.Panel1.Controls.Add(this.registerLinkLabel);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.postsGrid);
            this.splitContainer1.Panel2.Controls.Add(this.postPanel);
            this.splitContainer1.Panel2.Controls.Add(this.subforumsGrid);
            this.splitContainer1.Size = new System.Drawing.Size(805, 451);
            this.splitContainer1.SplitterDistance = 71;
            this.splitContainer1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(623, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Logged in as:";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(739, 10);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(53, 15);
            this.linkLabel2.TabIndex = 3;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "LogOut";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(688, 9);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(43, 15);
            this.linkLabel1.TabIndex = 2;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "LogIn";
            // 
            // registerLinkLabel
            // 
            this.registerLinkLabel.AutoSize = true;
            this.registerLinkLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registerLinkLabel.Location = new System.Drawing.Point(621, 9);
            this.registerLinkLabel.Name = "registerLinkLabel";
            this.registerLinkLabel.Size = new System.Drawing.Size(61, 15);
            this.registerLinkLabel.TabIndex = 1;
            this.registerLinkLabel.TabStop = true;
            this.registerLinkLabel.Text = "Register";
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
            this.subforumsGrid.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // postPanel
            // 
            this.postPanel.Controls.Add(this.splitContainer2);
            this.postPanel.Location = new System.Drawing.Point(307, 24);
            this.postPanel.Name = "postPanel";
            this.postPanel.Size = new System.Drawing.Size(474, 346);
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
            this.splitContainer2.Panel2.Controls.Add(this.repliesIndicator);
            this.splitContainer2.Panel2.Controls.Add(this.repliesGrid);
            this.splitContainer2.Size = new System.Drawing.Size(474, 346);
            this.splitContainer2.SplitterDistance = 227;
            this.splitContainer2.TabIndex = 0;
            // 
            // linkLabel4
            // 
            this.linkLabel4.AutoSize = true;
            this.linkLabel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel4.Location = new System.Drawing.Point(433, 6);
            this.linkLabel4.Name = "linkLabel4";
            this.linkLabel4.Size = new System.Drawing.Size(38, 15);
            this.linkLabel4.TabIndex = 2;
            this.linkLabel4.TabStop = true;
            this.linkLabel4.Text = "Back";
            this.linkLabel4.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel4_LinkClicked);
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
            // postTitleLabel
            // 
            this.postTitleLabel.AutoSize = true;
            this.postTitleLabel.Location = new System.Drawing.Point(5, 29);
            this.postTitleLabel.Name = "postTitleLabel";
            this.postTitleLabel.Size = new System.Drawing.Size(51, 13);
            this.postTitleLabel.TabIndex = 4;
            this.postTitleLabel.Text = "Post Title";
            // 
            // postBody
            // 
            this.postBody.BackColor = System.Drawing.Color.Gainsboro;
            this.postBody.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.postBody.Location = new System.Drawing.Point(8, 89);
            this.postBody.Name = "postBody";
            this.postBody.Size = new System.Drawing.Size(463, 107);
            this.postBody.TabIndex = 5;
            this.postBody.Text = "";
            // 
            // repliesIndicator
            // 
            this.repliesIndicator.AutoSize = true;
            this.repliesIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repliesIndicator.Location = new System.Drawing.Point(185, 51);
            this.repliesIndicator.Name = "repliesIndicator";
            this.repliesIndicator.Size = new System.Drawing.Size(95, 15);
            this.repliesIndicator.TabIndex = 6;
            this.repliesIndicator.TabStop = true;
            this.repliesIndicator.Text = "Show Replies";
            this.repliesIndicator.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.repliesIndicator_LinkClicked);
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
            // postDateLabel
            // 
            this.postDateLabel.AutoSize = true;
            this.postDateLabel.Location = new System.Drawing.Point(7, 68);
            this.postDateLabel.Name = "postDateLabel";
            this.postDateLabel.Size = new System.Drawing.Size(33, 13);
            this.postDateLabel.TabIndex = 8;
            this.postDateLabel.Text = "Date:";
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
            this.repliesGrid.Size = new System.Drawing.Size(474, 115);
            this.repliesGrid.TabIndex = 3;
            // 
            // addReply
            // 
            this.addReply.AutoSize = true;
            this.addReply.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addReply.Location = new System.Drawing.Point(196, 203);
            this.addReply.Name = "addReply";
            this.addReply.Size = new System.Drawing.Size(71, 15);
            this.addReply.TabIndex = 9;
            this.addReply.TabStop = true;
            this.addReply.Text = "Add Reply";
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
            ((System.ComponentModel.ISupportInitialize)(this.subforumsGrid)).EndInit();
            this.postPanel.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.postsGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repliesGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
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
    }
}