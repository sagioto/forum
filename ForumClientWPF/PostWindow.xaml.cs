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

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for PostWindow.xaml
    /// </summary>
    public partial class PostWindow : Window
    {
        List<PostControl> repliesList;
        Post currentPost;

        public PostWindow()
        {
            InitializeComponent();
            repliesList = new List<PostControl>();
            //repliesList.Add(new PostControl(null));
            //repliesList.Add(new PostControl(null));
            //repliesList.Add(new PostControl(null));
            repliesListBox.DataContext = repliesList;
        }




        internal void setPost(Post currentPost)
        {
            
        }
    }
}
