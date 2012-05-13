using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumUtils.SharedDataTypes;

namespace ForumServer.NetworkLayer
{
    /// <summary>
    /// This interface is used for defining client object which gets callbacks from server
    /// </summary>
    public interface IForumListener
    {
        [OperationContract(IsOneWay = true)]
        void onUpdate(Post message);
        

    }
}
