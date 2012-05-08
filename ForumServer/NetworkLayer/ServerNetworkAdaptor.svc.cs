﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.NetworkLayer;
using ForumServer.DataTypes;

namespace ForumServer
{
    public class ServerNetworkAdaptor : IForumService
    {
        private static ServerController controller = new ServerController();
        private ISerializer serializer = new JsonSerializer();
        
        public string Enter()
        {
            return serializer.Serialize(controller.Enter());
        }

        public bool Register(String username, String password)
        {
            return controller.Register(username, password);
        }

        public bool Login(String username, String password)
        {
            return controller.Login(username, password);
        }

        public bool Logout(String username)
        {
            return controller.Logout(username);
        }

        public string GetSubforumsList(string subforum)
        {
            return serializer.Serialize(controller.Enter());
        }

        public string GetSubforum(string subforum)
        {
            return serializer.SerializeSubforum(controller.GetSubForum(subforum));
        }


        public string GetPost(string postkey)
        {
            Postkey toGet = serializer.DeserializePostkey(postkey);
            return serializer.SerializePost(controller.GetPost(toGet));
        }


        public bool Post(string currentSubforum, string toPost)
        {
            Post toAdd = serializer.DeserializePost(toPost);
            return controller.Post(currentSubforum, toAdd);
        }

        public bool Reply(string current, string toPost)
        {
            Postkey currentKey = serializer.DeserializePostkey(current);
            Post toAdd= serializer.DeserializePost(toPost);
            return controller.Reply(currentKey, toAdd);
        }

        public bool EditPost(string postToUpdate, string originalPost, string username, string password)
        {
            Postkey originalPostKey = serializer.DeserializePostkey(originalPost);
            Post toUpdate = serializer.DeserializePost(postToUpdate);
            return controller.EditPost(originalPostKey, toUpdate, password);
        }


        public bool RemovePost(string postkey, string username, string password)
        {
            Postkey originalPostKey = serializer.DeserializePostkey(postkey);
            return controller.RemovePost(originalPostKey, password);
        }

        #region admin functions

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public string ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            throw new NotImplementedException();
        }

        public string ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            throw new NotImplementedException();
        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            throw new NotImplementedException();
        }

        #endregion

        public string Subscribe()
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