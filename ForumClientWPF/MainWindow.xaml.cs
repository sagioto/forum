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
using ForumClientCore;
using ForumShared.ForumAPI;
using System.Threading;
using System.Windows.Media.Animation;
using ForumShared.SharedDataTypes;
using System.Data;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SubforumsList subforumsList;
        private PostControlList postsControlsList;
        private BackgroundWorker subscriberThread;
        private string currentUser;

       // Thread getPostsWorkerThread;

        private LoginWindow loginWin;
        private string currentSubforum;
        private string updatedSubforum;


        public MainWindow()
        {
            InitializeComponent();
            loginWin = new LoginWindow();
            loginWin.loggedIn += new LoginWindow.LoginEventHandler(loginWin_loggedIn);
            loginWin.registered += new LoginWindow.LoginEventHandler(loginWin_registered);
            loginWin.cancelled += new LoginWindow.LoginEventHandler(loginWin_cancelled);

            StaticObjects.controller = new ClientController(true);
            // controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer);

            subforumsList = new SubforumsList();
            //Thread newThread = new Thread(GetSubforums);
            //newThread.Start();
            GetSubforums();

            //GUI:
            StaticObjects.darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Topmost = true
            };
            StaticObjects.newPostWin = new AddPostWin();
            StaticObjects.newPostWin.closed += new AddPostWin.LoginEventHandler(loginWin_cancelled);
            
            currentUser = "guest";

            postsControlsList = new PostControlList();
            subforumsComboBox.ItemsSource = subforumsList;

            subscriberThread = new BackgroundWorker();
            subscriberThread.DoWork += new DoWorkEventHandler(subscriberThread_DoWork);
            subscriberThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(subscriberThread_RunWorkerCompleted);
            subscriberThread.RunWorkerAsync();

        }

        #region Worker Threads Method:

        private void getSubforum()
        {
            List<Post> postsList = StaticObjects.controller.GetSubforum(currentSubforum).ToList<Post>();
            foreach (Post p in postsList)
            {
                PostControl pc = new PostControl(p, false);
                TreeViewItem ti = new TreeViewItem();
                ti.Header = pc;
                ti.IsExpanded = true;
                if (p.HasReplies)
                {
                    getReplies(p, ti);
                }
               postsTreeView.Items.Add(ti);
               // postsControlsList.Add(pc);
            }
        }

        private static void getReplies(Post p, TreeViewItem ti)
        {
            // Get replies from server
            Post[] replies = StaticObjects.controller.GetReplies(p.Key);
            // Add replies as children
            foreach (Post reply in replies)
            {
                TreeViewItem child = new TreeViewItem();
                child.Header = new PostControl(reply, true);
                getReplies(reply, child);
                ti.Items.Add(child);
            }
        }

        private void subscriberThread_DoWork(object sender, DoWorkEventArgs e)
        {
            ClientController controller = new ClientController();
            e.Result = controller.Subscribe();
        }

        private void subscriberThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Post p = (Post)e.Result;
            if (p != null)
            {
                updatedSubforum = p.Subforum;
                displayNotify(p.Subforum, p.Title);
                if (p.Subforum == currentSubforum)
                {
                    postsTreeView.Items.Clear();
                    getSubforum();
                }
            }
            subscriberThread.RunWorkerAsync();
        }

        private void displayNotify(string subforum, string postTitle)
        {
            notifyImage.ToolTip = "New update: post '" + postTitle + "', in subforum: " + subforum;
            notifyImage.Visibility = System.Windows.Visibility.Visible;
        }

        #endregion

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            loginWin.setTitle("Register");

            StaticObjects.darkwindow.Show();
            loginWin.Show();
        }



        private void GetSubforums()
        {
            string[] sl = StaticObjects.controller.GetSubforumsList();
            foreach (string s in sl)
            {
                subforumsList.Add(s);
            }
            
            currentSubforum = subforumsList[0];


        }
        #region Register/Login
       
        public void loginWin_loggedIn()
        {
            StaticObjects.darkwindow.Hide();
            try
            {
                bool res = StaticObjects.controller.Login(loginWin.Username, loginWin.Password);
                if (!res)
                {
                    MessageBox.Show("Bad username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    currentUser = loginWin.Username;
                    helloLabel.Content = "Hello " + loginWin.Username;
                    //TODO Add enable logout button
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            //loginWin.Username, loginWin.Password;
        }

        public void loginWin_registered()
        {
            StaticObjects.darkwindow.Hide();
            try
            {
                Result res = StaticObjects.controller.Register(loginWin.Username, loginWin.Password);
                if (res != Result.OK)
                {
                    MessageBox.Show("Username is already exists. Details: " + res.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    currentUser = loginWin.Username;
                  //  helloLabel.Content = "Hello " + loginWin.Username;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void loginWin_cancelled()
        {
            StaticObjects.darkwindow.Hide();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginWin.setTitle("Login");

            StaticObjects.darkwindow.Show();
            loginWin.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool res = StaticObjects.controller.Logout();
                if (!res)
                {
                    MessageBox.Show("Username is not registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    helloLabel.Content = "Hello guest";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion

        private void newPostImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                StaticObjects.newPostWin.setWinProperties(null, currentSubforum);
                StaticObjects.darkwindow.Show();
                StaticObjects.newPostWin.Show();
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void subforumsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            mainLabel.Visibility = System.Windows.Visibility.Hidden;
            currentSubforum = e.AddedItems[0].ToString();
            postsTreeView.Items.Clear();
            //Thread getPostsWorkerThread = new Thread(getSubforum);
            //getPostsWorkerThread.SetApartmentState(ApartmentState.STA);
            //getPostsWorkerThread.Start();

            getSubforum();
            postsTreeView.Visibility = System.Windows.Visibility.Visible;
        }

        private void registerButton_Click(object sender, MouseButtonEventArgs e)
        {
            loginWin.setTitle("Register");

            StaticObjects.darkwindow.Show();
            loginWin.Show();
        }

        private void loginImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            loginWin.setTitle("Login");

            StaticObjects.darkwindow.Show();
            loginWin.Show();
        }

        private void logoutImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool res = StaticObjects.controller.Logout();
                if (!res)
                {
                    MessageBox.Show("Username is not registered", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    helloLabel.Content = "Hello guest";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void notifyImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            notifyImage.Visibility = System.Windows.Visibility.Hidden;
            if (currentSubforum != updatedSubforum)
            {
                postsTreeView.Items.Clear();
                subforumsComboBox.SelectedValue = updatedSubforum;
            }
        }






    }
}
