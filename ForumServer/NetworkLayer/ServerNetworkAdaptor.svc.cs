﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ForumServer
{
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
                throw new FaultException<Exception>(e, "couldn't login");
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
                throw new FaultException<Exception>(e, "couldn't logout");
            }

        }

        public Post Subscribe(string username)
        {
            throw new NotImplementedException();
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

        public Post GetPost(Postkey postkey)
        {
            try
            {
                return controller.GetPost(postkey);
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "couldn't get post");
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

        #endregion

        #region posting functions

        public Result Post(string currentSubforum, Post toPost)
        {
            try
            {
                return controller.Post(currentSubforum, toPost);

            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "something went wrong with post");
            }

        }

        public Result Reply(Postkey current, Post toPost)
        {
            try
            {
                return controller.Reply(current, toPost);
          
            }
            catch (Exception e)
            {
                throw new FaultException<Exception>(e, "something went wrong with reply");
            }

        }

        public Result EditPost(Postkey postToUpdate, Post originalPost, string username, string password)
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


        public Result RemovePost(Postkey postkey, string username, string password)
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
                throw new FaultException<Exception>(e, "couldn't add moderator");
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
                throw new FaultException<Exception>(e, "couldn't remove moderator");
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
                throw new FaultException<Exception>(e, "couldn't replace moderator");
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
                throw new FaultException<Exception>(e, "couldn't add sub forum");
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

        public Result ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword)
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


       }
}