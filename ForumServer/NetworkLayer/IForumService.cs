﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using ForumServer.NetworkLayer;
using System.Web.Script.Serialization;

namespace ForumServer
{
    /// <summary>
    /// Server interface
    /// </summary>
    [ServiceContract(CallbackContract = typeof(IForumListener))]
    public interface IForumService
    {

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Register(String username, String password);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Login(String username, String password);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Logout(String username);

        /// <summary>
        /// Returns subforums list
        /// </summary>s
        /// <returns>returns a json of an array of the sub forum</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetSubforumsList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subforumName">just the name - no json!</param>
        /// <returns>returns a json of the requested sub-forum</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetSubforum(string subforum);

        /// <summary>
        /// postkey contains of user + timestamp in json form
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post json</returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetPost(string postkey);

        /// <summary>
        /// add toPost to a sub forum
        /// </summary>
        /// <param name="current">the sub forum name - no json!</param>
        /// <param name="toPost">the post to add json</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Post(string current, string toPost);

        /// <summary>
        /// add oldPost as reply to current oldPost
        /// </summary>
        /// <param name="current">the postkey json of the post to reply to</param>
        /// <param name="toPost">the reply post in json form</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool Reply(string current, string toPost);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postToUpdate">json of the new post</param>
        /// <param name="originalPost">json of the original postkey</param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool EditPost(string postToUpdate, string originalPost,string usrname, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postkey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool RemovePost(string postkey,string username, string password);

        #region admin functions
        
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool AddSubforum(string adminUsername, string adminPassword, string subforumName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        int ReportUserTotalPosts(string adminUsername, string adminPassword, string username);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword);

        #endregion

        #region not used
        /// <summary>
        /// Not used in Version 2.0.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string Subscribe();

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool AddMessage(string message);

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool SubscribeToForum();

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        [FaultContract(typeof(Exception))]
        bool UnsubscribeFromForum();



        // The following methods are only for debugg:
        [OperationContract]
        [FaultContract(typeof(Exception))]
        string GetData(int value);

        [OperationContract]
        [FaultContract(typeof(Exception))]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        #endregion
    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get
            {
                return boolValue;
            }
            set
            {
                boolValue = value;
            }
        }

        [DataMember]
        public string StringValue
        {
            get
            {
                return stringValue;
            }
            set
            {
                stringValue = value;
            }
        }
    }
}
