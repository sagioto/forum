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
        /// <returns></returns>
        [OperationContract]
        string Enter();

        [OperationContract]
         bool Register(String username, String password);

        [OperationContract]
         bool Login(String username, String password);

        [OperationContract]
         bool Logout(String username);

        [OperationContract]
         string GetSubforumsList(string subforum);

        [OperationContract]
         string GetSubforum(string subforum);

        /// <summary>
        /// postkey contains of username + timestamp 
        /// </summary>
        /// <param name="postkey"></param>
        /// <returns></returns>
        [OperationContract]
         string GetPost(string postkey);

        [OperationContract]
         bool Post(string current, string toPost);

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
