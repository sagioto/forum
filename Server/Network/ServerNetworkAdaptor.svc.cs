using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.Network;

namespace ForumServer
{
    public class ServerNetworkAdaptor : IForumService
    {

        private static List<IForumListener> subscribers = new List<IForumListener>();

        /// <summary>
        /// Subscribe to forum
        /// </summary>
        /// <returns></returns>
        public bool SubscribeToForum()
        {
            try
            {
                IForumListener listener = OperationContext.Current.GetCallbackChannel<IForumListener>();
                if (!subscribers.Contains(listener))
                    subscribers.Add(listener);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Unsubscribe from forum
        /// </summary>
        /// <returns></returns>
        public bool UnsubscribeFromForum()
        {
            try
            {
                IForumListener callback = OperationContext.Current.GetCallbackChannel<IForumListener>();
                if (subscribers.Contains(callback))
                    subscribers.Remove(callback);
                return true;
            }
            catch
            {
                return false;
            }
        }


        /// <summary>
        /// Add message to forum. TODO - Should be changes to Post
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public bool AddMessage(string message)
        {
            foreach (IForumListener listener in subscribers)
            {
                if ((((ICommunicationObject)listener).State == CommunicationState.Opened))
                {
                    try
                    {
                        listener.onUpdate("Update From Server: " + message, DateTime.Now);
                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                else
                {
                    subscribers.Remove(listener);
                }
            }
            return true;
        }


        #region Temp methods

        public string GetData(int value)
        {
            return string.Format("You sent to server: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        #endregion
    }
}
