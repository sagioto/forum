using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Threading.Tasks;
using ForumUtils.SharedDataTypes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;
using ForumServer.DataLayer;

namespace ForumServer.Policy
{
    public class PolicyManager : IPolicyManager
    {
        private IDataManager dataManager;

        public PolicyManager(IDataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Result AddModerator(string username, string subforum)
        {
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);
            if (user != null && forum != null)
            {
                int numOfPublishedPosts = dataManager.GetSubforum(subforum).Posts.Values.
                    Where(post => post.Key.Username.Equals(username) && post.Subforum.Equals(subforum)).Count();
                if (numOfPublishedPosts >= 5)
                    return Result.OK;
            }
            return Result.POLICY_REJECTED;
        }

        public Result RemoveModerator(string username, string subforum)
        {
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);

            if (user != null && forum != null)
            {
                int numOfPublishedPostsInLastHour = dataManager.GetSubforum(subforum).Posts.Values
                    .Where(post => post.Key.Username.Equals(username)
                    && post.Subforum.Equals(subforum)
                    && DateTime.Now.Subtract(post.Key.Time) < TimeSpan.FromHours(1)).Count();
                if (numOfPublishedPostsInLastHour == 0)
                    return Result.OK;
            }
            return Result.POLICY_REJECTED;
        }

        public Result ChangeModerator(string oldUsername, string newUsername, string subforum)
        {
            return Result.OK;
        }

        public Result IsAuthorizedToEdit(Postkey originalPostKey, string username)
        {
            return Result.OK;
        }

        public bool ShouldNotify(Post post, string username, string subforum)
        {
            return (post.Key.Username != username) && ((post.Subforum.Equals(subforum) || Participated(username, post));
        }

        private bool Participated(string username, Post post)
        {
            if (post.ParentPost == null)
            {
                return false;
            }
            else
            {
                if (post.ParentPost.Username.Equals(username))
                {
                    return true;
                }
                else
                {
                    return Participated(username, dataManager.GetPost(post.ParentPost));
                }
            }
        }

        public Result IsAuthorizedActivate(string username)
        {
            return CheckUserState(username, UserState.NotActive);
        }

        private Result CheckUserState(string username, UserState state)
        {
            User user = dataManager.GetUser(username);
            if (user == null)
                return Result.USER_NOT_FOUND;
            else if (user.CurrentState == state)
                return Result.OK;
            else return Result.POLICY_REJECTED;
        }

        public Result IsAuthorizedDeactivate(string username)
        {
            return CheckUserState(username, UserState.Active);
        }

        public Result ShouldBeBanned(string username)
        {
            return CheckUserState(username, UserState.ShouldBeBanned);
        }
    }
}