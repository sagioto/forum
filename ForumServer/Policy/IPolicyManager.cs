using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumUtils.SharedDataTypes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumServer.Policy
{
    interface IPolicyManager
    {
         Result AddModerator(string username, string subforum);

         Result RemoveModerator(string username, string subforum);

         Result ChangeModerator(string oldUsername, string newUsername, string subforum);

         bool ShouldNotify(Post post, string username);

         Result IsAuthorizedToEdit(Postkey originalPostKey, string username);

         Result IsAuthorizedActivate(string username);

         Result IsAuthorizedDeactivate(string username);

         Result ShouldBeBanned(string username);
    }
}
