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

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for PostControl.xaml
    /// </summary>
    public partial class PostControl : UserControl
    {
        private bool isExpended;
        private PostWindow postWin;
        private Post currentPost;
      //  private List<PostControl> repliesList;

        public PostControl(Post p)
        {
            InitializeComponent();
            this.Height = 50;
            isExpended = false;
            postWin = StaticObjects.postWindow;
            currentPost = p;
            //setGUI();
           // repliesList = new List<PostControl>();
           // repliesList.Add(new PostControl());

            //repliesListBox.DataContext = repliesListBox;
        }


        private void setGUI()
        {
            postTableTitle.Content = currentPost.Title;
            postTableUser.Content = currentPost.Key.Username;
            postTableDate.Content = currentPost.Key.Time.ToString();
            postTitleLabel.Content = currentPost.Title;
            postContentLabel.Content = currentPost.Body;
            datePostLabel.Content = currentPost.Key.Time.ToString();
            usernameLabel.Content = currentPost.Key.Username;

        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!isExpended)
            {
                this.Height =350;
                isExpended = true;
                datePostLabel.Visibility = System.Windows.Visibility.Visible;
                usernameLabel.Visibility = System.Windows.Visibility.Visible;
                postTitleLabel.Visibility = System.Windows.Visibility.Visible;
                postContentLabel.Visibility = System.Windows.Visibility.Visible;
                contentLabel.Visibility = System.Windows.Visibility.Visible;
                subjectLabel.Visibility = System.Windows.Visibility.Visible;

                // Hide label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Hidden;
                postTableTitle.Visibility = System.Windows.Visibility.Hidden;
                postTableUser.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                this.Height = 50;
                isExpended = false;
                datePostLabel.Visibility = System.Windows.Visibility.Hidden;
                usernameLabel.Visibility = System.Windows.Visibility.Hidden;
                postTitleLabel.Visibility = System.Windows.Visibility.Hidden;
                postContentLabel.Visibility = System.Windows.Visibility.Hidden;
                contentLabel.Visibility = System.Windows.Visibility.Hidden;
                subjectLabel.Visibility = System.Windows.Visibility.Hidden;

                // Show label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Visible;
                postTableTitle.Visibility = System.Windows.Visibility.Visible;
                postTableUser.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            postWin.setPost(currentPost);
            //if (!postWin.ShowActivated)
            //{
                postWin.Show();
            //}
            
        }

        private void expandIndicator_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {
            //if (!isExpended)
            //{
            //    expandIndicatorPlus.Visibility = System.Windows.Visibility.Hidden;
            //    expandIndicatorMinus.Visibility = System.Windows.Visibility.Visible;
            //    this.Height = 350;
            //    isExpended = true;
            //    datePostLabel.Visibility = System.Windows.Visibility.Visible;
            //    usernameLabel.Visibility = System.Windows.Visibility.Visible;
            //    postTitleLabel.Visibility = System.Windows.Visibility.Visible;
            //    postContentLabel.Visibility = System.Windows.Visibility.Visible;
            //    contentLabel.Visibility = System.Windows.Visibility.Visible;
            //    subjectLabel.Visibility = System.Windows.Visibility.Visible;

            //    // Hide label of shrunken status:
            //    postTableDate.Visibility = System.Windows.Visibility.Hidden;
            //    postTableTitle.Visibility = System.Windows.Visibility.Hidden;
            //    postTableUser.Visibility = System.Windows.Visibility.Hidden;
            //}
            //else
            //{
            //    expandIndicatorPlus.Visibility = System.Windows.Visibility.Hidden;
            //    expandIndicatorMinus.Visibility = System.Windows.Visibility.Visible;
            //    this.Height = 50;
            //    isExpended = false;
            //    datePostLabel.Visibility = System.Windows.Visibility.Hidden;
            //    usernameLabel.Visibility = System.Windows.Visibility.Hidden;
            //    postTitleLabel.Visibility = System.Windows.Visibility.Hidden;
            //    postContentLabel.Visibility = System.Windows.Visibility.Hidden;
            //    contentLabel.Visibility = System.Windows.Visibility.Hidden;
            //    subjectLabel.Visibility = System.Windows.Visibility.Hidden;

            //    // Show label of shrunken status:
            //    postTableDate.Visibility = System.Windows.Visibility.Visible;
            //    postTableTitle.Visibility = System.Windows.Visibility.Visible;
            //    postTableUser.Visibility = System.Windows.Visibility.Visible;
            //}
        }

        private void expandIndicator_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
               {
            if (!isExpended)
            {
                expandIndicatorPlus.Visibility = System.Windows.Visibility.Hidden;
                expandIndicatorMinus.Visibility = System.Windows.Visibility.Visible;
                this.Height = 350;
                isExpended = true;
                datePostLabel.Visibility = System.Windows.Visibility.Visible;
                usernameLabel.Visibility = System.Windows.Visibility.Visible;
                postTitleLabel.Visibility = System.Windows.Visibility.Visible;
                postContentLabel.Visibility = System.Windows.Visibility.Visible;
                contentLabel.Visibility = System.Windows.Visibility.Visible;
                subjectLabel.Visibility = System.Windows.Visibility.Visible;

                // Hide label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Hidden;
                postTableTitle.Visibility = System.Windows.Visibility.Hidden;
                postTableUser.Visibility = System.Windows.Visibility.Hidden;
            }
            else
            {
                expandIndicatorPlus.Visibility = System.Windows.Visibility.Visible;
                expandIndicatorMinus.Visibility = System.Windows.Visibility.Hidden;
                this.Height = 50;
                isExpended = false;
                datePostLabel.Visibility = System.Windows.Visibility.Hidden;
                usernameLabel.Visibility = System.Windows.Visibility.Hidden;
                postTitleLabel.Visibility = System.Windows.Visibility.Hidden;
                postContentLabel.Visibility = System.Windows.Visibility.Hidden;
                contentLabel.Visibility = System.Windows.Visibility.Hidden;
                subjectLabel.Visibility = System.Windows.Visibility.Hidden;

                // Show label of shrunken status:
                postTableDate.Visibility = System.Windows.Visibility.Visible;
                postTableTitle.Visibility = System.Windows.Visibility.Visible;
                postTableUser.Visibility = System.Windows.Visibility.Visible;
            }
        }





    }
}
