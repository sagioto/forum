﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.NetworkLayer;
using System.Web.Script.Serialization;
using ForumUtils.SharedDataTypes;

namespace ForumServer
{
    /// <summary>
    /// Server interface
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IForumListener))]
    public interface IForumService
    {
        #region user functions
        /// <summary>
        /// register the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result Register(String username, String password);

        /// <summary>
        /// login the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result Login(String username, String password);

        /// <summary>
        /// logout the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result Logout(String username);

        /// <summary>
        /// subscribe the user for notifications
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Post Subscribe(String username);


        #endregion

        #region viewing functions
        /// <summary>
        /// Returns subforums list
        /// </summary>s
        /// <returns>returns array of the sub forum</returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        string[] GetSubforumsList();

        /// <summary>
        /// get the posts of the requsted sub forum
        /// </summary>
        /// <param name="subforumName">just the name</param>
        /// <returns>returns the posts of the requested sub forum</returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Post[] GetSubforum(string subforum);


        /// <summary>
        /// get post object of the provided postkey
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Post GetPost(Postkey postkey);

        /// <summary>
        /// get the replies of the post with postkey
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Post[] GetReplies(Postkey postkey);

        #endregion

        #region posting functions

        /// <summary>
        /// post the provieded post on the sub forum
        /// </summary>
        /// <param name="current">the sub forum name</param>
        /// <param name="toPost">the post to add </param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result Post(string current, Post toPost);

        /// <summary>
        /// post the provieded post as a reply on the current post
        /// </summary>
        /// <param name="current">the postkey  of the post to reply to</param>
        /// <param name="toPost">the reply post in  form</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result Reply(Postkey current, Post toPost);

        /// <summary>
        /// edit the post
        /// </summary>
        /// <param name="postToUpdate">post key to update</param>
        /// <param name="originalPost"> of the original postkey</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result EditPost(Postkey oldPost, Post newPost, string usrname, string password);

        /// <summary>
        /// remove the post
        /// </summary>
        /// <param name="postkey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result RemovePost(Postkey postkey, string username, string password);

        #endregion

        #region admin functions
        /// <summary>
        /// add a moderator to a sub forum
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="usernameToAdd"></param>
        /// <param name="subforum"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum);

        /// <summary>
        /// remove a moderator to a sub forum
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="usernameToRemove"></param>
        /// <param name="subforum"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum);

        /// <summary>
        /// replace a moderator to a sub forum
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="usernameToAdd"></param>
        /// <param name="usernameToRemove"></param>
        /// <param name="subforum"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum);

        /// <summary>
        /// add a subforum
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="subforumName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result AddSubforum(string adminUsername, string adminPassword, string subforumName);


        /// <summary>
        /// remove a sub forum
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="subforumName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        Result RemoveSubforum(string adminUsername, string adminPassword, string subforumName);

        /// <summary>
        /// report total messages in sub forums
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="subforumName"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName);

        /// <summary>
        /// report the total posts of a user
        /// </summary>
        /// <param name="adminUsername"></param>
        /// <param name="adminPassword"></param>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        int ReportUserTotalPosts(string adminUsername, string adminPassword, string username);

        /// <summary>
        /// relace the current admin
        /// </summary>
        /// <param name="oldAdminUsername"></param>
        /// <param name="oldAdminPassword"></param>
        /// <param name="newAdminUsername"></param>
        /// <param name="newAdminPassword"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword);

        #endregion


 
    }

    [DataContract]
    [Flags]
    enum Result : byte 
    {
        NULL = 0x00,
        USER_NOT_FOUND = 0x01,
        POST_NOT_FOUND = 0x02,
        SUB_FORUM_NOT_FOUND = 0x04,
        INSUFFICENT_PERMISSIONS = 0x08,
        ADMIN_PERMISSIONS_NEEDED = 0x16
    }

}