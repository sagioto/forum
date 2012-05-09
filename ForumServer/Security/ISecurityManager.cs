using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.Security
{
    interface ISecurityManager
    {

        bool AuthorizedRegister(string username, string password);

        bool AuthorizedLogin(string username, string password);

        bool AuthorizedLogout(string username);

        bool IsLoggedin(string username);

        bool IsAuthorizedToPost(string username, string subforum);

        bool IsAuthorizedToEdit(string username, string post, string password);

        bool IsAuthorizedToEditSubforums(string username);

        bool AuthenticateAdmin(string username, string password);
    }
}
