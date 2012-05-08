﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.Security
{
    interface ISecurityManager
    {

        bool AuthorizedLogin(string username);

        bool AuthorizedLogout(string username);

        bool IsLoggedin(string username);

        bool IsAuthorizedToPost(string username, string subforum);

        bool IsAuthorizedToEdit(string username, Postkey post);

        bool IsAuthorizedToEditSubforum(string username);


    }
}