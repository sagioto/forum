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
            return dataManager.GetSubforums();
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

        public bool Post(string subforum, Post post)
        {
            return securityManager.IsAuthorizedToPost(post.Key.Username, subforum)
                && dataManager.AddPost(post, subforum.ToString());
        }

        public bool Reply(Postkey currPost, Post post)
        {
            return securityManager.IsAuthorizedToPost(post.Key.Username, post.Subforum.Name)
                && dataManager.AddReply(post, currPost);
        }

        public bool EditPost(Postkey currPost, Post post, string password)
        {
            return securityManager.IsAuthorizedToEdit(post.Key.Username, currPost, password)
                && dataManager.EditPost(post, currPost);
        }

        public bool RemovePost(Postkey originalPostKey, string password)
        {
            return securityManager.IsAuthorizedToEdit(originalPostKey.Username, originalPostKey, password)
                && policyManager.IsAuthorizedToEdit(originalPostKey, originalPostKey.Username);
            //TODO remove comment 
                //&& dataManager.RemovePost(originalPostKey);
        }

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        internal bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        internal bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            throw new NotImplementedException();
        }

        internal int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        internal int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            throw new NotImplementedException();
        }
    }
}