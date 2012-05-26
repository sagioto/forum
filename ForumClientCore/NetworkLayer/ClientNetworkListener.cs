using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumClientCore.ForumService;
//using ForumShared.SharedDataTypes;

namespace ForumClientCore.NetworkLayer
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class ClientNetworkListener
    {

        // Event setting
        public delegate void OnUpdate(Post p);
        public event OnUpdate OnUpdateFromServer;


        /// <summary>
        /// This method is called by Server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timestamp"></param>
        public void onUpdate(Post message)
        {
            // Invoking OnUpdateFromController event - ClientNetworkAdaptor will be notified
            OnUpdateFromServer(message);
        }
    }
}
