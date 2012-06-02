using System;
using System.Linq;
using ForumServer.DataLayer;
using ForumServer.DataTypes;
using ForumServer.Policy;
using ForumServer.Security;
using ForumShared.SharedDataTypes;
using ForumUtils.SharedDataTypes;
using ForumShared.ForumAPI;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;
using System.Configuration;
using System.Collections.Concurrent;

namespace ForumServer
{
    public class ServerController
    {
        private IDataManager dataManager;
        private ISecurityManager securityManager;
        private IPolicyManager policyManager;
        volatile private Post posted = new Post();
        private static ConcurrentDictionary<string, Object> subscribed = new ConcurrentDictionary<string, Object>();
        private TimeSpan timeToWait;
        private log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public ServerController()
        {
            log4net.Config.XmlConfigurator.Configure();
            try
            {
                log.Info("initializing service...");
                subscribed.TryAdd("guest", new Object());
                dataManager = new DataManager();
                securityManager = new SecurityManager(dataManager);
                policyManager = new PolicyManager(dataManager);
                string time = ConfigurationManager.AppSettings["timeToWaitMinutes"];
                timeToWait = TimeSpan.FromMinutes(int.Parse(time));
                // dataManager.InitForumData();

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
                if (username != null && username.Length != 0
                    && password != null && password.Length != 0)
                    return securityManager.AuthorizedRegister(username, password);
                else return Result.NULL;
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



        internal int GetNumOfLoggedInUsers()
        {
            try
            {
                log.Info("got request for number of users");

                return dataManager.GetAllLoggedInUsers().Count;

            }
            catch (Exception e)
            {
                log.Error("failed to get number of users", e);
                throw e;
            }

        }

        public Post Subscribe(String username)
        {
            try
            {
                log.Info("got request to subscribe frome user " + username);
                if(!username.Equals("guest"))
                    subscribed.TryAdd(username, new Object());
                {
                    lock (subscribed[username])
                    {
                        DateTime start = DateTime.Now;
                        Monitor.Wait(subscribed[username], timeToWait);
                        if (DateTime.Now - start < timeToWait)
                            return this.posted;
                    }
                }
                return null;
            }
            catch (Exception e)
            {
                log.Error("failed to subscribe", e);
                throw e;
            }
        }


        private void Notify()
        {

            try
            {
                log.Info("got request to notify on post");
                
                foreach (string username in subscribed.Keys)
                {
                    lock (subscribed[username])
                    {
                        Monitor.PulseAll(subscribed[username]);
                        if (!username.Equals("guest"))
                            subscribed.Keys.Remove(username);
                    }
                }

            }
            catch (Exception e)
            {
                log.Error("failed to notify", e);
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

                string[] subs = dataManager.GetSubforums().Select(subforum => subforum.Name).ToArray();
                Array.Sort<string>(subs);
                return subs;
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
                if (key != null)
                {
                    log.Info("got request for post " + key);
                    return dataManager.GetPost(key).Replies.Values.ToArray();//.OrderBy(post => post.Key.Time).ToArray();
                }
                else
                {
                    log.Info("got a null postkey in get replies");
                    return null;
                }
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
                if (!CheckPost(post))
                    return Result.ILLEGAL_POST;

                Result res = securityManager.IsAuthorizedToPost(post.Key.Username, subforum);
                if (res == Result.OK)
                    if (dataManager.AddPost(post, subforum.ToString()))
                    {
                        this.posted = post;
                        Notify();
                        return Result.OK;
                    }
                    else return Result.SUB_FORUM_NOT_FOUND;
                else return res;
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
                if (!CheckPost(post))
                    return Result.ILLEGAL_POST;

                Result res = securityManager.IsAuthorizedToPost(post.Key.Username, post.Subforum);
                if (res == Result.OK)
                    if (dataManager.AddReply(post, currPost))
                    {
                        this.posted = post;
                        Notify();
                        return Result.OK;
                    }
                    else return Result.POST_NOT_FOUND;
                else return res;
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
                if (!CheckPost(post))
                    return Result.ILLEGAL_POST;
                Result res = securityManager.IsAuthorizedToEdit(username, currPost, password);
                if (res == Result.OK)
                    if (dataManager.EditPost(post, currPost))
                        return Result.OK;
                    else return Result.POST_NOT_FOUND;
                else return res;
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
                Result res = securityManager.IsAuthorizedToEdit(username, originalPostKey, password)
                                | policyManager.IsAuthorizedToEdit(originalPostKey, username);

                if (res == Result.OK)
                    if (dataManager.RemovePost(originalPostKey))
                        return Result.OK;
                    else return Result.POST_NOT_FOUND;
                else return res;
            }
            catch (Exception e)
            {
                log.Error("failed to remove post " + originalPostKey, e);
                throw e;
            }

        }

        private bool CheckPost(Post post)
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

                Result res = securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                             | policyManager.AddModerator(usernameToAdd, subforum);
                if (res == Result.OK)
                {
                    dataManager.GetSubforum(subforum).ModeratorsList.Add(usernameToAdd);
                    User user = dataManager.GetUser(usernameToAdd);
                    if (!user.Level.Equals(AuthorizationLevel.ADMIN))
                    {
                        user.Level = AuthorizationLevel.MODERATOR;
                        dataManager.UpdateUser(user);
                    }
                    return Result.OK;
                }
                else return res;

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
                Result res = securityManager.AuthenticateAdmin(adminUsername, adminPassword)
                                | policyManager.RemoveModerator(usernameToRemove, subforum);
                if (res == Result.OK)
                {
                    dataManager.GetSubforum(subforum).ModeratorsList.Remove(usernameToRemove);
                    bool moderator = CheckIfModerator(usernameToRemove);
                    if (!moderator)
                    {
                        User user = dataManager.GetUser(usernameToRemove);
                        user.Level = AuthorizationLevel.MEMBER;
                        dataManager.UpdateUser(user);
                    }
                    return Result.OK;
                }
                else return res;
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

        public Result ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {

            try
            {
                log.Info("got request to replace moderator" + usernameToRemove + " with " + usernameToAdd);

                return policyManager.ChangeModerator(usernameToRemove, usernameToAdd, subforum)
                        | RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum)
                        | AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);

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

                Result res = securityManager.AuthenticateAdmin(adminUsername, adminPassword);
                if (res == Result.OK)
                    if (dataManager.AddSubforum(new Subforum(subforumName)))
                        return Result.OK;
                    else return Result.ENTRY_EXISTS;
                else return res;

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

                Result res = securityManager.AuthenticateAdmin(adminUsername, adminPassword);
                if (res == Result.OK)
                    if (dataManager.RemoveSubforum(subforumName))
                        return Result.OK;
                    else return Result.SUB_FORUM_NOT_FOUND;
                else return res;

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

                Result res = securityManager.AuthenticateAdmin(oldAdminUsername, oldAdminPassword);
                if (res == Result.OK)
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
                    if (dataManager.SetAdmin(newAdmin))
                        return Result.OK;
                    else return Result.ENTRY_EXISTS;
                }
                else return res;

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

                if (securityManager.AuthenticateAdmin(adminUsername, adminPassword) == Result.OK)
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

                if (securityManager.AuthenticateAdmin(adminUsername, adminPassword) == Result.OK)
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