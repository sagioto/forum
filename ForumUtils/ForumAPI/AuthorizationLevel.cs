using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ForumShared.ForumAPI
{
    [DataContract]
    [Flags]
    public enum AuthorizationLevel
    {
        GUEST = 0x0,      /*Not Registered*/
        MEMBER = 0x1,     /*Registeres*/
        MODERATOR = 0x2,  /*Subforum admin*/
        ADMIN = 0x4       /*Forum admin*/
    }

}
