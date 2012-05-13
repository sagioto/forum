using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ForumServer.Security
{
    public class SecurityManager : ISecurityManager
    {
        private DataLayer.DataManager dataManager;

        public SecurityManager(DataLayer.DataManager dataManager)
        {
            this.dataManager = dataManager;

            User admin = dataManager.GetAdmin();
            if (admin == null)
            {
                string adminName = System.Web.Configuration.WebConfigurationManager.AppSettings["adminName"];
                string adminPassword = System.Web.Configuration.WebConfigurationManager.AppSettings["adminPassword"];
                admin = new User(adminName, adminPassword);
                admin.Level = AuthorizationLevel.ADMIN;
                dataManager.SetAdmin(admin);
            }
        }


        public Result AuthorizedRegister(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user != null)
                return false;
            else
            {
                user = new User(username, password);
                user.Level = AuthorizationLevel.MEMBER;
                dataManager.UpdateUser(user);
                return Result.OK;
            }
        }

        public Result AuthorizedLogin(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user != null && user.Password.Equals(password)
                && user.CurrentState == UserState.Logout)
            {
                user.CurrentState = UserState.Login;
                dataManager.UpdateUser(user);
                return Result.OK;
            }
            else return false;
        }

        public Result AuthorizedLogout(string username)
        {
            User user = dataManager.GetUser(username);
            if (IsUserLoggendIn(user))
            {
                user.CurrentState = UserState.Logout;
                dataManager.UpdateUser(user);
                return Result.OK;
            }
            else return false;
        }

        public Result IsLoggedin(string username)
        {
            User user = dataManager.GetUser(username);
            return (IsUserLoggendIn(user));
        }


        public Result IsAuthorizedToPost(string username, string subforum)
        {
            //TODO check if sub forum should be considered
            User user = dataManager.GetUser(username);
            return (user != null && user.Level.Equals(AuthorizationLevel.MEMBER) 
                && IsUserLoggendIn(user));
        }

        public Result IsAuthorizedToEdit(string username, Postkey postkey, string password)
        {
            User user = dataManager.GetUser(username);
            Post post = dataManager.GetPost(postkey);
            Subforum sub = dataManager.GetSubforums().Find(subforum => subforum.Name.Equals(post.Subforum));
            return user != null && post != null
                && user.Password.Equals(password)
                && (post.Key.Username.Equals(username)
                    || (user.Level.Equals(AuthorizationLevel.MODERATOR)
                        && sub != null && sub.ModeratorsList.Contains(username))
                    || (user.Level.Equals(AuthorizationLevel.ADMIN)));                
        }

        public Result IsAuthorizedToEditSubforums(string username)
        {
            //TODO check if this is the right condition
            User user = dataManager.GetUser(username);
            return user != null && user.Level.Equals(AuthorizationLevel.ADMIN);
        }


        public Result AuthenticateAdmin(string username, string password)
        {
            User admin = dataManager.GetAdmin();
            return admin.Password.Equals(password) && admin.Username.Equals(password);
        }

        private static Result IsUserLoggendIn(User user)
        {
            return user != null && user.CurrentState.Equals(UserState.Login);
        }



    }
}