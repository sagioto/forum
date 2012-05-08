using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.Security
{
    public class SecurityManager : ISecurityManager
    {
        private DataLayer.DataManager dataManager;

        public SecurityManager(DataLayer.DataManager dataManager)
        {
            // TODO: Complete member initialization
            this.dataManager = dataManager;
        }


        public bool AuthorizedLogin(string username)
        {
            throw new NotImplementedException();
        }

        public bool AuthorizedLogout(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsLoggedin(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorizedToPost(string username, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorizedToEdit(string username, DataTypes.Postkey post)
        {
            throw new NotImplementedException();
        }

        public bool IsAuthorizedToEditSubforum(string username)
        {
            throw new NotImplementedException();
        }

        internal bool AuthorizedRegister(string username, string password)
        {
            throw new NotImplementedException();
        }

        internal bool AuthorizedLogin(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}