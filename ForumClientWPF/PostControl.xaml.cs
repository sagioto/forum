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
using System.Windows.Navigation;
using System.Windows.Shapes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;
using System.Threading;

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for PostControl.xaml
    /// </summary>
    public partial class PostControl : UserControl
    {
        private bool isExpended;
        private Post currentPost;
        PostControlList repliesList;
      //  private List<PostControl> repliesList;

        public PostControl(Post p, bool isReply)
        {
            InitializeComponent();
            isExpended = false;
            currentPost = p;
            setGUI(isReply);
            isExpended = true;
            expandIndicator_MouseLeftButtonDown(null, null);
           // repliesList = new List<PostControl>();
           // repliesList.Add(new PostControl());
            repliesList = new PostControlList();

            StaticObjects.newPostWin.closed += new AddPostWin.LoginEventHandler(newPostWin_closed);
            //Thread worker = new Thread(getReplies);
            //worker.Start();
            
            //repliesListBox.DataContext = repliesListBox;
          //  currentHeight = 50;
          //  grid.Height = currentHeight;
          //  this.Height = currentHeight;
        }

        private void setGUI(bool isReply)
        {
            postTableTitle.Content = currentPost.Title;
            postTableUser.Content = currentPost.Key.Username;
            postTableDate.Content = currentPost.Key.Time.ToString();
            postTitleTextbox.Text = currentPost.Title;
            postContentTextBox.Text = currentPost.Body;
            datePostLabel.Content = currentPost.Key.Time.ToString();
            usernameLabel.Content = currentPost.Key.Username;
            if (isReply)
            {
                postTableTitle.Foreground = Brushes.DarkBlue;
            }
        }


        private void expandIndicator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
               {
            if (!isExpended)
            {
                expandIndicatorPlus.Visibility = System.Windows.Visibility.Hidden;
                expandIndicatorMinus.Visibility = System.Windows.Visibility.Visible;
                this.Height = 172;
                border1.Visibility = System.Windows.Visibility.Visible;
              //  grid.Height = Double.NaN;
              //  currentHeight = 300;
                isExpended = true;
                datePostLabel.Visibility = System.Windows.Visibility.Visible;
                usernameLabel.Visibility = System.Windows.Visibility.Visible;
                postTitleTextbox.Visibility = System.Windows.Visibility.Visible;
                postContentTextBox.Visibility = System.Windows.Visibility.Visible;
                contentLabel.Visibility = System.Windows.Visibility.Visible;
                subjectLabel.Visibility = System.Windows.Visibility.Visible;
                image1.Visibility = System.Windows.Visibility.Visible;
                // Hide label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Hidden;
                postTableTitle.Visibility = System.Windows.Visibility.Hidden;
                postTableUser.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                border1.Visibility = System.Windows.Visibility.Hidden;
                expandIndicatorPlus.Visibility = System.Windows.Visibility.Visible;
                expandIndicatorMinus.Visibility = System.Windows.Visibility.Hidden;
                this.Height = 50;
              //  grid.Height = 50;
                //currentHeight = 50;
                isExpended = false;
                datePostLabel.Visibility = System.Windows.Visibility.Hidden;
                usernameLabel.Visibility = System.Windows.Visibility.Hidden;
                postTitleTextbox.Visibility = System.Windows.Visibility.Hidden;
                postContentTextBox.Visibility = System.Windows.Visibility.Hidden;
                contentLabel.Visibility = System.Windows.Visibility.Hidden;
                subjectLabel.Visibility = System.Windows.Visibility.Hidden;
                image1.Visibility = System.Windows.Visibility.Hidden;
                // Show label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Visible;
                postTableTitle.Visibility = System.Windows.Visibility.Visible;
                postTableUser.Visibility = System.Windows.Visibility.Visible;
            }
        }


        private void editImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            postContentTextBox.IsReadOnly = false;
            postContentTextBox.Background = Brushes.LightGray;
            postTitleTextbox.IsReadOnly = false;
            postTitleTextbox.Background = Brushes.LightGray;
            saveImage.Visibility = System.Windows.Visibility.Visible;
            editImage.Visibility = System.Windows.Visibility.Hidden;
        }

        private void saveImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Result r = StaticObjects.controller.EditPost(currentPost.Key, postTitleTextbox.Text, postContentTextBox.Text);
            if (!(r == Result.OK))
            {
                MessageBox.Show("Error in edit post. Reason: " + r.ToString(), "Edit Post error", MessageBoxButton.OK, MessageBoxImage.Error);
                postContentTextBox.Text = currentPost.Body;
                postTitleTextbox.Text = currentPost.Title;
            }

            postContentTextBox.IsReadOnly = true;
            postContentTextBox.Background = Brushes.Transparent;
            postTitleTextbox.IsReadOnly = true;
            postTitleTextbox.Background = Brushes.Transparent;
            saveImage.Visibility = System.Windows.Visibility.Hidden;
            editImage.Visibility = System.Windows.Visibility.Visible;
        }

        private void addReplyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                StaticObjects.newPostWin.setWinProperties(currentPost, currentPost.Subforum);
                StaticObjects.darkwindow.Show();
                StaticObjects.newPostWin.Show();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void newPostWin_closed()
        {
            StaticObjects.darkwindow.Hide();
        }

    }
}
