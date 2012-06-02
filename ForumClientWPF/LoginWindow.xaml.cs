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

namespace ForumClientWPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private string username;
        private string password;

        public delegate void LoginEventHandler();
        public event LoginEventHandler loggedIn;
        public event LoginEventHandler registered;
        public event LoginEventHandler cancelled;

        public LoginWindow()
        {
            InitializeComponent();

        }

        public void setTitle(string title)
        {
            titleLabel.Content = title;
            usernameTextBox.Text = "";
            passwordTextBox.Password = "";
            usernameTextBox.Focus();
        }

        private void goButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "" || passwordTextBox.Password == "")
            {
                MessageBox.Show("All fields are required", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                username = usernameTextBox.Text;
                password = passwordTextBox.Password;
                this.Hide();
                if (titleLabel.Content.Equals("Register"))
                {
                    registered();
                }
                else
                {
                    loggedIn();
                }


            }

        }


        public string Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            cancelled();
            this.Hide();
        }


    }
}
