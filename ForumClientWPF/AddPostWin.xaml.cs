using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for AddPostWin.xaml
    /// </summary>
    public partial class AddPostWin : Window
    {
        private Post currentPost = null;
        private string currentSubforum;

        public delegate void LoginEventHandler();
        public event LoginEventHandler cancelled;
        public event LoginEventHandler posted;

        public AddPostWin()
        {
            InitializeComponent();
        }

        public void setWinProperties(Post parentPost, string subforum)
        {
            currentPost = parentPost;
            if (currentPost != null)
            {
                postTitleTextbox.Text = "RE:" + currentPost.Title;
            }
            else
            {
                postTitleTextbox.Text = "";
            }
            postContentTextBox.Text = "";
            currentSubforum = subforum;
            currentSubforumLabel.Content = currentSubforum;
        }

        private void image1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {

                if (postTitleTextbox.Text != "")
                {
                    Result r; 
                    if (currentPost != null) // This is a reply
                    {
                        r = StaticObjects.controller.Reply(currentPost.Key, postTitleTextbox.Text, postContentTextBox.Text);
                        posted();
                    }
                    else //New post
                    {
                        r = StaticObjects.controller.Post(currentSubforum, postTitleTextbox.Text, postContentTextBox.Text);
                        posted();
                    }
                    if (r != Result.OK)
                    {
                        MessageBox.Show("Error in posting a new post. Reason: " + r.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        cancelled();
                    }
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Title is required !", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred. Reason: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                cancelled();
                this.Hide();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            cancelled();
            this.Hide();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            cancelled();
            this.Hide();
        }

        private void cancelImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            cancelled();
            this.Hide();
        }
    }
}
