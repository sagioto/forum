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
            if (securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                && policyManager.AddModerator(usernameToAdd, subforum))
            {
                dataManager.GetSubforum(subforum).ModeratorsList.Add(usernameToAdd);
                User user = dataManager.GetUser(usernameToAdd);
                if (!user.Level.Equals(AuthorizationLevel.ADMIN))
                {
                    user.Level = AuthorizationLevel.MODERATOR;
                    dataManager.UpdateUser(user);
                }
                return true;
            }
            else return false;
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            if (securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                && policyManager.RemoveModerator(usernameToRemove, subforum))
            {
                dataManager.GetSubforum(subforum).ModeratorsList.Remove(usernameToRemove);
                bool moderator = false;
                foreach (Subforum sub in dataManager.GetSubforums())
                {
                    if (sub.ModeratorsList.Contains(usernameToRemove))
                        moderator = true;
                }
                if (!moderator)
                {
                    User user = dataManager.GetUser(usernameToRemove);
                    user.Level = AuthorizationLevel.MEMBER;
                    dataManager.UpdateUser(user);
                }
                return true;
            }
            else return false;
        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {
            return policyManager.ChangeModerator(usernameToRemove, usernameToAdd, subforum)
                && RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum)
                && AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return securityManager.AuthenticateAdmin(adminUsername, adminPassword);
                //TODO && dataManager.AddSubforum(subforum)
        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return securityManager.AuthenticateAdmin(adminUsername, adminPassword);
            //TODO && dataManager.RemoveSubforum(subforum)
        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            return securityManager.AuthenticateAdmin(oldAdminUsername, oldAdminPassword);
            //TODO && dataManager.SetAdmin(new User(newAdminUsername, newAdminPassword))
        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            if (securityManager.AuthenticateAdmin(adminUsername, adminPassword))
                return dataManager.GetSubforum(subforumName).TotalPosts;
            return -1;
        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            if (securityManager.AuthenticateAdmin(adminUsername, adminPassword))
                //return dataManager.GetUser(username);
                //TODO complete
                return 0;
            return -1;
        }
    }
}