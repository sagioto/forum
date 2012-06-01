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

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ClientController controller;
        SubforumsList subforumsList;
        private PostControlList postsControlsList;
        List<Post> postsList;
       // Thread getPostsWorkerThread;

        //GUI parameters:
        private Window darkwindow;

        LoginWindow loginWin;
        private string currentSubforum;


        public MainWindow()
        {
            InitializeComponent();
            loginWin = new LoginWindow();
            loginWin.loggedIn += new LoginWindow.LoginEventHandler(loginWin_loggedIn);
            loginWin.registered += new LoginWindow.LoginEventHandler(loginWin_registered);
            loginWin.cancelled += new LoginWindow.LoginEventHandler(loginWin_cancelled);

            controller = new ClientController(true);
            // controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer);

            subforumsList = new SubforumsList();
            //Thread newThread = new Thread(GetSubforums);
            //newThread.Start();
            GetSubforums();

            //GUI:
            darkwindow = new Window()
            {
                Background = Brushes.Black,
                Opacity = 0.4,
                AllowsTransparency = true,
                WindowStyle = WindowStyle.None,
                WindowState = WindowState.Maximized,
                Topmost = true
            };

            //getPostsWorkerThread = new Thread(getSubforum);
            //getPostsWorkerThread.SetApartmentState(ApartmentState.STA);


            StaticObjects.postWindow = new PostWindow();

            postsControlsList = new PostControlList();
            postsListBox.DataContext = postsControlsList;

            subforumsListBox.ItemsSource = subforumsList;
            
            // treeView1.DataContext = postsControlsList;
            // treeView1.ItemsSource = postsControlsList;

        }

        #region Worker Threads Method:

        private void getSubforum()
        {
            List<Post> postsList = controller.GetSubforum(currentSubforum).ToList<Post>();
            foreach (Post p in postsList)
            {
                postsControlsList.Add(new PostControl(p));
            }
        }



        #endregion

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            loginWin.setTitle("Register");

            darkwindow.Show();
            loginWin.Show();
        }



        private void GetSubforums()
        {
            string[] sl = controller.GetSubforumsList();
            foreach (string s in sl)
            {
                subforumsList.Add(s);
            }
            
            //     DataTable dt = ArrayToTable(subforumsList);

            //subforumsListBox.DataContext = subforumsList.to;

            currentSubforum = subforumsList[0];


        }
        #region Register/Login
       
        public void loginWin_loggedIn()
        {
            darkwindow.Hide();
            try
            {
                bool res = controller.Login(loginWin.Username, loginWin.Password);
                if (!res)
                {
                    MessageBox.Show("Bad username or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
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
            darkwindow.Hide();
            try
            {
                Result res = controller.Register(loginWin.Username, loginWin.Password);
                if (res != Result.OK)
                {
                    MessageBox.Show("Username is already exists. Details: " + res.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    helloLabel.Content = "Hello " + loginWin.Username;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void loginWin_cancelled()
        {
            darkwindow.Hide();
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {
            loginWin.setTitle("Login");

            darkwindow.Show();
            loginWin.Show();
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool res = controller.Logout();
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


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            GetSubforums();
            subforumsListBox.Visibility = System.Windows.Visibility.Visible;
            // postsDataGrid.Visibility = System.Windows.Visibility.Hidden;
            currentSubforumLabel.Visibility = System.Windows.Visibility.Hidden;
        }

        private void subforumsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSubforum = e.AddedItems[0].ToString();
            postsControlsList.Clear();
            //Thread getPostsWorkerThread = new Thread(getSubforum);
            //getPostsWorkerThread.SetApartmentState(ApartmentState.STA);
            //getPostsWorkerThread.Start();

            getSubforum();
            
            
            

            currentSubforumLabel.Content = "Subforum: " + currentSubforum;
            currentSubforumLabel.Visibility = System.Windows.Visibility.Visible;
           

            //TODO - Animation for subforum list
            subforumsListBox.Visibility = System.Windows.Visibility.Hidden;
            postsListBox.Visibility = System.Windows.Visibility.Visible;
            // postsDataGrid.Visibility = System.Windows.Visibility.Visible;
           // DataTable dt = ListToTable(controller.GetSubforum(currentSubforum).ToList<Post>());
            //  postsDataGrid.ItemsSource = dt.DefaultView;
        }

        private DataTable ListToTable(List<Post> postsList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            foreach (Post p in postsList)
            {
                dt.Rows.Add("* ", p.Title, p.Key.Username, p.Key.Time.ToShortDateString());
            }
            return dt;
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            postsControlsList.Add(new PostControl(null));
        }


    }
}
