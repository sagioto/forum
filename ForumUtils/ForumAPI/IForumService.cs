﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.Script.Serialization;
using ForumShared.SharedDataTypes;

namespace ForumShared.ForumAPI
{
    /// <summary>
    /// Service interface
    /// </summary>
    [ServiceContract]
    public interface IForumService
    {
 
        #region user functions
        /// <summary>
        /// get the number of logged in users.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        int GetNumOfLoggedInUsers();


        /// <summary>
        /// register the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result Register(String username, String password);

        /// <summary>
        /// login the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        AuthorizationLevel Login(String username, String password);

        /// <summary>
        /// logout the user to the system
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result Logout(String username);

        /// <summary>
        /// subscribe the user for notifications
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Post Subscribe(String username);


        #endregion

        #region viewing functions
        /// <summary>
        /// Returns subforums list
        /// </summary>s
        /// <returns>returns array of the sub forum</returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string[] GetSubforumsList();

        /// <summary>
        /// get the posts of the requsted sub forum
        /// </summary>
        /// <param name="subforumName">just the name</param>
        /// <returns>returns the posts of the requested sub forum</returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Post[] GetSubforum(string subforum);


        /// <summary>
        /// get post object of the provided postkey
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Post GetPost(Postkey postkey);

        /// <summary>
        /// get the replies of the post with postkey
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post </returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result Post(string current, Post toPost);

        /// <summary>
        /// post the provieded post as a reply on the current post
        /// </summary>
        /// <param name="current">the postkey  of the post to reply to</param>
        /// <param name="toPost">the reply post in  form</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result Reply(Postkey current, Post toPost);

        /// <summary>
        /// edit the post
        /// </summary>
        /// <param name="postToUpdate">post key to update</param>
        /// <param name="originalPost"> of the original postkey</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result EditPost(Postkey oldPost, Post newPost, string username, string password);

        /// <summary>
        /// remove the post
        /// </summary>
        /// <param name="postkey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(FaultException))]
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
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
        [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped,
            ResponseFormat = WebMessageFormat.Json)]
        Result ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword);

        #endregion

    }

}