using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.Security
{
    interface ISecurityManager
    {

        bool authorizedLogin(string username);

         bool authorizedLogout(string username);

         bool isLoggedin(string username);

         bool isAuthorizedToPost(string username, string subforum);

         bool isAuthorizedToEdit(string username, Postkey post);

         bool isAuthorizedToEditSubforum(string username);


    }
}
