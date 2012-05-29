using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Threading.Tasks;
using ForumUtils.SharedDataTypes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumServer.Policy
{
    public class PolicyManager : IPolicyManager
    {
        private DataLayer.DataManager dataManager;

        public PolicyManager(DataLayer.DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public Result AddModerator(string username, string subforum)
        {  
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);
            if(user != null && forum != null)
            {
                int numOfPublishedPosts = dataManager.GetSubforum(subforum).Posts.Values.
                    Where(post => post.Key.Username.Equals(username) && post.Subforum.Equals(subforum)).Count();
                if(numOfPublishedPosts >= 5)
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

        public bool ShouldNotify(Post post, string username)
        {
            return (post.Key.Username != username);
        }

    }
}