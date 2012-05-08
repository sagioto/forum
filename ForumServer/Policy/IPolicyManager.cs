using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumServer.Policy
{
    interface IPolicyManager
    {
         bool addModerator(string username, string subforum);

         bool removeModerator(string username, string subforum);

         bool changeModerator(string oldUsername, string newUsername, string subforum);
 
    }
}
