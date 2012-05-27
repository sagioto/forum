using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore;
using System.Text.RegularExpressions;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

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
            controller = new ClientController(false);
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

                char[] delimeters = { ' ' };
                string[] command = line.Split(delimeters, 2);

                switch (command[0])
                {
                    case "menu":
                        Console.WriteLine("\nThe available commands are:");
                        Console.WriteLine("\n\tlist-forums\n\tshow-forum [forum name]\n\tshow-replies [post title]\n\tback\n\tedit [post title]\n\tregister\n\tlogin\n\tlogout\n\tpost\n\tremove [post title]\n\tquit\n\tadmin-menu\n");
                        break;
                    case "admin-menu": // TODO add report commands
                        Console.WriteLine("\n\tadd-moderator\n\tremove-moderator\n\treplace-moderator\n\treplace-admin\n\tadd-forum\n\tremove-forum\n\t");
                        break;
                    case "remove":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Usage: remove [post title]");
                            break;
                        }
                        RemovePost(command);
                        PrintCurrentLocation();
                        break;
                    case "edit":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Usage: edit [post title]");
                            break;
                        }
                        EditPost(command);
                        break;
                    case "back":
                        Back();
                        break;
                    case "list-forums":
                        Console.WriteLine("Here is the list of sub-forums:");
                        int i = 0;
                        foreach (String subforum in controller.GetSubforumsList())
                        {
                            Console.WriteLine(i + ") " + subforum);
                            i++;
                        }
                        break;
                    case "show-replies":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Usage: show-replies [post title]");
                            break;
                        }
                        ShowReplies(command);
                        break;
                    case "reply":
                        Reply();
                        break;
                    case "show-forum":
                        if (command.Length < 2)
                        {
                            Console.WriteLine("Usage: show [forum name]");
                            break;
                        }
                        GetSubforum(command[1]);
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
                        Post(); //post the message on the current subforum
                        GetSubforum(currentSubForum); //reload the subforum post list
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

        private void PrintCurrentLocation()
        {
            //Should print the current Post/Subforum.
            throw new NotImplementedException();
        }

        private void RemovePost(string[] command)
        {
            Post[] posts;
            if (currentPost != null)
            {
                posts = controller.GetReplies(currentPost.Key);
            }
            else if (currentSubForum != null)
            {
                posts = controller.GetSubforum(currentSubForum);
            }
            else
            {
                Console.WriteLine("Please enter a subforum or a post first.");
                return;
            }
            foreach (Post p in posts)
            {
                if (p.Title.Equals(command[1]))
                {
                    try
                    {
                        Result r = controller.RemovePost(p.Key);
                        if (r == Result.OK)
                        {
                            Console.WriteLine("Post was removed successfully");
                        }
                        else
                        {
                            Console.WriteLine("Post could not be removed. Sorry. Got response: " + r.ToString());
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry, can't get the post right now...");
                    }
                    break;
                }
            }
        }

        private void EditPost(string[] command)
        {
            Post[] posts;
            if (currentPost != null)
            {
                posts = controller.GetReplies(currentPost.Key);
            }
            else if (currentSubForum != null)
            {
                posts = controller.GetSubforum(currentSubForum);
            }
            else
            {
                Console.WriteLine("Please enter a subforum or a post first.");
                return;
            }
            foreach (Post p in posts)
            {
                if (p.Title.Equals(command[1]))
                {
                    try
                    {
                        Console.WriteLine("Please enter a new title to your post");
                        string title = Console.ReadLine();
                        Console.WriteLine("Enter the new body of your post");
                        string body = Console.ReadLine();
                        Result r = controller.EditPost(p.Key, title, body);
                        if (r == Result.OK)
                        {
                            Console.WriteLine("Post was edited successfully");
                        }
                        else
                        {
                            Console.WriteLine("Post could not be edited. Sorry.");
                        }

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry, can't get the post right now...");
                    }
                    break;
                }
            }
        }

        #region Console Operations

        private void Back() //TODO support currentPost navigation!!!! CRITICAL!
        {
            if (currentPost != null)
            {
                Post[] posts = controller.Back(currentPost.Key);
                PrintPostList(posts);
            }
            else if (currentSubForum != null)
            {
                Post[] posts = controller.GetSubforum(currentSubForum);
                PrintPostList(posts);
            }
            else
            {
                
            }
        }

        private void ShowReplies(string[] command)
        {
            Post[] posts;
            if (currentPost != null)
            {
                posts = controller.GetReplies(currentPost.Key);
            }
            else
            {
                posts = controller.GetSubforum(currentSubForum);
            }
            foreach (Post p in posts)
            {
                if (p.Title.Equals(command[1]))
                {
                    try
                    {
                        Post[] replies = controller.GetReplies(p.Key);
                        PrintPostList(replies);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Sorry, can't get the post right now...");
                    }
                    break;
                }
            }
        }

        private void GetSubforum(string subforumname)
        {
            Post[] subForumPosts = controller.GetSubforum(subforumname);
            if (subForumPosts == null)
            {
                Console.Write("The subforum you requested was not found.");
            }
            else
            {
                currentSubForum = subforumname;
                PrintPostList(subForumPosts);
            }
        }

        private void PrintPostList(Post[] subForumPosts)
        {
            if (subForumPosts != null)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Post p in subForumPosts)
                {
                    sb.Append("***********************************************************************************************\n");
                    sb.Append("*                                                                                             *\n");
                    string empty = "*                                                                                             *\n";
                    string title = empty.Substring(0, empty.Length / 2 - p.Title.Length / 2) + p.Title + empty.Substring((empty.Length / 2) + (p.Title.Length / 2) + 1);
                    sb.Append(title);
                    sb.Append("*                                                                                             *\n");
                    sb.Append("*---------------------------------------------------------------------------------------------*\n");
                    sb.Append("*                                                                                             *\n");
                    string body = empty.Substring(0, empty.Length / 2 - p.Body.Length / 2) + p.Body + empty.Substring((empty.Length / 2) + (p.Body.Length / 2) + 1);
                    sb.Append(body);
                    sb.Append("*                                                                                             *\n");
                }
                sb.Append("***********************************************************************************************\n");
                Console.WriteLine(sb);
            }
        }

        private void Reply()
        {
            Console.WriteLine("Please enter a title to your post");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the body of your post");
            string body = Console.ReadLine();
            if (currentPost == null)
            {
                Console.WriteLine("Sorry, you can only reply inside a sub-forum");
            }
            else
            {
                if (controller.Reply(currentPost.Key, title, body) == Result.OK)
                {
                    Console.WriteLine("Replied to the post successfully!");
                }
                else
                {
                    Console.WriteLine("Sorry, could not reply. Are you logged in?");
                }
            }
        }

        private void Post()
        {
            Console.WriteLine("You are writing a post in: " + currentSubForum);
            Console.WriteLine("Please enter a title to your post");
            string title = Console.ReadLine();
            Console.WriteLine("Enter the body of your post");
            string body = Console.ReadLine();
            try
            {
                Result r = controller.Post(currentSubForum, title, body);
                if (r == Result.OK)
                {
                    Console.WriteLine("Posted successfully!");
                }
                else if (r == Result.INSUFFICENT_PERMISSIONS)
                {
                    Console.WriteLine("Sorry, could not post. Are you logged in?");
                } 
                else if (r == Result.ENTRY_EXISTS)
                {
                    Console.WriteLine("The post already exists.");
                } 
                else 
                {
                    Console.WriteLine("Sorry, couldn't post your message. Got response from server: " + r.ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error! Got response from server: " + e.Message);
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
            Result r = controller.Register(userName, password);
            if (r == Result.OK)
            {
                Console.WriteLine("Registration Successful. You can now login to your account");
            }
            else if (r == Result.ENTRY_EXISTS)
            {
                Console.WriteLine("Registration Failed! The username already exists");
            }
            else
            {
                Console.WriteLine("Sorry, you could not be registered. message: " + r.ToString());
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
        public void controller_OnUpdateFromServer(Post p)
        {
            try
            {
                Console.Beep();
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


        public string currentSubForum { get; set; }

        public Post currentPost { get; set; }
    }
}
