using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.NetworkLayer;

namespace ForumServer
{
    public class ServerNetworkAdaptor : IForumService
    {
        public string Enter()
        {
            throw new NotImplementedException();
        }

        public bool register(String username, String password)
        {
            throw new NotImplementedException();
        }

        public bool login(String username, String password)
        {
            throw new NotImplementedException();
        }

        public bool logout(String username)
        {
            throw new NotImplementedException();
        }

        public string getSubforumsList(string subforum)
        {
            throw new NotImplementedException();
        }

        public string getSubforum(string subforum)
        {
            throw new NotImplementedException();
        }


        public string getPost(string postkey)
        {
            throw new NotImplementedException();
        }


        public bool post(string current, string toPost)
        {
            throw new NotImplementedException();
        }


        public string subscribe()
        {
            throw new NotImplementedException();
        }

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
