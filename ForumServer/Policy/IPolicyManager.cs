using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumUtils.SharedDataTypes;

namespace ForumServer.Policy
{
    interface IPolicyManager
    {
         Result AddModerator(string username, string subforum);

         Result RemoveModerator(string username, string subforum);

         Result ChangeModerator(string oldUsername, string newUsername, string subforum);

         bool ShouldNotify(Post post, string username);
 
    }
}
