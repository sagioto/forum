using System;
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

        /// <summary>
        /// Returns subforums list
        /// </summary>
        /// <param name="subforumName"></param>
        /// <returns>returns a json of an array of the sub forum</returns>
        [OperationContract]
        string Enter();


        [OperationContract]
        bool Register(String username, String password);

        [OperationContract]
        bool Login(String username, String password);

        [OperationContract]
        bool Logout(String username);

        /// <summary>
        /// Returns subforums list
        /// </summary>s
        /// <returns>returns a json of an array of the sub forum</returns>
        [OperationContract]
        string GetSubforumsList();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subforumName">just the name - no json!</param>
        /// <returns>returns a json of the requested sub-forum</returns>
        [OperationContract]
        string GetSubforum(string subforum);

        /// <summary>
        /// postkey contains of user + timestamp in json form
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns>post json</returns>
        [OperationContract]
        string GetPost(string postkey);

        /// <summary>
        /// add toPost to a sub forum
        /// </summary>
        /// <param name="current">the sub forum name - no json!</param>
        /// <param name="toPost">the post to add json</param>
        /// <returns></returns>
        [OperationContract]
        bool Post(string current, string toPost);

        /// <summary>
        /// add oldPost as reply to current oldPost
        /// </summary>
        /// <param name="current">the postkey json of the post to reply to</param>
        /// <param name="toPost">the reply post in json form</param>
        /// <returns></returns>
        [OperationContract]
        bool Reply(string current, string toPost);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postToUpdate">json of the new post</param>
        /// <param name="originalPost">json of the original postkey</param>
        /// <returns></returns>
        bool EditPost(string postToUpdate, string originalPost,string usrname, string password);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postkey"></param>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool RemovePost(string postkey,string username, string password);

        #region admin functions

        bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum);

        bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum);

        bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum);

        bool AddSubforum(string adminUsername, string adminPassword, string subforumName);

        bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName);

        int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName);

        int ReportUserTotalPosts(string adminUsername, string adminPassword, string username);

        bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword);

        #endregion

        #region not used
        /// <summary>
        /// Not used in Version 2.0.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        string Subscribe();

        /// <summary>
        /// Not used
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        [OperationContract]
        bool AddMessage(string message);

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool SubscribeToForum();

        /// <summary>
        /// Not used
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        bool UnsubscribeFromForum();



        // The following methods are only for debugg:
        [OperationContract]
        string GetData(int value);

        [OperationContract]
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
