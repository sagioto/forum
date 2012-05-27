using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ForumShared.ForumAPI
{
    [DataContract]
    [Flags]
    public enum Result
    {
        NULL = 0x0000,
        OK = 0x0001,
        USER_NOT_FOUND = 0x0002,
        POST_NOT_FOUND = 0x0004,
        SUB_FORUM_NOT_FOUND = 0x0010,
        ENTRY_EXISTS = 0x0020,
        INSUFFICENT_PERMISSIONS = 0x0040,
        ADMIN_PERMISSIONS_NEEDED = 0x0100,
        SECURITY_ERROR = 0x0200,
        POLICY_REJECTED = 0x0400,
        ILLEGAL_POST = 0x1000
    }
}
