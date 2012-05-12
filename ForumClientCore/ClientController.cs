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
        private string loggedAs = null;
        private Post currentPost = null;
        private string currentSubForum = "";

        public event ClientNetworkAdaptor.OnUpdate OnUpdateFromController;  //Event to be invoked when getting a notify by NetworkAdaptor

        /// <summary>
        /// Constructor
        /// </summary>
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
        public void netAdaptor_OnUpdateFromServer(string message)
        {
            OnUpdateFromController(message);    // Invoking an event - will notify evryone who sleep on it
        }


        public void AddMessage(string s)
        {
            netAdaptor.addMessage(s);
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
            return netAdaptor.Register(userName, password);
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
            return netAdaptor.Post(subForumName, newPost);
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
            }
            return subForum;
        }

        public Post[] GetReplies(Postkey postkey)
        {
            return netAdaptor.GetReplies(postkey);
        }

        bool Reply(Postkey originalPost, string title, string body)
        {
            Post newReply = new Post(new Postkey(loggedAs, DateTime.Now), title, body, originalPost, currentSubForum);
            return netAdaptor.Reply(originalPost, newReply);
        }
        

    }
}

