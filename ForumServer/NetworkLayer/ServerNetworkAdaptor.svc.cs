﻿﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumUtils.NetworkLayer;
using ForumServer.NetworkLayer;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ForumServer
{
    public class ServerNetworkAdaptor : IForumService
    {
        private static ServerController controller = new ServerController();
        private ISerializer serializer = new JsonSerializer();
        
        public bool Register(String username, String password)
        {
            try
            {
                return controller.Register(username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't register");
            }

        }

        public bool Login(String username, String password)
        {

            try
            {
                return controller.Login(username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't login");
            }

        }

        public bool Logout(String username)
        {

            try
            {
                return controller.Logout(username);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't logout");
            }

        }

        public string[] GetSubforumsList()
        {

            try
            {
                return controller.GetSubforumsList();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get sub forums list");
            }

        }

        public Post[] GetSubforum(string subforum)
        {
            try
            {
                return controller.GetSubForum(subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get sub forum");
            }
        }


        public Post[] GetReplies(Postkey postkey)
        {
            try
            {
                return controller.GetReplies(postkey);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get post");
            }

        }


        public bool Post(string currentSubforum, Post toPost)
        {
            try
            {
                return controller.Post(currentSubforum, toPost);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "somthing went wrong with post");
            }

        }

        public bool Reply(Postkey current, Post toPost)
        {
            try
            {
                return controller.Reply(current, toPost);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "somthing went wrong with reply");
            }

        }

        public bool EditPost(Postkey postToUpdate, Post originalPost, string username, string password)
        {
            try
            {
                return controller.EditPost(postToUpdate, originalPost, username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "somthing went wrong with edit");
            }

        }


        public bool RemovePost(Postkey postkey, string username, string password)
        {

            try
            {
                return controller.RemovePost(postkey, username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "somthing went wrong with remove");
            }

        }

        #region admin functions

        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {

            try
            {
                return controller.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't add moderator");
            }

        }

        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {

            try
            {
                return controller.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't remove moderator");
            }

        }

        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {

            try
            {
                return controller.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't replace moderator");
            }

        }

        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                return controller.AddSubforum(adminUsername, adminPassword, subforumName);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't add sub forum");
            }

        }

        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                return controller.RemoveSubforum(adminUsername, adminPassword, subforumName);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't remove sub forum");
            }

        }

        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                return controller.ReportSubForumTotalPosts(adminUsername, adminPassword, subforumName);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get report");
            }

        }

        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username)
        {

            try
            {
                return controller.ReportUserTotalPosts(adminUsername, adminPassword, username);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get report");
            }

        }

        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {

            try
            {
                return controller.ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't replace admin");
            }

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