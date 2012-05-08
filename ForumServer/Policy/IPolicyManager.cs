using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumServer.Policy
{
    interface IPolicyManager
    {
         bool AddModerator(string username, string subforum);

         bool RemoveModerator(string username, string subforum);

         bool ChangeModerator(string oldUsername, string newUsername, string subforum);
 
    }
}
