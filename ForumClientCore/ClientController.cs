using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.NetworkLayer;
using System.ServiceModel;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumClientCore
{
    public class ClientController
    {
        ClientNetworkAdaptor netAdaptor;
        private bool loggedIn = false;
        private string loggedAs = "";
        private string loggedPassword = "";
        private Post currentPost = null;
        private string currentSubForum = "";

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
            //Default behaviour is without callback
            netAdaptor = new ClientNetworkAdaptor(false);
            netAdaptor.OnUpdateFromServer += new ClientNetworkAdaptor.OnUpdate(netAdaptor_OnUpdateFromServer);
        }


        /// <summary>
        /// This method is an example of using NetworkAdaptor
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        //     public string getDataFromServer(int num)
        //     {
        //          return netAdaptor.getDataFromServer(num);
        //       }


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
            if (netAdaptor.Login(userName, password) == Result.OK)
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

        public Result Register(string userName, string password)
        {
            return netAdaptor.Register(userName, password);
        }


        public bool Logout()
        {
            if (!loggedIn)
            {
                return true;
            }
            if (netAdaptor.Logout(loggedAs) == Result.OK)
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

        public Result Post(string subForumName, string title, string body)
        {
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
                Post[] toReturn = netAdaptor.GetSubforum(currentSubForum);

                if (toReturn == null)
                    return new Post[0];
                else
                    return toReturn;
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

        public Result Reply(Postkey originalPost, string title, string body)
        {
            Post newReply = new Post(new Postkey(loggedAs, DateTime.Now), title, body, originalPost, currentSubForum);
            return netAdaptor.Reply(originalPost, newReply);
        }

        //overload - no need the argument originalPost (can get this info from the field currentPost)
        public Result Reply(string title, string body)
        {
            Post newReply = new Post(new Postkey(loggedAs, DateTime.Now), title, body, currentPost.Key, currentSubForum);
            return netAdaptor.Reply(currentPost.Key, newReply);
        }

        public Result EditPost(string title, string body)
        {
            Post newPost = new Post(currentPost.Key, title, body, currentPost.ParentPost, currentSubForum);
            return netAdaptor.EditPost(currentPost.Key, newPost, loggedAs, loggedPassword);
        }

        public Result RemovePost(Postkey postkey)
        {
            return netAdaptor.RemovePost(postkey, loggedAs, loggedPassword);
        }

        //overload
        public Result RemovePost()
        {
            Post parentPost = netAdaptor.GetPost(currentPost.ParentPost);
            Result adapterAnswer = netAdaptor.RemovePost(currentPost.Key, loggedAs, loggedPassword);
            currentPost = parentPost;
            return adapterAnswer;
        }

        public Result AddModerator(string usernameToAdd, string subforum)
        {
            return netAdaptor.AddModerator(loggedAs, loggedPassword, usernameToAdd, subforum);
        }

        public Result RemoveModerator(string usernameToRemove, string subforum)
        {
            return netAdaptor.RemoveModerator(loggedAs, loggedPassword, usernameToRemove, subforum);
        }

        public Result ReplaceModerator(string usernameToAdd, string usernameToRemove, string subforum)
        {
            return netAdaptor.ReplaceModerator(loggedAs, loggedPassword, usernameToAdd, usernameToRemove, subforum);
        }

        public Result AddSubforum(string subforumName)
        {
            return netAdaptor.AddSubforum(loggedAs, loggedPassword, subforumName);
        }

        public Result RemoveSubforum(string subforumName)
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
