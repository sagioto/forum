using System;
using System.Linq;
using ForumServer.DataLayer;
using ForumServer.DataTypes;
using ForumServer.Policy;
using ForumServer.Security;
using ForumUtils.SharedDataTypes;
using System.Threading.Tasks;
using System.Collections.Generic;

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

                dataManager.InitForumData();

            }
            catch (Exception e)
            {
                log.Error("couldn't initialize service", e);
                throw e;
            }

        }


        #region user functions

        public Result Register(string username, string password)
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


        public Result Login(string username, string password)
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

        public Result Logout(string username)
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

        public Post Subscribe(String username)
        {
            try
            {
                log.Info("got request to subscribe frome user " + username);

                throw new NotImplementedException();

            }
            catch (Exception e)
            {
                log.Error("failed to subscribe", e);
                throw e;
            }
        }

        #endregion

        #region viewing functions

        public string[] GetSubforumsList()
        {
            try
            {
                log.Info("got request to enter");

                Subforum[] subs = dataManager.GetSubforums().ToArray<Subforum>();
                List<string> names = new List<string>();
                foreach (Subforum sub in subs)
                {
                    names.Add(sub.Name);
                }
                string[] sorted = names.ToArray();
                Array.Sort<string>(sorted);
                return sorted;
            }
            catch (Exception e)
            {
                log.Error("got requset to enter from someone but got error", e);
                throw e;
            }
        }


        public Post[] GetSubForum(string subforum)
        {
            try
            {
                log.Info("got request for sub forum " + subforum);
                return dataManager.GetSubforum(subforum).Posts.Values.ToArray().OrderBy(post => post.Key.Time).ToArray();
            }
            catch (Exception e)
            {
                log.Error("failed get sub forum " + subforum, e);
                throw e;
            }

        }

        public Post GetPost(Postkey postkey)
        {
            try
            {
                log.Info("got request to get post");

                return dataManager.GetPost(postkey);

            }
            catch (Exception e)
            {
                log.Error("failed to get post", e);
                throw e;
            }

        }

        public Post[] GetReplies(Postkey key)
        {
            try
            {
                log.Info("got request for post " + key);
                return dataManager.GetPost(key).Replies.Values.ToArray();//.OrderBy(post => post.Key.Time).ToArray();
            }
            catch (Exception e)
            {
                log.Error("failed get post " + key, e);
                throw e;
            }
        }

        #endregion

        #region posting functions

        public Result Post(string subforum, Post post)
        {
            try
            {
                log.Info("got request to post in sub forum: " + subforum);
                try
                {
                    return CheckPost(post)
                        && securityManager.IsAuthorizedToPost(post.Key.Username, subforum)
                        && dataManager.AddPost(post, subforum.ToString());
                }
                catch (SubforumNotFoundException)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                log.Error("failed to post in sub forum: " + subforum, e);
                throw e;
            }

        }


        public Result Reply(Postkey currPost, Post post)
        {
            try
            {
                log.Info("got request to reply to post " + currPost);
                return CheckPost(post)
                        && securityManager.IsAuthorizedToPost(post.Key.Username, post.Subforum)
                        && dataManager.AddReply(post, currPost);
            }
            catch (Exception e)
            {
                log.Error("failed to reply to post " + currPost, e);
                throw e;
            }

        }

        public Result EditPost(Postkey currPost, Post post, string username, string password)
        {
            try
            {
                log.Info("got request to edit post " + currPost);
                return CheckPost(post)
                    && securityManager.IsAuthorizedToEdit(username, currPost, password)
                    && dataManager.EditPost(post, currPost);
            }
            catch (Exception e)
            {
                log.Error("failed to edit post " + currPost, e);
                throw e;
            }

        }

        public Result RemovePost(Postkey originalPostKey, string username, string password)
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

        private Result CheckPost(ForumUtils.SharedDataTypes.Post post)
        {
            return ((post.Body != null && post.Body.Length != 0)
                   || (post.Title != null && post.Title.Length != 0));
        }

        #endregion

        #region admin functions


        public Result AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
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

        public Result RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
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

        private Result CheckIfModerator(string usernameToRemove)
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

        public Result ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
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

        public Result AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                log.Info("got request to add sub forum " + subforumName);

                return securityManager.AuthenticateAdmin(adminUsername, adminPassword)
            && dataManager.AddSubforum(new Subforum(subforumName));     //TODO - Its better that you will build the Subforum. No more required fields (like at least one Moderator?)

            }
            catch (Exception e)
            {
                log.Error("failed to add sub forum " + subforumName, e);
                throw e;
            }

        }

        public Result RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
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

        public Result ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {

            try
            {
                log.Info("got request to replace admin");

                if (securityManager.AuthenticateAdmin(oldAdminUsername, oldAdminPassword))
                {
                    User newAdmin;
                    try
                    {
                        newAdmin = dataManager.GetUser(newAdminUsername);
                    }
                    catch (UserNotFoundException)
                    {
                        newAdmin = new User(newAdminUsername, newAdminPassword);
                        dataManager.AddUser(newAdmin);
                    }
                    
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
                    return dataManager.GetAllPosts().Where(post => post.Key.Username.Equals(username)).Count();
                return -1;

            }
            catch (Exception e)
            {
                log.Error("failed to report total posts of user " + username, e);
                throw e;
            }

        }

        #endregion

       

    }
}