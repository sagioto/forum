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
                        Console.WriteLine("\n\tlist-forums\n\tshow-forum [forum name]\n\tshow-replies [post title]\n\tback\n\trefresh\n\tedit [post title]\n\tregister\n\tlogin\n\tlogout\n\tpost\n\tremove [post title]\n\tquit\n\tadmin-menu\n");
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
                        PrintCurrentLocation();
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
                    case "add-moderator":
                        AddModerator();
                        break;
                    case "remove-moderator":
                        RemoveModerator();
                        break;
                    case "replace-moderator":
                        ReplaceModerator();
                        break;
                    case "replace-admin":
                        ReplaceAdmin();
                        break;
                    case "add-forum":
                        AddForum();
                        break;
                    case "remove-forum":
                        RemoveForum();
                        break;
                    case "refresh":
                        PrintCurrentLocation();
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

        private void RemoveForum()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the name of sub-forum you want to remove");
            string subforumName = Console.ReadLine();
            Result r = controller.RemoveSubforum(subforumName);
            if (r == Result.OK)
            {
                Console.WriteLine("Forum was removed successfully!");
            }
            else
            {
                Console.WriteLine("Could not remove the forum! Got response: " + r.ToString());
            }
        }

        private void AddForum()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the name of the new sub-forum");
            string subforumName = Console.ReadLine();
            Result r = controller.AddSubforum(subforumName);
            if (r == Result.OK)
            {
                Console.WriteLine("Forum was added successfully!");
            }
            else
            {
                Console.WriteLine("Could not add the forum! Got response: " + r.ToString());
            }
        }

        private void ReplaceAdmin()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the user name of the new admin");
            string newAdmin = Console.ReadLine();
            Console.WriteLine("Please enter the password of the new admin");
            string newPassword = Console.ReadLine();
            bool r = controller.ReplaceAdmin(newAdmin, newPassword);
            if (r)
            {
                Console.WriteLine("Admin replaced successfully!");
            }
            else
            {
                Console.WriteLine("Could not replace the admin! Got response: " + r.ToString());
            }
        }

        private void ReplaceModerator()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the user name of the subforum to replace in");
            string subforumName = Console.ReadLine();            
            Console.WriteLine("Please enter the name of the new moderator");
            string newMod = Console.ReadLine();
            Console.WriteLine("Please enter the name of the old moderator");
            string oldMod = Console.ReadLine();
            Result r = controller.ReplaceModerator(newMod, oldMod, subforumName);
            if (r == Result.OK)
            {
                Console.WriteLine("Moderators replaced successfully!");
            }
            else
            {
                Console.WriteLine("Could not replace the moderator! Got response: " + r.ToString());
            }
        }

        private void RemoveModerator()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the user name of the subforum to remove from");
            string subforumName = Console.ReadLine();
            Console.WriteLine("Please enter the name of the moderator to remove");
            string moderator = Console.ReadLine();
            Result r = controller.RemoveModerator(moderator, subforumName);
            if (r == Result.OK)
            {
                Console.WriteLine("Moderator removed successfully!");
            }
            else
            {
                Console.WriteLine("Could not remove the moderator! Got response: " + r.ToString());
            }
        }

        private void AddModerator()
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to login first.");
                return;
            }
            Console.WriteLine("Please enter the user name of the subforum to add to");
            string subforumName = Console.ReadLine();
            Console.WriteLine("Please enter the name of the moderator to add");
            string moderator = Console.ReadLine();
            Result r = controller.AddModerator(moderator, subforumName);
            if (r == Result.OK)
            {
                Console.WriteLine("Moderator added successfully!");
            }
            else
            {
                Console.WriteLine("Could not add the moderator! Got response: " + r.ToString());
            }
        }

        private void PrintCurrentLocation()
        {
            //Should print the current Post/Subforum.
            if (currentSubForum == null)
            {
                ListSubForums();
            }
            else if (currentPost == null)
            {
                PrintPostList(controller.GetSubforum(currentSubForum));
            }
            else
            {
                PrintPostList(controller.GetReplies(currentPost.Key));
            }

        }

        private void RemovePost(string[] command)
        {
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to log in in order to delete a post.");
            }
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
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to log in in order to edit a post.");
                return;
            }
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
            if (currentPost == null || currentSubForum == null)
            {
                //we are already at top level or looking at a subforum. just list the subforums again.
                currentPost = null;
                currentSubForum = null;
                ListSubForums();
            }
            else if (currentPost.ParentPost == null)
            {
                //we are currently looking at a subforum post, back to the subforum itself.
                currentPost = null;
                GetSubforum(currentSubForum);
            }
            else
            {
                //we are currently looking at some post. back up to its parent.
                currentPost = controller.GetPost(currentPost.Key);
                PrintPostList(controller.GetReplies(currentPost.Key));
            }
        }

        private void ListSubForums()
        {
            Console.WriteLine("Here is the list of sub-forums:");
            int i = 0;
            foreach (String subforum in controller.GetSubforumsList())
            {
                Console.WriteLine(i + ") " + subforum);
                i++;
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
                    Post[] replies = controller.GetReplies(p.Key);
                    if (replies != null)
                    {
                        currentPost = p;
                        PrintPostList(replies);
                    }
                    else
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
                currentPost = null;
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
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to log in in order to Reply on a post.");
                return;
            }
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
                    PrintCurrentLocation();
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
            if (!controller.loggedIn)
            {
                Console.WriteLine("You need to log in in order to post.");
                return;
            }
            if (currentSubForum != null)
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
                        Console.WriteLine("Sorry, you don't have enough permissions.");
                    }
                    else if (r == Result.ENTRY_EXISTS)
                    {
                        Console.WriteLine("The post already exists.");
                    }
                    else if (r == Result.USER_NOT_FOUND)
                    {
                        Console.WriteLine("Sorry, could not post. Are you logged in?");
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
            else
            {
                Console.WriteLine("It seems that you haven't entered a subforum yet...");
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
