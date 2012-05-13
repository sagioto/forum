using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using ForumClientCore;
using Infragistics.Win.UltraWinTree;
using Infragistics.Win.UltraWinEditors;
using ForumUtils.SharedDataTypes;

namespace ForumClientGui
{
    public partial class ClientFormUI : Form
    {
        ClientController controller;
        Post currentPost;
        string currentSubforum;
        List<Post> currentSubforumPosts;
        List<string> subforumsList;
        Post updatedPost;
        List<Post> currentReplies;

        public ClientFormUI()
        {
            InitializeComponent();

            controller = new ClientController(true);
            controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer);

            currentPost = null;
            currentSubforumPosts = new List<Post>();

            postsGrid.Dock = DockStyle.Fill;
            subforumsGrid.Dock = DockStyle.Fill;
            postPanel.Dock = DockStyle.Fill;
            newPostPanel.Dock = DockStyle.Fill;


            GetSubforums();


        }

        private void GetSubforums()
        {
            subforumsList = controller.GetSubforumsList().ToList<string>();
            DataTable dt = ArrayToTable(subforumsList);
            subforumsGrid.DataSource = dt;
            subforumsComboBox.DataSource = subforumsList;
            currentSubforum = subforumsList[0];
        }

        /// <summary>
        /// Will be called when controller will invoke update event
        /// </summary>
        /// <param name="text"></param>
        public void controller_OnUpdateFromServer(Post postUpdated)
        {
            onUpdatePictureBox.Visible = true;
            updatedPost = postUpdated;
            //richTextBox1.Text = richTextBox1.Text + text + '\n';    // Update textBox with the message from server
        }

        /// <summary>
        /// When clicking button1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //Thread thread = new Thread(new ThreadStart(WorkThreadFunction));
                //thread.Start();
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public void WorkThreadFunction()
        {

            // controller.AddMessage(textBox1.Text);
        }

        private void ClientFormUI_Load(object sender, EventArgs e)
        {
            // DataTable dt = TempGetPosts();
            //subforumsGrid.DataSource = dt;


        }




        private void ShowPost(Post p)
        {
            currentPost = p;

            // Switch Panels
            postsGrid.Visible = false;
            subforumsGrid.Visible = false;
            postPanel.Visible = true;
            newPostPanel.Visible = false;

            // Update panel data:
            repliesIndicator.Visible = true;
            postTitleLabel.Text = p.Title;
            postUserLabel.Text = p.Key.Username;
            postDateLabel.Text = p.Key.Time.ToShortDateString();
            postBody.Text = p.Body;
            ShowRepliesIndicator(p.HasReplies);


        }


        private void ShowNewPost(string title, bool reply)
        {
            // Switch Panels
            postsGrid.Visible = false;
            subforumsGrid.Visible = false;
            postPanel.Visible = false;
            newPostPanel.Visible = true;

            // Update panel data:
            postBodyTextBox.Text = "";
            if (currentSubforum != null)
            {

                //List<string> ls = (List<string>)subforumsComboBox.DataSource;

                subforumsComboBox.SelectedItem = currentSubforum;
            }
            else
            {
                //subforumsComboBox.SelectedValue = 0;
            }

            if (reply)
            {
                subforumsComboBox.Enabled = false;
                postTitleTextBox.Text = "RE: " + title;
                replyToTitle.Visible = true;
                backPostLabel.Text = currentPost.Title;
            }
            else
            {
                subforumsComboBox.Enabled = true;
                postTitleTextBox.Text = title;
                replyToTitle.Visible = false;
                backPostLabel.Text = "Back";
            }
        }



        private void ShowRepliesIndicator(bool p)
        {
            repliesGrid.Visible = false;
            if (p)
            {
                repliesIndicator.Text = "Show Replies";
                repliesIndicator.Enabled = true;
            }
            else
            {
                repliesIndicator.Text = "No Replies";
                repliesIndicator.Enabled = false;
            }
        }

        private void repliesIndicator_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentPost.HasReplies)
            {
                repliesIndicator.Text = "Loading...";
                currentReplies = controller.GetReplies(currentPost.Key).ToList<Post>();
                repliesGrid.DataSource = ListToTable(currentReplies);

                repliesIndicator.Visible = false;
                repliesGrid.Visible = true;
            }
        }

        private static List<Post> TempGetPosts()
        {
            List<Post> pl = new List<Post>();
            for (int i = 0 ; i < 20 ; i++)
            {
                //pl.Add(new Post(new Postkey("dor", DateTime.Now), "Post " + i, null, "subforum"));
            }
            return pl;
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (currentPost.ParentPost != null)
            {
                ShowPost(currentPost);
            }
            else    //Return to posts list
            {
                ShowPostsGrid(currentPost.Subforum);
            }
        }

        private void ShowPostsGrid(string subforum)
        {
            // Switch Panels
            postsGrid.Visible = true;
            postsGrid.BringToFront();
            subforumsGrid.Visible = false;
            postPanel.Visible = false;
            newPostPanel.Visible = false;

            DisplayLoading(true);
            currentSubforumPosts = controller.GetSubforum(subforum).ToList<Post>();
            postsGrid.DataSource = ListToTable(currentSubforumPosts);
            DisplayLoading(false);

        }

        private DataTable ListToTable(List<Post> postsList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            foreach (Post p in postsList)
            {
                dt.Rows.Add(p.Title, p.Key.Username, p.Key.Time.ToShortDateString());
            }
            return dt;
        }

        private void ShowMainScreen()
        {
            // Switch Panels
            postsGrid.Visible = false;
            subforumsGrid.Visible = true;
            postPanel.Visible = false;
            newPostPanel.Visible = false;

            DisplayLoading(true);


            //subforumsGrid.DataSource = TempGetSubforums();    //TODO DELETE
            DisplayLoading(false);


        }

        private DataTable ArrayToTable(List<string> p)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();
            foreach (string s in p.ToList<string>())
            {
                dt.Rows.Add(s);
            }
            return dt;
        }


        private object TempGetSubforums()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Name2");
            dt.Columns.Add("Name3");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            dt.Rows.Add("lkdsnflksdmkflmsdlkmflkds", "dor", "10:10");
            return dt;
        }




        private void DisplayLoading(bool p)
        {
            //TODO

        }

        private void ClientFormUI_Load_1(object sender, EventArgs e)
        {
            ShowMainScreen();
        }

        private void postsGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowPost(currentSubforumPosts[e.RowIndex]);
        }

        private void subforumsGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            currentSubforum = subforumsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            ShowPostsGrid(currentSubforum);
        }

        private void label1_Click(object sender, EventArgs e)
        {
            ShowMainScreen();
        }

        private void addReply_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowNewPost(currentPost.Title, true);
        }

        private void backPostLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (replyToTitle.Visible == true)
            {
                ShowPost(currentPost);
            }
            else
            {
                ShowMainScreen();
            }
        }

        private void sendPostButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (postTitleTextBox.Text != "")
            {
                BackgroundWorker t = new BackgroundWorker();
                t.DoWork += new DoWorkEventHandler(t_DoWork);
                t.RunWorkerCompleted += new RunWorkerCompletedEventHandler(t_RunWorkerCompleted);
                PostSend ps = new PostSend(subforumsComboBox.Text, postTitleTextBox.Text, postBodyTextBox.Text);
                t.RunWorkerAsync(ps);
                //Thread SendPost = new Thread(unused => SendPost(postTitleTextBox.Text, postBodyTextBox.Text));
            }
            else
            {
                MessageBox.Show("Title is a required fields!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }



        private void t_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                PostSend ps = e.Argument as PostSend;
                if (replyToTitle.Visible == false)
                {
                    //controller.Post(currentSubforum,postTitleTextBox.Text, postBodyTextBox.Text)
                    e.Result = controller.Post(ps.Subforum, ps.Title, ps.Body);
                }
                else
                {
                    e.Result = controller.Reply(currentPost.Key,  ps.Title, ps.Body);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw;
            }
        }


        private void t_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool res = (bool)e.Result;
            if (res)
            {
                backPostLabel_LinkClicked(null, null);
            }
            else
            {
                MessageBox.Show("Only logged in users are allowed to add posts !", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowNewPost("", false);
        }

        private void registerLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                bool res = controller.Register(usernameTextBox.Text, passTextBox.Text);
                if (!res)
                {
                    MessageBox.Show("Username is already registered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    linkLabel1_LinkClicked(null, null);
                    loginPanel.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                bool res = controller.Login(usernameTextBox.Text, passTextBox.Text);
                if (!res)
                {
                    MessageBox.Show("Bad username or password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    loggedInTitle.Text = "Logged in as: " + usernameTextBox.Text;
                    loginPanel.Visible = false;
                    usernameTextBox.Text = "";
                    passTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                bool res = controller.Logout();
                if (!res)
                {
                    MessageBox.Show("Username is not registered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    loginPanel.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void onUpdatePictureBox_Click(object sender, EventArgs e)
        {
            if (subforumsGrid.Visible == true)
            {
                GetSubforums();
                ShowMainScreen();
            }
            if (postsGrid.Visible == true && updatedPost.Subforum == currentSubforum)
            {
                ShowPostsGrid(currentSubforum);
            }
            onUpdatePictureBox.Visible = false;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            ShowNewPost("", false);
        }

        private void repliesGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (currentReplies != null)
            {
                //List<Post> l = currentPost.Replies.Values.ToList();
                ShowPost(currentReplies[e.RowIndex]);
            }
        }
    }
}
