using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumServer.DataTypes
{
    public enum UserState
    {
        Login,  // TODO - Delete
        Logout, // TODO - Delete

        Active,
        NotActive,
        Banned,
        ShouldBeBanned
    }
}
