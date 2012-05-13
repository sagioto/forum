﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17379
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ForumClientConsole.ForumService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ForumService.IForumService", CallbackContract=typeof(ForumClientConsole.ForumService.IForumServiceCallback))]
    public interface IForumService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Register", ReplyAction="http://tempuri.org/IForumService/RegisterResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/RegisterFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool Register(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Login", ReplyAction="http://tempuri.org/IForumService/LoginResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/LoginFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool Login(string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Logout", ReplyAction="http://tempuri.org/IForumService/LogoutResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/LogoutFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool Logout(string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetSubforumsList", ReplyAction="http://tempuri.org/IForumService/GetSubforumsListResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetSubforumsListFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        string[] GetSubforumsList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetSubforum", ReplyAction="http://tempuri.org/IForumService/GetSubforumResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetSubforumFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        ForumUtils.SharedDataTypes.Post[] GetSubforum(string subforum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetPost", ReplyAction="http://tempuri.org/IForumService/GetPostResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetPostFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        ForumUtils.SharedDataTypes.Post GetPost(ForumUtils.SharedDataTypes.Postkey postkey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetReplies", ReplyAction="http://tempuri.org/IForumService/GetRepliesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetRepliesFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        ForumUtils.SharedDataTypes.Post[] GetReplies(ForumUtils.SharedDataTypes.Postkey postkey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Post", ReplyAction="http://tempuri.org/IForumService/PostResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/PostFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool Post(string current, ForumUtils.SharedDataTypes.Post toPost);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Reply", ReplyAction="http://tempuri.org/IForumService/ReplyResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/ReplyFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool Reply(ForumUtils.SharedDataTypes.Postkey current, ForumUtils.SharedDataTypes.Post toPost);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/EditPost", ReplyAction="http://tempuri.org/IForumService/EditPostResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/EditPostFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool EditPost(ForumUtils.SharedDataTypes.Postkey oldPost, ForumUtils.SharedDataTypes.Post newPost, string usrname, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/RemovePost", ReplyAction="http://tempuri.org/IForumService/RemovePostResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/RemovePostFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool RemovePost(ForumUtils.SharedDataTypes.Postkey postkey, string username, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/AddModerator", ReplyAction="http://tempuri.org/IForumService/AddModeratorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/AddModeratorFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/RemoveModerator", ReplyAction="http://tempuri.org/IForumService/RemoveModeratorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/RemoveModeratorFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/ReplaceModerator", ReplyAction="http://tempuri.org/IForumService/ReplaceModeratorResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/ReplaceModeratorFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/AddSubforum", ReplyAction="http://tempuri.org/IForumService/AddSubforumResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/AddSubforumFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool AddSubforum(string adminUsername, string adminPassword, string subforumName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/RemoveSubforum", ReplyAction="http://tempuri.org/IForumService/RemoveSubforumResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/RemoveSubforumFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/ReportSubForumTotalPosts", ReplyAction="http://tempuri.org/IForumService/ReportSubForumTotalPostsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/ReportSubForumTotalPostsFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/ReportUserTotalPosts", ReplyAction="http://tempuri.org/IForumService/ReportUserTotalPostsResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/ReportUserTotalPostsFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        int ReportUserTotalPosts(string adminUsername, string adminPassword, string username);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/ReplaceAdmin", ReplyAction="http://tempuri.org/IForumService/ReplaceAdminResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/ReplaceAdminFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/Subscribe", ReplyAction="http://tempuri.org/IForumService/SubscribeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/SubscribeFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        string Subscribe();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/SubscribeToForum", ReplyAction="http://tempuri.org/IForumService/SubscribeToForumResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/SubscribeToForumFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool SubscribeToForum();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/UnsubscribeFromForum", ReplyAction="http://tempuri.org/IForumService/UnsubscribeFromForumResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/UnsubscribeFromForumFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        bool UnsubscribeFromForum();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetData", ReplyAction="http://tempuri.org/IForumService/GetDataResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetDataFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IForumService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IForumService/GetDataUsingDataContractResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(System.ServiceModel.FaultException), Action="http://tempuri.org/IForumService/GetDataUsingDataContractFaultExceptionFault", Name="FaultException", Namespace="http://schemas.datacontract.org/2004/07/System.ServiceModel")]
        ForumClientCore.ForumService.CompositeType GetDataUsingDataContract(ForumClientCore.ForumService.CompositeType composite);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IForumServiceCallback {
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IForumService/onUpdate")]
        void onUpdate(ForumUtils.SharedDataTypes.Post message);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IForumServiceChannel : ForumClientConsole.ForumService.IForumService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ForumServiceClient : System.ServiceModel.DuplexClientBase<ForumClientConsole.ForumService.IForumService>, ForumClientConsole.ForumService.IForumService {
        
        public ForumServiceClient(System.ServiceModel.InstanceContext callbackInstance) : 
                base(callbackInstance) {
        }
        
        public ForumServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName) : 
                base(callbackInstance, endpointConfigurationName) {
        }
        
        public ForumServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, string remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ForumServiceClient(System.ServiceModel.InstanceContext callbackInstance, string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, endpointConfigurationName, remoteAddress) {
        }
        
        public ForumServiceClient(System.ServiceModel.InstanceContext callbackInstance, System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(callbackInstance, binding, remoteAddress) {
        }
        
        public bool Register(string username, string password) {
            return base.Channel.Register(username, password);
        }
        
        public bool Login(string username, string password) {
            return base.Channel.Login(username, password);
        }
        
        public bool Logout(string username) {
            return base.Channel.Logout(username);
        }
        
        public string[] GetSubforumsList() {
            return base.Channel.GetSubforumsList();
        }
        
        public ForumUtils.SharedDataTypes.Post[] GetSubforum(string subforum) {
            return base.Channel.GetSubforum(subforum);
        }
        
        public ForumUtils.SharedDataTypes.Post GetPost(ForumUtils.SharedDataTypes.Postkey postkey) {
            return base.Channel.GetPost(postkey);
        }
        
        public ForumUtils.SharedDataTypes.Post[] GetReplies(ForumUtils.SharedDataTypes.Postkey postkey) {
            return base.Channel.GetReplies(postkey);
        }
        
        public bool Post(string current, ForumUtils.SharedDataTypes.Post toPost) {
            return base.Channel.Post(current, toPost);
        }
        
        public bool Reply(ForumUtils.SharedDataTypes.Postkey current, ForumUtils.SharedDataTypes.Post toPost) {
            return base.Channel.Reply(current, toPost);
        }
        
        public bool EditPost(ForumUtils.SharedDataTypes.Postkey oldPost, ForumUtils.SharedDataTypes.Post newPost, string usrname, string password) {
            return base.Channel.EditPost(oldPost, newPost, usrname, password);
        }
        
        public bool RemovePost(ForumUtils.SharedDataTypes.Postkey postkey, string username, string password) {
            return base.Channel.RemovePost(postkey, username, password);
        }
        
        public bool AddModerator(string adminUsername, string adminPassword, string usernameToAdd, string subforum) {
            return base.Channel.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
        }
        
        public bool RemoveModerator(string adminUsername, string adminPassword, string usernameToRemove, string subforum) {
            return base.Channel.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
        }
        
        public bool ReplaceModerator(string adminUsername, string adminPassword, string usernameToAdd, string usernameToRemove, string subforum) {
            return base.Channel.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
        }
        
        public bool AddSubforum(string adminUsername, string adminPassword, string subforumName) {
            return base.Channel.AddSubforum(adminUsername, adminPassword, subforumName);
        }
        
        public bool RemoveSubforum(string adminUsername, string adminPassword, string subforumName) {
            return base.Channel.RemoveSubforum(adminUsername, adminPassword, subforumName);
        }
        
        public int ReportSubForumTotalPosts(string adminUsername, string adminPassword, string subforumName) {
            return base.Channel.ReportSubForumTotalPosts(adminUsername, adminPassword, subforumName);
        }
        
        public int ReportUserTotalPosts(string adminUsername, string adminPassword, string username) {
            return base.Channel.ReportUserTotalPosts(adminUsername, adminPassword, username);
        }
        
        public bool ReplaceAdmin(string oldAdminUsername, string oldAdminPassword, string newAdminUsername, string newAdminPassword) {
            return base.Channel.ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
        }
        
        public string Subscribe() {
            return base.Channel.Subscribe();
        }
        
        public bool SubscribeToForum() {
            return base.Channel.SubscribeToForum();
        }
        
        public bool UnsubscribeFromForum() {
            return base.Channel.UnsubscribeFromForum();
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public ForumClientCore.ForumService.CompositeType GetDataUsingDataContract(ForumClientCore.ForumService.CompositeType composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
    }
}
