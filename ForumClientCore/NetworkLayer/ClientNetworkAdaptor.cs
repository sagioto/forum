using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.ForumService;
using System.ServiceModel;

namespace ForumClientCore.NetworkLayer
{
    public class ClientNetworkAdaptor
    {
        IForumService webService;
        ClientNetworkListener netListener;

        // Event setting:
        public delegate void OnUpdate(string text);
        public event OnUpdate OnUpdateFromServer;

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientNetworkAdaptor()
        {
            // Network listener settings
            netListener = new ClientNetworkListener();
            netListener.OnUpdateFromServer +=new ClientNetworkListener.OnUpdate(netListener_OnUpdateFromServer); // Register to the update event of netListener
            
            // Web Service settings
            InstanceContext context = new InstanceContext(netListener);     // Sending IntanceContex to Server that it will be able to make callbacks
            webService = new ForumServiceClient(context);
            
            webService.SubscribeToForum();  // Subscribing to Forum in order to get callbacks
        }

        /// <summary>
        /// Destructor
        /// </summary>
        /// <remarks>
        /// TODO - Need to find a way to do unsubscribe when being destroyed. Not sure that destructor is the right way... 
        /// </remarks>
        ~ClientNetworkAdaptor()
        {
            webService.UnsubscribeFromForum();
        }

        /// <summary>
        /// Temp method - just to test connection to ws. To be deleted.
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string getDataFromServer(int num)
        {
            return webService.GetData(num);
        }

        
        /// <summary>
        /// Need to be changed to 'Post'
        /// </summary>
        /// <param name="s"></param>
        internal void addMessage(string s)
        {
            try
            {
                webService.AddMessage(s);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        /// <summary>
        /// Will be called when netListener will invoke its update event
        /// </summary>
        /// <param name="message"></param>
        public void netListener_OnUpdateFromServer(string message)
        {
            OnUpdateFromServer(message);    // Invoke event OnUpdateFronServer - will be notify controller
        }

    }
}
