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
        /// <param name="subforum"></param>
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
        /// </summary>
        /// <param name="subforum"></param>
        /// <returns>returns a json of an array of the sub forum</returns>
        [OperationContract]
        string GetSubforumsList(string subforum);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subforum"></param>
        /// <returns>returns a json of the requested sub-forum</returns>
        [OperationContract]
        string GetSubforum(string subforum);

        /// <summary>
        /// postkey contains of username + timestamp 
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns></returns>
        [OperationContract]
        string GetPost(string postkey);

        /// <summary>
        /// add a post to a sub forum
        /// </summary>
        /// <param name="current">the sub forum json</param>
        /// <param name="toPost">the post to add json</param>
        /// <returns></returns>
        [OperationContract]
        bool Post(string current, string toPost);

        /// <summary>
        /// add post as reply to current post
        /// </summary>
        /// <param name="current">the current post postkey json</param>
        /// <param name="toPost">the post to add json</param>
        /// <returns></returns>
        [OperationContract]
        bool Reply(string current, string toPost);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="postToUpdate">json of the post</param>
        /// <param name="originalPost">json of the current postkey</param>
        /// <returns></returns>
        bool EditPost(string postToUpdate, string originalPost);

        bool RemovePost(string postkey);

        #region admin functions

        bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum);

        bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum);

        bool RemoveModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum);

        bool AddSubforum(string adminUsername, string adminPassword, string subforumName);

        bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName);

        string ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName);

        string ReportUserTotalPosts(string adminUsername, string adminPassword, string username);

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
