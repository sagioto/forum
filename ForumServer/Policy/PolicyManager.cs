using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Threading.Tasks;

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
            //TODO completeSreturn user != null && user.
            if(user != null && forum != null)
            {
              
            }
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string username, string subforum)
        {
            Subforum forum = dataManager.GetSubforum(subforum);
            User user = dataManager.GetUser(username);
            //TODO completeSreturn user != null && user.
            if (user != null && forum != null)
            {

            }
            throw new NotImplementedException();
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