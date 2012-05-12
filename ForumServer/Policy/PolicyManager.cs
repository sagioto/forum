using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Threading.Tasks;
using ForumUtils.SharedDataTypes;

namespace ForumServer.Policy
{
    public class PolicyManager : IPolicyManager
    {
        private DataLayer.DataManager dataManager;

        public PolicyManager(DataLayer.DataManager dataManager)
        {
            this.dataManager = dataManager;
        }
        public bool AddModerator(string username, string subforum)
        {  
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);
            if(user != null && forum != null)
            {
            int numOfPublishedPosts = dataManager.GetAllPosts()
                .Where(post => post.Key.Username.Equals(username) && post.Subforum.Equals(subforum)).Distinct().Count();
                if(numOfPublishedPosts >= 5)
                    return true;
            }
            return false;
        }

        public bool RemoveModerator(string username, string subforum)
        {
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);
            
            if (user != null && forum != null)
            {
                int numOfPublishedPostsInLastHour = dataManager.GetAllPosts()
                .Where(post => post.Key.Username.Equals(username) && post.Subforum.Equals(subforum)
                    && DateTime.Now.Subtract(post.Key.Time) < TimeSpan.FromHours(1)).Distinct().Count();
                if (numOfPublishedPostsInLastHour == 0)
                    return true;
                return true;
            }
            return false;
        }

        public bool ChangeModerator(string oldUsername, string newUsername, string subforum)
        {
            return true;
        }

        public bool IsAuthorizedToEdit(Postkey originalPostKey, string username)
        {
            return true;
        }
    }
}