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

        public ClientFormUI()
        {
            InitializeComponent();

            currentPost = null;
            currentSubforumPosts = new List<Post>();

            postsGrid.Dock = DockStyle.Fill;
            subforumsGrid.Dock = DockStyle.Fill;
            postPanel.Dock = DockStyle.Fill;

            // controller = new ClientController();
            // controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer); ;
        }

        /// <summary>
        /// Will be called when controller will invoke update event
        /// </summary>
        /// <param name="text"></param>
        public void controller_OnUpdateFromServer(string text)
        {
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

        

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            ShowPostsGrid(subforumsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }


        private void ShowPost(Post p)
        {
            currentPost = p;

            // Switch Panels
            postsGrid.Visible = false;
            subforumsGrid.Visible = false;
            postPanel.Visible = true;

            // Update panel data:
            repliesIndicator.Visible = true;
            postTitleLabel.Text = p.Title;
            postUserLabel.Text = p.Key.Username;
            postDateLabel.Text = p.Key.Time.ToShortDateString();
            postBody.Text = p.Body;
            ShowRepliesIndicator(p.HasReplies);
            

        }



        private void ShowRepliesIndicator(bool p)
        {
            if (p)
            {
                repliesIndicator.Text = "Show Replies";
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
                
                //controller.getReplies(currentPost);
                repliesGrid.DataSource = TempGetPosts();    //TODO DELETE
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

            DisplayLoading(true);
            //currentSubforumPosts = controller.getPostsOfSubforum(subforum);
            currentSubforumPosts = TempGetPosts();
            postsGrid.DataSource = ListToTable(TempGetPosts());    //TODO DELETE
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

            DisplayLoading(true);
            //controller.getAllSubforum(currentPost);
            subforumsGrid.DataSource = TempGetSubforums();    //TODO DELETE
            DisplayLoading(false);


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
            //TODO
            
            //ShowPost(subforumsGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
        }
    }
}
