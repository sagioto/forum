using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.Policy
{
    public class PolicyManager : IPolicyManager
    {
        private DataLayer.DataManager dataManager;

        public PolicyManager(DataLayer.DataManager dataManager)
        {
            // TODO: Complete member initialization
            this.dataManager = dataManager;
        }
        public bool AddModerator(string username, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string username, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool ChangeModerator(string oldUsername, string newUsername, string subforum)
        {
            throw new NotImplementedException();
        }
    }
}