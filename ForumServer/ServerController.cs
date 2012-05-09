using System;
using System.Linq;
using ForumServer.DataLayer;
using ForumServer.DataTypes;
using ForumServer.Policy;
using ForumServer.Security;

namespace ForumServer
{
    public class ServerController
    {
        private DataManager dataManager;
        private SecurityManager securityManager;
        private PolicyManager policyManager;
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public ServerController()
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                log.Info("intializing service...");
                dataManager = new DataManager();
                securityManager = new SecurityManager(dataManager);
                policyManager = new PolicyManager(dataManager);

            }
            catch (Exception e)
            {
                log.Error("couldn't initialize service", e);
                throw e;
            }

        }

        public Subforum[] Enter()
        {
            try
            {
                log.Info("got request to enter");
                return dataManager.GetSubforums().ToArray<Subforum>();
            }
            catch (Exception e)
            {
                log.Error("got requset to enter from someone but got error", e);
                throw e;
            }
        }

        public bool Register(string username, string password)
        {
            try
            {
                log.Info("got request to register from user " + username + " and password *******");
                return securityManager.AuthorizedRegister(username, password);
            }
            catch (Exception e)
            {
                log.Error("failed to register user " + username, e);
                throw e;
            }
        }


        public bool Login(string username, string password)
        {
            try
            {
                log.Info("got request to login from user " + username + " and password *******");
                return securityManager.AuthorizedLogin(username, password);
            }
            catch (Exception e)
            {
                log.Error("failed to login user " + username, e);
                throw e;
            }

        }

        public bool Logout(string username)
        {
            try
            {
                log.Info("got request to logout from user " + username);
                return securityManager.AuthorizedLogout(username);
            }
            catch (Exception e)
            {
                log.Error("failed to logout user " + username, e);
                throw e;
            }

        }

        public Subforum GetSubForum(string subforum)
        {
            try
            {
                log.Info("got request for sub forum " + subforum);
                return dataManager.GetSubforum(subforum);
            }
            catch (Exception e)
            {
                log.Error("failed get sub forum " + subforum, e);
                throw e;
            }

        }

        public Post GetPost(Postkey key)
        {
            try
            {
                log.Info("got request for post " + key);
                return dataManager.GetPost(key);
            }
            catch (Exception e)
            {
                log.Error("failed get post " + key, e);
                throw e;
            }
        }

        public bool Post(string subforum, Post post)
        {
            try
            {
                log.Info("got request to post in sub forum: " + subforum);
                return securityManager.IsAuthorizedToPost(post.Key.Username, subforum)
               && dataManager.AddPost(post, subforum.ToString());
            }
            catch (Exception e)
            {
                log.Error("failed to post in sub forum: " + subforum, e);
                throw e;
            }

        }

        public bool Reply(Postkey currPost, Post post)
        {
            try
            {
                log.Info("got request to reply to post " + currPost);
                return securityManager.IsAuthorizedToPost(post.Key.Username, post.Subforum.Name)
                && dataManager.AddReply(post, currPost);
            }
            catch (Exception e)
            {
                log.Error("failed to reply to post " + currPost, e);
                throw e;
            }

        }

        public bool EditPost(Postkey currPost, Post post, string username, string password)
        {
            try
            {
                log.Info("got request to edit post " + currPost);
                return securityManager.IsAuthorizedToEdit(username, currPost, password)
                && dataManager.EditPost(post, currPost);
            }
            catch (Exception e)
            {
                log.Error("failed to edit post " + currPost, e);
                throw e;
            }

        }

        public bool RemovePost(Postkey originalPostKey, string username, string password)
        {
            try
            {
                log.Info("got request to remove post " + originalPostKey);
                return securityManager.IsAuthorizedToEdit(username, originalPostKey, password)
                && policyManager.IsAuthorizedToEdit(originalPostKey, username)
                && dataManager.RemovePost(originalPostKey);
            }
            catch (Exception e)
            {
                log.Error("failed to remove post " + originalPostKey, e);
                throw e;
            }

        }

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            try
            {
                log.Info("got request to add moderator " + usernameToAdd + " to " + subforum);

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
            catch (Exception e)
            {
                log.Error("failed to add moderator " + usernameToAdd + " to " + subforum, e);
                throw e;
            }

        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            try
            {
                log.Info("got request to remove moderator " + usernameToRemove + " to " + subforum);

                if (securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                 && policyManager.RemoveModerator(usernameToRemove, subforum))
                {
                    dataManager.GetSubforum(subforum).ModeratorsList.Remove(usernameToRemove);
                    bool moderator = CheckIfModerator(usernameToRemove);
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
            catch (Exception e)
            {
                log.Error("failed to remove moderator " + usernameToRemove + " to " + subforum, e);
                throw e;
            }




        }

        private bool CheckIfModerator(string usernameToRemove)
        {

            try
            {
                log.Info("got request to check if moderator " + usernameToRemove);

                bool moderator = false;
                foreach (Subforum sub in dataManager.GetSubforums())
                {
                    if (sub.ModeratorsList.Contains(usernameToRemove))
                        moderator = true;
                }
                return moderator;

            }
            catch (Exception e)
            {
                log.Error("failed to check if moderator " + usernameToRemove, e);
                throw e;
            }

        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {

            try
            {
                log.Info("got request to replace moderator" + usernameToRemove + " with " + usernameToAdd);

                return policyManager.ChangeModerator(usernameToRemove, usernameToAdd, subforum)
        && RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum)
        && AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);

            }
            catch (Exception e)
            {
                log.Error("failed to replace moderator" + usernameToRemove + " with " + usernameToAdd, e);
                throw e;
            }

        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                log.Info("got request to add sub forum " + subforumName);

                return securityManager.AuthenticateAdmin(adminUsername, adminPassword)
            && dataManager.AddSubforum(subforumName);

            }
            catch (Exception e)
            {
                log.Error("failed to add sub forum " + subforumName, e);
                throw e;
            }

        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                log.Info("got request add sub forum " + subforumName);

                return securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                    && dataManager.RemoveSubforum(subforumName);

            }
            catch (Exception e)
            {
                log.Error("failed to add sub forum " + subforumName, e);
                throw e;
            }

        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {

            try
            {
                log.Info("got request to replace admin");

                if (securityManager.AuthenticateAdmin(oldAdminUsername, oldAdminPassword))
                {
                    User newAdmin = new User(newAdminUsername, newAdminPassword);
                    User oldAdmin = dataManager.GetAdmin();
                    if (CheckIfModerator(oldAdminUsername))
                        oldAdmin.Level = AuthorizationLevel.MODERATOR;
                    else
                        oldAdmin.Level = AuthorizationLevel.MEMBER;
                    newAdmin.Level = AuthorizationLevel.ADMIN;
                    return dataManager.SetAdmin(newAdmin);
                }
                else return false;

            }
            catch (Exception e)
            {
                log.Error("failed to replace admin", e);
                throw e;
            }

        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                log.Info("got request to report sub forum total posts on " + subforumName);

                if (securityManager.AuthenticateAdmin(adminUsername, adminPassword))
                    return dataManager.GetSubforum(subforumName).TotalPosts;
                return -1;

            }
            catch (Exception e)
            {
                log.Error("failed report sub forum total posts on " + subforumName, e);
                throw e;
            }

        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {

            try
            {
                log.Info("got request to report total posts of user " + username);

                if (securityManager.AuthenticateAdmin(adminUsername, adminPassword))
                    return dataManager.GetAllPosts().Select(post => post.Key.Username.Equals(username)).Count(); ;
                return -1;

            }
            catch (Exception e)
            {
                log.Error("failed to report total posts of user " + username, e);
                throw e;
            }

        }
    }
}