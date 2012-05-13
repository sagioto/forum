using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.NetworkLayer;
using System.ServiceModel;
using ForumUtils.SharedDataTypes;

namespace ForumClientCore
{
    public class ClientController
    {
        ClientNetworkAdaptor netAdaptor;
        private bool loggedIn = false;
        private string loggedAs = "";
        private string loggedPassword = "";
        private Post currentPost = null;

        public Post CurrentPost
        {
            get
            {
                return currentPost;
            }
            set
            {
                currentPost = value;
            }
        }
        private string currentSubForum = "";

        public string CurrentSubForum
        {
            get
            {
                return currentSubForum;
            }
            set
            {
                currentSubForum = value;
            }
        }

        public event ClientNetworkAdaptor.OnUpdate OnUpdateFromController;  //Event to be invoked when getting a notify by NetworkAdaptor

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientController(bool GetCallBack)
        {
            netAdaptor = new ClientNetworkAdaptor(GetCallBack);
            netAdaptor.OnUpdateFromServer += new ClientNetworkAdaptor.OnUpdate(netAdaptor_OnUpdateFromServer);
        }

        public ClientController()
        {
            netAdaptor = new ClientNetworkAdaptor();
            netAdaptor.OnUpdateFromServer += new ClientNetworkAdaptor.OnUpdate(netAdaptor_OnUpdateFromServer);
        }


        /// <summary>
        /// This method is an example of using NetworkAdaptor
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string getDataFromServer(int num)
        {
            return netAdaptor.getDataFromServer(num);
        }


        /// <summary>
        /// When netAdaptor invoked an event OnUpdateFromController it gets to this method
        /// </summary>
        /// <param name="message"></param>
        public void netAdaptor_OnUpdateFromServer(Post message)
        {
            OnUpdateFromController(message);    // Invoking an event - will notify evryone who sleep on it
        }

        public bool Login(string userName, string password)
        {
            if (loggedIn)
            {
                return false;
            }
            if (netAdaptor.Login(userName, password))
            {
                loggedAs = userName;
                loggedPassword = password;
                loggedIn = true;
                return true;
            }
            else
            {
                return false;
            }
        }

        public String[] GetSubforumsList()
        {
            return netAdaptor.GetSubforumsList();
        }

        public bool Register(string userName, string password)
        {
            if (password.Length == 0)
            {
                return false;
            }
            else
            {
                return netAdaptor.Register(userName, password);
            }
        }


        public bool Logout()
        {
            if (!loggedIn)
            {
                return true;
            }
            if (netAdaptor.Logout(loggedAs))
            {
                loggedAs = "";
                loggedPassword = "";
                loggedIn = false;
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Post(string subForumName, string title, string body)
        {
            if (!loggedIn)
            {
                return false;
            }
            Postkey newKey = new Postkey(loggedAs, DateTime.Now);
            Post newPost = new Post(newKey, title, body, null, subForumName);
            try
            {
                return netAdaptor.Post(subForumName, newPost);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Post[] Back()
        {
            if (currentPost == null)
            {
                currentSubForum = "";
                return null;
            }
            else if (currentPost.ParentPost == null)
            {
                currentPost = null;
                return netAdaptor.GetSubforum(currentSubForum);
            }
            else
            {
                Post parent = netAdaptor.GetPost(currentPost.ParentPost);
                currentPost = parent;
                return netAdaptor.GetReplies(currentPost.Key);
            }

        }

        public Post[] GetSubforum(String subforumname)
        {
            Post[] subForum = netAdaptor.GetSubforum(subforumname);
            if (subForum != null)
            {
                currentSubForum = subforumname;
                currentPost = null;
            }
            return subForum;
        }

        public Post[] GetReplies(Postkey postkey)
        {
            try
            {
                currentPost = netAdaptor.GetPost(postkey);
                return netAdaptor.GetReplies(postkey);
            }
            catch (FaultException e)
            {
                throw e;
            }
        }

        public bool Reply(Postkey originalPost, string title, string body)
        {
            Post newReply = new Post(new Postkey(loggedAs, DateTime.Now), title, body, originalPost, currentSubForum);
            return netAdaptor.Reply(originalPost, newReply);
        }

        public bool EditPost(string title, string body)
        {
            Post newPost = new Post(currentPost.Key, title, body, currentPost.ParentPost, currentSubForum);
            return netAdaptor.EditPost(currentPost.Key, newPost, loggedAs, loggedPassword);
        }

        public bool RemovePost(Postkey postkey)
        {
            return netAdaptor.RemovePost(postkey, loggedAs, loggedPassword);
        }

        public bool AddModerator(string usernameToAdd, string subforum)
        {
            return netAdaptor.AddModerator(loggedAs, loggedPassword, usernameToAdd, subforum);
        }

        public bool RemoveModerator(string usernameToRemove, string subforum)
        {
            return netAdaptor.RemoveModerator(loggedAs, loggedPassword, usernameToRemove, subforum);
        }

        public bool ReplaceModerator(string usernameToAdd, string usernameToRemove, string subforum)
        {
            return netAdaptor.ReplaceModerator(loggedAs, loggedPassword, usernameToAdd, usernameToRemove, subforum);
        }

        public bool AddSubforum(string subforumName)
        {
            return netAdaptor.AddSubforum(loggedAs, loggedPassword, subforumName);
        }

        public bool RemoveSubforum(string subforumName)
        {
            return netAdaptor.RemoveSubforum(loggedAs, loggedPassword, subforumName);
        }

        public int ReportSubForumTotalPosts(string subforumName)
        {
            return netAdaptor.ReportSubForumTotalPosts(loggedAs, loggedPassword, subforumName);
        }

        public int ReportUserTotalPosts(string username)
        {
            return netAdaptor.ReportUserTotalPosts(loggedAs, loggedPassword, username);
        }

        public bool ReplaceAdmin(string newAdminUsername, string newAdminPassword)
        {
            return netAdaptor.ReplaceAdmin(loggedAs, loggedPassword, newAdminUsername, newAdminPassword);
        }

        public string Subscribe()
        {
            throw new NotImplementedException();
        }
    }
}
