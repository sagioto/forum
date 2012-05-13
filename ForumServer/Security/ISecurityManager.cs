using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ForumServer.Security
{
    interface ISecurityManager
    {

        Result AuthorizedRegister(string username, string password);

        Result AuthorizedLogin(string username, string password);

        Result AuthorizedLogout(string username);

        bool IsLoggedin(string username);

        Result IsAuthorizedToPost(string username, string subforum);

        Result IsAuthorizedToEdit(string username, Postkey post, string password);

        Result IsAuthorizedToEditSubforums(string username);

        Result AuthenticateAdmin(string username, string password);
    }
}
