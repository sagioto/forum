using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore;
using System.Text.RegularExpressions;

namespace ForumClientConsole
{
    class ClientConsole
    {
        ClientController controller;

        // This code is for opening Console
        // TODO - we need to try finding an more elegant way to alloc & release console
        //[DllImport("kernel32.dll")]
        //public static extern Boolean AllocConsole();
        //[DllImport("kernel32.dll")]
        //public static extern Boolean FreeConsole();

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientConsole()
        {
            controller = new ClientController();
            controller.OnUpdateFromController += new ForumClientCore.NetworkLayer.ClientNetworkAdaptor.OnUpdate(controller_OnUpdateFromServer);
        }

        /// <summary>
        /// Menu method
        /// </summary>
        public void startMenu()
        {
            //AllocConsole(); 
            InitConsole();

            Console.WriteLine("Hello, Guest!");
            Console.WriteLine("What would you like to do? (Type menu at any point for a list of available commands)");

            string line;

            while (true)
            {
                line = Console.ReadLine();

                string[] command = line.Split(' ');

                switch (command[0])
                {
                    case "menu":
                        Console.WriteLine("\nThe available commands are:");
                        Console.WriteLine("list-forums\nregister\nlogin\nlogout\npost\nquit\n");
                        break;
                    case "list-forums":
                        Console.WriteLine("Here is the list of forums:");
                        Console.WriteLine(controller.GetSubforumsList());
                        break;
                    case "register":
                        Register();
                        break;
                    case "login":
                        Login();
                        break;
                    case "logout":
                        Logout();
                        break;
                    case "post":
                        Post();  //TODO Need to support more args - change to Post method
                        break;
                    case "quit":
                        Logout();
                        freeConsole();
                        return;
                    default:
                        Console.WriteLine("Unrecognized command: " + line + ". You can type menu for a list of commands");
                        break;
                }
            }

        }

        #region Console Operations

        private void Post()
        {
            Console.WriteLine("Please enter a title to your post");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the name of the forum you want to post in");
            string subForum = Console.ReadLine();
            if (controller.Post(title, subForum))
            {
                Console.WriteLine("Posted successfully!");
            }
            else
            {
                Console.WriteLine("Sorry, could not post. Are you logged in?");
            }
        }

        private void Logout()
        {
            if (controller.Logout())
            {
                Console.WriteLine("Logged out successfully.");
            }
            else
            {
                Console.WriteLine("Something happened and you could not be logged out. Please try again.");
            }
        }

        private void Login()
        {
            string userName = "";
            string password = "";
            Console.WriteLine("Please enter your User Name");
            userName = Console.ReadLine();
            while (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]+$"))
            {
                Console.WriteLine("User name should contain only letters, number, or an underscore. Enter again.");
                userName = Console.ReadLine();
            }
            Console.WriteLine("Please enter password for " + userName);
            password = ReadPassword();
            if (controller.Login(userName, password))
            {
                Console.WriteLine("Login Successful. Hello " + userName + "!");
            }
            else
            {
                Console.WriteLine("Login Failed. Please check your user name and/or password.");
            }
        }

        private void Register()
        {
            string userName = "";
            string password = "";
            Console.WriteLine("Please enter your desired User Name");
            userName = Console.ReadLine();
            while (!Regex.IsMatch(userName, @"^[a-zA-Z0-9_]+$"))
            {
                Console.WriteLine("User name should contain only letters, number, or an underscore. Enter again.");
                userName = Console.ReadLine();
            }
            Console.WriteLine("Please choose your password");
            password = ReadPassword();
            if (controller.Register(userName, password))
            {
                Console.WriteLine("Registration Successful. You can now login to your account");
            }
            else
            {
                Console.WriteLine("Registration Failed! Please try again...");
            }
        }

        private string ReadPassword()
        {
            string pass = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    pass += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                    {
                        pass = pass.Substring(0, (pass.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            } while (key.Key != ConsoleKey.Enter);
            Console.WriteLine();
            return pass;
        }

        #endregion

        /// <summary>
        /// This method is called when controller invoked its OnUpdate event
        /// </summary>
        /// <param name="text"></param>
        public void controller_OnUpdateFromServer(string text)
        {
            try
            {
                Console.Beep();
                Console.WriteLine(text);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Must to be called when quiting console
        /// </summary>
        public void freeConsole()
        {
            //FreeConsole();
        }


        private static void InitConsole()
        {
            Console.Title = "Forum Client";
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WindowWidth = 100;
            Console.Beep();
            Console.BackgroundColor = ConsoleColor.Blue;
            StringBuilder sb = new StringBuilder();
            sb.Append("  $$\\     $$\\                        $$$$$$\\                                                       \n");
            sb.Append("  $$ |    $$ |                      $$  __$$\\                                                      \n");
            sb.Append("$$$$$$\\   $$$$$$$\\   $$$$$$\\        $$ /  \\__|$$$$$$\\   $$$$$$\\  $$\\   $$\\ $$$$$$\\$$$$\\            \n");
            sb.Append("\\_$$  _|  $$  __$$\\ $$  __$$\\       $$$$\\    $$  __$$\\ $$  __$$\\ $$ |  $$ |$$  _$$  _$$\\           \n");
            sb.Append("  $$ |    $$ |  $$ |$$$$$$$$ |      $$  _|   $$ /  $$ |$$ |  \\__|$$ |  $$ |$$ / $$ / $$ |          \n");
            sb.Append("  $$ |$$\\ $$ |  $$ |$$   ____|      $$ |     $$ |  $$ |$$ |      $$ |  $$ |$$ | $$ | $$ |          \n");
            sb.Append("  \\$$$$  |$$ |  $$ |\\$$$$$$$\\       $$ |     \\$$$$$$  |$$ |      \\$$$$$$  |$$ | $$ | $$ |          \n");
            sb.Append("   \\____/ \\__|  \\__| \\_______|      \\__|      \\______/ \\__|       \\______/ \\__| \\__| \\__|          \n");
            Console.WriteLine(sb);
        }

    }
}
