using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using ForumClientCore.ForumService;

namespace ForumClientCore.Network
{
    [CallbackBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    class ClientNetworkListener : ForumService.IForumServiceCallback
    {

        // Event setting
        public delegate void OnUpdate(string text);
        public event OnUpdate OnUpdateFromServer;


        /// <summary>
        /// This methos is called by Server
        /// </summary>
        /// <param name="message"></param>
        /// <param name="timestamp"></param>
        public void onUpdate(string message, DateTime timestamp)
        {
            // Invoking OnUpdateFromServer event - ClientNetworkAdaptor will be notified
            OnUpdateFromServer(message + "  ,   " + timestamp.ToShortDateString() + "/" + timestamp.ToLongTimeString());
        }

    }
}
