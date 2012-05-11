﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumUtils.NetworkLayer;
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

        public string GetSubforumsList()
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

        public bool EditPost(string postToUpdate, string originalPost, string password)
        {
            Postkey originalPostKey = serializer.DeserializePostkey(originalPost);
            Post toUpdate = serializer.DeserializePost(postToUpdate);
            return controller.EditPost(originalPostKey, toUpdate, originalPostKey.Username, password);
        }


        public bool RemovePost(string postkey, string password)
        {
            Postkey originalPostKey = serializer.DeserializePostkey(postkey);
            return controller.RemovePost(originalPostKey, originalPostKey.Username , password);
        }

        #region admin functions

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {
            return controller.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {
            return controller.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {
            return controller.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return controller.AddSubforum(adminUsername, adminPassword, subforumName);
        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {
            return controller.RemoveSubforum(adminUsername, adminPassword, subforumName);
        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {
            return controller.ReportSubForumTotalPosts(adminUsername, adminPassword, subforumName);
        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {
            return controller.ReportUserTotalPosts(adminUsername, adminPassword, username);
        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {
            return controller.ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
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






        public bool EditPost(string postToUpdate, string originalPost, string usrname, string password)
        {
            throw new NotImplementedException();
        }

        public bool RemovePost(string postkey, string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}