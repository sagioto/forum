﻿using System;
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
        public bool loggedIn = false;
        private string loggedAs = "guest";
        private string loggedPassword = "";
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

        public Result Register(string userName, string password)
        {
            return netAdaptor.Register(userName, password);
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

        public bool Activate()
        {
            if (!loggedIn)
            {
                return false;
            }
            if (netAdaptor.Activate(loggedAs, loggedPassword) == Result.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Deactivate(string username, string password)
        {
            if (!loggedIn)
            {
                return false;
            }
            if (netAdaptor.Deactivate(loggedAs, loggedPassword) == Result.OK)
            {
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

        public Post GetPost(Postkey postkey)
        {
            return netAdaptor.GetPost(postkey);
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

        internal Post[] Search(string query)
        {
            return netAdaptor.Search(query);
        }

        public Post[] GetReplies(Postkey postkey)
        {
            try
            {
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

        public Result EditPost(Postkey postToEdit, string title, string body)
        {
            Post edit = netAdaptor.GetPost(postToEdit);
            Post newPost = new Post(postToEdit, title, body, edit.ParentPost, currentSubForum);
            return netAdaptor.EditPost(postToEdit, newPost, loggedAs, loggedPassword);
        }

        public Result RemovePost(Postkey postkey)
        {
            return netAdaptor.RemovePost(postkey, loggedAs, loggedPassword);
        }

        internal Result Ban(string usernameToBan)
        {
            return netAdaptor.Ban(usernameToBan, loggedAs, loggedPassword);
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

        public Result ReplaceAdmin(string newAdminUsername, string newAdminPassword)
        {
            return netAdaptor.ReplaceAdmin(loggedAs, loggedPassword, newAdminUsername, newAdminPassword);
        }

        public Post Subscribe(string username)
        {
            return netAdaptor.Subscribe(username);
        }

        public bool ListenOnForum(string username, string subForumName)
        {
            return netAdaptor.ListenOnForum(username, subForumName);
        }

        public int GetNumOfLoggedInUsers()
        {
            return netAdaptor.GetNumberOfLoggedInUsers();
        }
    }
}
