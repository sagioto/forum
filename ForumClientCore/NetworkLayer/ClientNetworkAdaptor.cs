using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.ForumService;
using System.ServiceModel;
using ForumUtils.NetworkLayer;
using ForumServer.DataTypes;

namespace ForumClientCore.NetworkLayer
{
    public class ClientNetworkAdaptor
    {
        IForumService webService;
        ClientNetworkListener netListener;
        private ISerializer serializer = new JsonSerializer();

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
        /// Enter the server.
        /// </summary>
        /// <returns>Returns an array of the sub forum (the main forum list?)</returns>
        Subforum[] Enter()
        {
            return serializer.DeserializeSubforumArray(webService.Enter());
        }

        /// <summary>
        /// Registers a new user on the server.
        /// </summary>
        /// <param name="usename"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if registration succeeded, false otherwise.</returns>
        bool Register(String usename, String password)
        {
            return webService.Register(usename, password);
        }

        /// <summary>
        /// Logs in to server with username and password.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>Returns true if login succeeded, false otherwise</returns>
        bool Login(String username, String password)
        {
            return webService.Login(username, password);
        }

        /// <summary>
        /// Logs out of server with username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>Returns true if logged out successfully, false otherwise.</returns>
        bool Logout(String username)
        {
            return webService.Logout(username);
        }

        /// <summary>
        /// Gets the list of sub forums from the server.
        /// </summary>
        /// <returns>Returns an array of Subforums. (The main forum).</returns>
        Subforum[] GetSubforumsList()
        {
            return serializer.DeserializeSubforumArray(webService.GetSubforumsList());
        }

        /// <summary>
        /// Get a Subforum by its name.
        /// </summary>
        /// <param name="subforumname"></param>
        /// <returns>Returns a Subforum</returns>
        Subforum GetSubforum(String subforumname)
        {
            return serializer.DeserializeSubforum(webService.GetSubforum(subforumname));
        }

        /// <summary>
        /// Get a Post using its PostKey
        /// </summary>
        /// <param name="postkey">A post key consisting of the user + timestamp</param>
        /// <returns>The required Post</returns>
        Post GetPost(Postkey postkey)
        {
            return serializer.DeserializePost(webService.GetPost(serializer.SerializePostkey(postkey)));
        }

        /// <summary>
        /// Add a post to a sub forum.
        /// </summary>
        /// <param name="forumToPostIn">The sub forum to post in</param>
        /// <param name="postToAdd">The new post to be posted</param>
        /// <returns>Returns true if posting is successful.</returns>
        bool Post(String forumToPostIn, Post postToAdd)
        {
            return webService.Post(forumToPostIn, serializer.SerializePost(postToAdd));
        }

        /// <summary>
        /// Add a reply to a post.
        /// </summary>
        /// <param name="originalPost">The post being replied</param>
        /// <param name="newReply">The new reply post</param>
        /// <returns>Returns true if reply succeeded, false otherwise</returns>
        bool Reply(Postkey originalPost, Post newReply)
        {
            return webService.Reply(serializer.SerializePostkey(originalPost), serializer.SerializePost(newReply));
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
