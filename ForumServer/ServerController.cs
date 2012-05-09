using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataLayer;
using ForumServer.Security;
using ForumServer.Policy;
using ForumServer.DataTypes;

namespace ForumServer
{
    public class ServerController
    {
        private DataManager dataManager;
        private SecurityManager securityManager;
        private PolicyManager policyManager;


        public ServerController()
        {
            dataManager = new DataManager();
            securityManager = new SecurityManager(dataManager);
            policyManager = new PolicyManager(dataManager);
        }

        public Subforum[] Enter()
        {
            return dataManager.GetSubforums().ToArray<Subforum>();
        }

        public bool Register(string username, string password)
        {
            return securityManager.AuthorizedRegister(username, password);
        }


        public bool Login(string username, string password)
        {
            return securityManager.AuthorizedLogin(username, password);
        }

        public bool Logout(string username)
        {
            return securityManager.AuthorizedLogout(username);
        }

        public Subforum GetSubForum(string subforum)
        {
            return dataManager.GetSubforum(subforum);
        }

        public Post GetPost(Postkey key)
        {
            return dataManager.GetPost(key);
        }

        public bool Post(Subforum subforum, Post post)
        {
            return securityManager.IsAuthorizedToPost(post.Key.Username, subforum)
                && dataManager.AddPost(post, subforum.ToString());
        }

        public bool Reply(Postkey currPost, Post post)
        {
            return securityManager.IsAuthorizedToPost(post.Key.Username, post.Subforum)
                && dataManager.AddReply(post, currPost);
        }

        internal bool EditPost(Postkey currPost, DataTypes.Post post)
        {
            return securityManager.IsAuthorizedToEdit(post.Key.Username, currPost)
                && dataManager.EditPost(post, currPost);
        }
    }
}