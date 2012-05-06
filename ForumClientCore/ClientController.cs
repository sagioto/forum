using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumClientCore.NetworkLayer;
using System.ServiceModel;

namespace ForumClientCore
{
    public class ClientController
    {
        ClientNetworkAdaptor netAdaptor;  

        public event ClientNetworkAdaptor.OnUpdate OnUpdateFromServer;  //Event to be invoked when getting a notify by NetworkAdaptor

        /// <summary>
        /// Constructor
        /// </summary>
        public ClientController()
        {
            netAdaptor = new ClientNetworkAdaptor();
            netAdaptor.OnUpdateFromServer += new ClientNetworkAdaptor.OnUpdate(netAdaptor_OnUpdateFromServer);
        }


        /// <summary>
        /// This method is an example of using NetworkAdaptor
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public string getDataFromServer(int num)
        {
            return netAdaptor.getDataFromServer(num);
        }


        /// <summary>
        /// When netAdaptor invoked an event OnUpdateFromServer it gets to this method
        /// </summary>
        /// <param name="message"></param>
        public void netAdaptor_OnUpdateFromServer(string message)
        {
            OnUpdateFromServer(message);    // Invoking an event - will notify evryone who sleep on it
        }


        public void AddMessage(string s)
        {
            netAdaptor.addMessage(s);
        }

    }
}
