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

        public string GetSubforumsList()
        {
            string forumsList = "";
            Subforum[] forumsArray = netAdaptor.GetSubforumsList();
            foreach (Subforum forum in forumsArray)
            {
                forumsList = forumsList + forum.Name;
            }
            return forumsList;
        }

        public bool Register(string userName, string password)
        {
            return netAdaptor.Register(userName, password);
        }


        public bool Logout()
        {
            return netAdaptor.Logout(loggedAs);
        }

        public bool Post(string title, string subForumName)
        {
            if (!loggedIn)
            {
                return false;
            }
            Postkey newKey = new Postkey(loggedAs, DateTime.Now);
            Post newPost = new Post(newKey, title, null, netAdaptor.GetSubforum(subForumName));
            return netAdaptor.Post(subForumName, newPost);
        }
    }
}
