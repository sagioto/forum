using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.ForumService;
using System.ServiceModel;
using ForumUtils.NetworkLayer;
using ForumUtils.SharedDataTypes;

namespace ForumClientCore.NetworkLayer
{
    public class ClientNetworkAdaptor
    {
        IForumService webService;
        ClientNetworkListener netListener;

        // Event setting:
        public delegate void OnUpdate(string text);
        public event OnUpdate OnUpdateFromServer;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientNetworkAdaptor()
        {
            // Network listener settings
            netListener = new ClientNetworkListener();
            netListener.OnUpdateFromServer +=new ClientNetworkListener.OnUpdate(netListener_OnUpdateFromServer); // Register to the update event of netListener
            
            // Web Service settings
            InstanceContext context = new InstanceContext(netListener);     // Sending IntanceContex to Server that it will be able to make callbacks
            webService = new ForumServiceClient(context);
            
            webService.SubscribeToForum();  // Subscribing to Forum in order to get callbacks
        }

        /// <summary>
        /// Destructor
        /// </summary>
        /// <remarks>
        /// TODO - Need to find a way to do unsubscribe when being destroyed. Not sure that destructor is the right way... 
        /// </remarks>
        ~ClientNetworkAdaptor()
        {
            webService.UnsubscribeFromForum();
        }

        /// <summary>
        /// Temp method - just to test connection to ws. To be deleted.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string getDataFromServer(int num)
        {
            return webService.GetData(num);
        }

        
        /// <summary>
        /// Need to be changed to 'Post'
        /// </summary>
        /// <param name="s"></param>
        internal void addMessage(string s)
        {
            try
            {
                webService.AddMessage(s);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Registers a new user on the server.
        /// </summary>
        /// <param name="usename"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if registration succeeded, false if user is already registered.</returns>
        internal bool Register(String usename, String password)
        {
            return webService.Register(usename, password);
        }

        /// <summary>
        /// Logs in to server with username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if login succeeded, false if User/Password are incorrect.</returns>
        internal bool Login(String username, String password)
        {
            return webService.Login(username, password);
        }

        /// <summary>
        /// Logs out of server with username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns true if logged out successfully, false otherwise.</returns>
        internal bool Logout(String username)
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
            return webService.GetReplies(postkey);
        }

        /// <summary>
        /// Add a post to a sub forum.
        /// </summary>
        /// <param name="subForumName">The name sub forum to post in</param>
        /// <param name="postToAdd">The new post to be posted</param>
        /// <returns>Returns true if posting is successful.</returns>
        internal bool Post(String subForumName, Post postToAdd)
        {
            return webService.Post(subForumName, postToAdd);
        }

        /// <summary>
        /// Add a reply to a post.
        /// </summary>
        /// <param name="originalPost">The post being replied</param>
        /// <param name="newReply">The new reply post</param>
        /// <returns>Returns true if reply succeeded, false otherwise</returns>
        internal bool Reply(Postkey originalPost, Post newReply)
        {
            return webService.Reply(originalPost, newReply);
        }

        public string GetPost(string postkey)
        {
            throw new NotImplementedException();
        }

        public bool Post(string current, string toPost)
        {
            throw new NotImplementedException();
        }

        public bool Reply(string current, string toPost)
        {
            throw new NotImplementedException();
        }

        public bool EditPost(string postToUpdate, string originalPost, string usrname, string password)
        {
            throw new NotImplementedException();
        }

        public bool RemovePost(string postkey, string username, string password)
        {
            throw new NotImplementedException();
        }

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            throw new NotImplementedException();
        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            throw new NotImplementedException();
        }

        public string Subscribe()
        {
            throw new NotImplementedException();
        }

        public bool AddMessage(string message)
        {
            throw new NotImplementedException();
        }

        public bool SubscribeToForum()
        {
            throw new NotImplementedException();
        }

        public bool UnsubscribeFromForum()
        {
            throw new NotImplementedException();
        }

        public string GetData(int value)
        {
            throw new NotImplementedException();
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Will be called when netListener will invoke its update event
        /// </summary>
        /// <param name="message"></param>
        public void netListener_OnUpdateFromServer(string message)
        {
            OnUpdateFromServer(message);    // Invoke event OnUpdateFronServer - will be notify controller
        }
    }

}
