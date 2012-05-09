using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;

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


        public bool AuthorizedRegister(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user != null)
                return false;
            else
            {
                user = new User(username, password);
                user.Level = AuthorizationLevel.MEMBER;
                dataManager.UpdateUser(user);
                return true;
            }
        }

        public bool AuthorizedLogin(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user != null && user.Password.Equals(password))
            {
                user.CurrentState = UserState.Login;
                dataManager.UpdateUser(user);
                return true;
            }
            else return false;
        }

        public bool AuthorizedLogout(string username)
        {
            User user = dataManager.GetUser(username);
            if (IsUserLoggendIn(user))
            {
                user.CurrentState = UserState.Logout;
                dataManager.UpdateUser(user);
                return true;
            }
            else return false;
        }

        public bool IsLoggedin(string username)
        {
            User user = dataManager.GetUser(username);
            return (IsUserLoggendIn(user));
        }

        
        public bool IsAuthorizedToPost(string username, string subforum)
        {
            //TODO check if sub forum should be considered
            User user = dataManager.GetUser(username);
            return (user != null && user.Level.Equals(AuthorizationLevel.MEMBER) 
                && IsUserLoggendIn(user));
        }

        public bool IsAuthorizedToEdit(string username, Postkey postkey, string password)
        {
            User user = dataManager.GetUser(username);
            Post post = dataManager.GetPost(postkey);
            return user != null && post != null
                && user.Password.Equals(password)
                && (post.Key.Username.Equals(username)
                    || (user.Level.Equals(AuthorizationLevel.MODERATOR) && post.Subforum.ModeratorsList.Contains(username))
                    || (user.Level.Equals(AuthorizationLevel.ADMIN)));                
        }

        public bool IsAuthorizedToEditSubforums(string username)
        {
            //TODO check if this is the right condition
            User user = dataManager.GetUser(username);
            return user != null && user.Level.Equals(AuthorizationLevel.ADMIN);
        }


        public bool AuthenticateAdmin(string username, string password)
        {
            User admin = dataManager.GetAdmin();
            return admin.Password.Equals(password) && admin.Username.Equals(password);
        }

        private static bool IsUserLoggendIn(User user)
        {
            return user != null && user.CurrentState.Equals(UserState.Login);
        }



    }
}