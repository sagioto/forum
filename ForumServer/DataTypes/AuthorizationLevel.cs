using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumServer.DataTypes
{
    public enum AuthorizationLevel
    {
        GUEST,      /*Not Registered*/
        MEMBER,     /*Registeres*/
        MODERATOR,  /*Subforum admin*/
        ADMIN       /*Forum admin*/
    }
}
