using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;
using ForumServer.DataLayer;

namespace ForumServer.Security
{
    public class SecurityManager : ISecurityManager
    {
        private IDataManager dataManager;

        public SecurityManager(IDataManager dataManagerArg)
        {
            this.dataManager = dataManagerArg;
        }


        public Result AuthorizedRegister(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user != null)
                return Result.SECURITY_ERROR;
            else
            {
                user = new User(username, password);
                user.Level = AuthorizationLevel.MEMBER;
                dataManager.AddUser(user);
                return Result.OK;
            }
        }

        public Result AuthorizedLogin(string username, string password)
        {
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            if (user.Password.Equals(password)
                /*&& user.CurrentState == UserState.Logout //Trying to enable multiple logins*/)
            {
                user.CurrentState = UserState.Login;
                dataManager.UpdateUser(user);
                return Result.OK;
            }
            else return Result.SECURITY_ERROR;
        }

        public Result AuthorizedLogout(string username)
        {
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            if (IsUserLoggendIn(user))
            {
                user.CurrentState = UserState.Logout;
                dataManager.UpdateUser(user);
                return Result.OK;
            }
            else return Result.SECURITY_ERROR;
        }

        public bool IsLoggedin(string username)
        {
            User user = dataManager.GetUser(username);
            if (user == null)
                return false;
            return IsUserLoggendIn(user);
        }


        public Result IsAuthorizedToPost(string username, string subforum)
        {   
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            if (!user.Level.Equals(AuthorizationLevel.GUEST)
                && IsUserLoggendIn(user))
                return Result.OK;
            return Result.INSUFFICENT_PERMISSIONS;
        }

        public Result IsAuthorizedToEdit(string username, Postkey postkey, string password)
        {
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            Post post = dataManager.GetPost(postkey);
            if (user == null)
                return Result.POST_NOT_FOUND;
            Subforum sub = dataManager.GetSubforums().Find(subforum => subforum.Name.Equals(post.Subforum));
            if(user.Password.Equals(password)
               && (post.Key.Username.Equals(username)
                    || (user.Level.Equals(AuthorizationLevel.MODERATOR)
                        && sub != null && sub.ModeratorsList.Contains(username))
                    || (user.Level.Equals(AuthorizationLevel.ADMIN))))
                return Result.OK;
            return Result.INSUFFICENT_PERMISSIONS;
        }

        public Result IsAuthorizedToEditSubforums(string username)
        {
            //TODO check if this is the right condition
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            if (user.Level.Equals(AuthorizationLevel.ADMIN))
                return Result.OK;
            return Result.INSUFFICENT_PERMISSIONS;
        }


        public Result AuthenticateAdmin(string username, string password)
        {
            User admin = dataManager.GetAdmin();
            if (admin == null)
                return Result.USER_NOT_FOUND;
            if (admin.Password.Equals(password) && admin.Username.Equals(password))
                return Result.OK;
            return Result.INSUFFICENT_PERMISSIONS;
        }

        private static bool IsUserLoggendIn(User user)
        {
            return user != null && user.CurrentState.Equals(UserState.Login);
        }



    }
}