﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.DataTypes;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;
using System.ServiceModel.Activation;

namespace ForumServer
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    public class ServerNetworkAdaptor : IForumService
    {
        private static ServerController controller = new ServerController();

        #region user functions

       public Result Register(String username, String password)
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

        public Result Login(String username, String password)
        {

            try
            {
                return controller.Login(username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result Logout(String username)
        {

            try
            {
                return controller.Logout(username);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public int GetNumOfLoggedInUsers()
        {

            try
            {
                return controller.GetNumOfLoggedInUsers();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }


        public Post Subscribe(string username)
        {
            try
            {
                return controller.Subscribe(username);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }
        }
        #endregion

        #region viewing functions

        public string[] GetSubforumsList()
        {
            try
            {
                return controller.GetSubforumsList();
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
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
                throw new FaultException<Exception>(e, e.Message);
            }
        }

        public Post GetPost(Postkey postkey)
        {
            try
            {
                return controller.GetPost(postkey);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
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
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        #endregion

        #region posting functions

        public Result Post(string currentSubforum, Post toPost)
        {
            try
            {
                toPost.Replies = new Dictionary<Postkey, Post>();
                return controller.Post(currentSubforum, toPost);

            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }
        }

        public Result Reply(Postkey current, Post toPost)
        {
            try
            {
                toPost.Replies = new Dictionary<Postkey, Post>();
                return controller.Reply(current, toPost);
          
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result EditPost(Postkey oldPost, Post newPost, string username, string password)
        {
            try
            {
                return controller.EditPost(oldPost, newPost, username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }


        public Result RemovePost(Postkey postkey, string username, string password)
        {

            try
            {
                return controller.RemovePost(postkey, username, password);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        #endregion

        #region admin functions

        public Result AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum)
        {

            try
            {
                return controller.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum)
        {

            try
            {
                return controller.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum)
        {

            try
            {
                return controller.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result AddSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                return controller.AddSubforum(adminUsername, adminPassword, subforumName);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result RemoveSubforum(string adminUsername, string adminPassword, string subforumName)
        {

            try
            {
                return controller.RemoveSubforum(adminUsername, adminPassword, subforumName);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
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
                throw new FaultException<Exception>(e, e.Message);
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
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        public Result ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
        {

            try
            {
                return controller.ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, e.Message);
            }

        }

        #endregion
    }
}