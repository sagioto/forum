using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.ForumService;
using ForumShared.ForumAPI;
using System.ServiceModel;
using ForumShared.NetworkLayer;
using ForumShared.SharedDataTypes;

namespace ForumClientCore.NetworkLayer
{
    public class ClientNetworkAdaptor
    {
        ForumClientCore.ForumService.IForumService webService;
        ClientNetworkListener netListener;


        // Event setting:
        public delegate void OnUpdate(Post p);
        public event OnUpdate OnUpdateFromServer;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientNetworkAdaptor(bool GetCallBack)
        {
            // Network listener settings
            netListener = new ClientNetworkListener();
            netListener.OnUpdateFromServer += new ClientNetworkListener.OnUpdate(netListener_OnUpdateFromServer); // Register to the update event of netListener

            // Web Service settings
            InstanceContext context = new InstanceContext(netListener);     // Sending IntanceContex to Server that it will be able to make callbacks
            //webService = new ForumServiceClient(context); //TODO uncomment
            if (GetCallBack)
            {
                //webService.SubscribeToForum();  // Subscribing to Forum in order to get callbacks TODO uncomment
            }
        }

        public ClientNetworkAdaptor()
        {
            // Network listener settings
            netListener = new ClientNetworkListener();
            netListener.OnUpdateFromServer += new ClientNetworkListener.OnUpdate(netListener_OnUpdateFromServer); // Register to the update event of netListener

            // Web Service settings
            InstanceContext context = new InstanceContext(netListener);     // Sending IntanceContex to Server that it will be able to make callbacks
            //  webService = new ForumServiceClient(context); //TODO uncomment
        }

        /// <summary>
        /// Destructor
        /// </summary>
        /// <remarks>
        /// TODO - Need to find a way to do unsubscribe when being destroyed. Not sure that destructor is the right way... 
        /// </remarks>
        ~ClientNetworkAdaptor()
        {
            // webService.UnsubscribeFromForum();
        }

        /// <summary>
        /// Temp method - just to test connection to ws. To be deleted.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        //       public string getDataFromServer(int num)
        //       {
        //          return webService.GetData(num);
        //     }


        /// <summary>
        /// Registers a new user on the server.
        /// </summary>
        /// <param name="usename"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if registration succeeded, false if user is already registered.</returns>
        internal Result Register(String usename, String password)
        {
            return webService.Register(usename, password);
        }

        /// <summary>
        /// Logs in to server with username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if login succeeded, false if User/Password are incorrect.</returns>
        internal Result Login(String username, String password)
        {
            return webService.Login(username, password);
        }

        /// <summary>
        /// Logs out of server with username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns true if logged out successfully, false otherwise.</returns>
        internal Result Logout(String username)
        {
            return webService.Logout(username);
        }

        /// <summary>
        /// Gets the list of sub forums from the server.
        /// </summary>
        /// <returns>Returns an array of Subforums. (The main forum).</returns>
        internal string[] GetSubforumsList()
        {
            return webService.GetSubforumsList();
        }

        /// <summary>
        /// Get a Subforum by its name.
        /// </summary>
        /// <param name="subforumname"></param>
        /// <returns>Returns a Subforum</returns>
        internal Post[] GetSubforum(String subforumname)
        {
            return webService.GetSubforum(subforumname);
        }

        /// <summary>
        /// Get a Post using its PostKey
        /// </summary>
        /// <param name="postkey">A post key consisting of the user + timestamp</param>
        /// <returns>The required Post</returns>
        internal Post[] GetReplies(Postkey postkey)
        {
            try
            {
                return webService.GetReplies(postkey);
            }
            catch (System.Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Add a post to a sub forum.
        /// </summary>
        /// <param name="subForumName">The name sub forum to post in</param>
        /// <param name="postToAdd">The new post to be posted</param>
        /// <returns>Returns true if posting is successful.</returns>
        internal Result Post(String subForumName, Post postToAdd)
        {
            try
            {
                return webService.Post(subForumName, postToAdd);
            }
            catch (System.ServiceModel.FaultException e)
            {
                throw e;
            }
        }

        /// <summary>
        /// Add a reply to a post.
        /// </summary>
        /// <param name="originalPost">The post being replied</param>
        /// <param name="newReply">The new reply post</param>
        /// <returns>Returns true if reply succeeded, false otherwise</returns>
        internal Result Reply(Postkey originalPost, Post newReply)
        {
            return webService.Reply(originalPost, newReply);
        }

        internal Post GetPost(Postkey postkey)
        {
            try
            {
                return webService.GetPost(postkey);
            }
            catch (System.ServiceModel.FaultException e)
            {
                throw e;
            }
        }

        public Result EditPost(Postkey oldPost, Post newPost, string username, string password)
        {
            return webService.EditPost(oldPost, newPost, username, password);
        }

        public Result RemovePost(Postkey postkey, string username, string password)
        {
            return webService.RemovePost(postkey, username, password);
        }

        public Result AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            return webService.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
        }

        public Result RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            return webService.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
        }

        public Result ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {
            return webService.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
        }

        public Result AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return webService.AddSubforum(adminUsername, adminPassword, subforumName);
        }

        public Result RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return webService.RemoveSubforum(adminUsername, adminPassword, subforumName);
        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            return webService.ReportSubForumTotalPosts(adminUsername, adminPassword, subforumName);
        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            return webService.ReportUserTotalPosts(adminUsername, adminPassword, username);
        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            return ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
        }

        public bool AddMessage(string message)
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will be called when netListener will invoke its update event
        /// </summary>
        /// <param name="message"></param>
        public void netListener_OnUpdateFromServer(Post message)
        {
            OnUpdateFromServer(message);    // Invoke event OnUpdateFronServer - will be notify controller
        }
    }

}
