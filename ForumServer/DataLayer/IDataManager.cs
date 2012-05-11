using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ForumServer.DataLayer
{
    interface IDataManager
    {

        #region Init methods

        /// <summary>
        /// Creates subforums & posts according to web.Config file parameters
        /// </summary>
        void InitForumData();
        #endregion

        #region Posts & Reply methods

        /// <summary>
        /// Returns Post that has postKey
        /// </summary>
        /// <param name="postkey">Postkey of the Post to return</param>
        /// <returns>Post of given postkey</returns>
        /// <exception cref="PostNotFoundException">In case postkey is not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        Post GetPost(Postkey postkey);

        /// <summary>
        /// Adding given post to subforumName`s posts
        /// </summary>
        /// <param name="post">Post to add</param>
        /// <param name="subforumName">Subforum to add post to</param>
        /// <returns>true is post was added</returns>
        /// <exception cref="SubforumNotFoundException">In case subforumName not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool AddPost(Post post, string subforum);

        /// <summary>
        /// Removes given postkey from forum`s posts
        /// </summary>
        /// <param name="postkey">Postkey to be removed</param>
        /// <returns>true if postkey was removed</returns>
        /// <remarks>Tests in ServerTests project</remarks>
        bool RemovePost(Postkey postkey);

        /// <summary>
        /// Adding iven reply to postKey
        /// </summary>
        /// <param name="reply">Reply to add</param>
        /// <param name="originalPost">Post to add the reply to</param>
        /// <returns>true is reply was added</returns>
        /// <exception cref="PostNotFoundException">In case postKey not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool AddReply(Post reply, Postkey postKey);

        /// <summary>
        /// Changing title & content of postKey. Postkey, replies & parent of oldPostKey will remain the same.
        /// <param name="oldPost"></param>
        /// <param name="postKey"></param>
        /// <returns></returns>
        /// <remarks>Tests in ServerTests project</remarks>
        bool EditPost(Post postToUpdate, Postkey oldPostKey);

        /// <summary>
        /// Returns a list of all posts & replies in forum
        /// </summary>
        /// <returns></returns>
        List<Post> GetAllPosts();
        #endregion

        #region Subforms Getters & Setters

        /// <summary>
        /// Adding given subforum to forum`s subforum list
        /// </summary>
        /// <param name="subforum">Subgorum to be added</param>
        /// <returns>true if subforum was added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool AddSubforum(Subforum subforum);

        /// <summary>
        /// Remove given subforum from forum`s subforums list
        /// </summary>
        /// <param name="subforum">Subforum to remove</param>
        /// <returns>true if subforum was removed</returns>
        /// <exception cref="SubforumNotFoundException">if subforum not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool RemoveSubforum(string subforum);

        /// <summary>
        /// Returns Subforum of given subforumName
        /// </summary>
        /// <param name="subforumName">Name of the subforum to be returned</param>
        /// <returns></returns>
        /// <exception cref="SubforumNotFoundException">In case subforum was not found</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        Subforum GetSubforum(string subforumName);

        /// <summary>
        /// Returns all sub forms sorted by title
        /// </summary>
        /// <returns>List of all subforums sorted by title</returns>
        /// <remarks>Tests in ServerTest project</remarks>
        List<Subforum> GetSubforums();

        /// <summary>
        /// Get list of all moderators of given subforum
        /// </summary>
        /// <param name="subforum">Name of subforum to get its moderators</param>
        /// <returns>List of username of all moderators</returns>
        /// <remarks>Tests in ServerTests project</remarks>
        /// <exception cref="SubforumNotFoundException">In case subforum was not found</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        List<string> GetModerators(string subforum);

        /// <summary>
        /// Setter for given subforum moderators list
        /// </summary>
        /// <param name="subforum">Subforum to set moderators</param>
        /// <param name="moderatorsList">Moderatores to be set in subforum</param>
        /// <returns>true if Moderatres list was set</returns>
        /// <exception cref="SubforumNotFoundException">In case subforum was not found</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool SetModerators(string subforum, List<string> moderatorsList);

        #endregion

        #region Users methods

        /// <summary>
        /// Add given user to users list
        /// </summary>
        /// <param name="user">User to be added</param>
        /// <returns>true if user was added</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="OverflowException"></exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool AddUser(User user);

        /// <summary>
        /// Returns User with name username
        /// </summary>
        /// <param name="username">Name of User to be returned</param>
        /// <returns>User with given username</returns>
        /// <exception cref="UserNotFoundException">In case username not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        User GetUser(string username);

        /// <summary>
        /// Update user with user`s name to be user
        /// </summary>
        /// <param name="user">User to update</param>
        /// <returns>true if user was updated</returns>
        /// <exception cref="UserNotFoundException">In case username not exists</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        bool UpdateUser(User user);

        /// <summary>
        /// Update given username state to be state
        /// </summary>
        /// <param name="username">Username to update its state</param>
        /// <param name="state">State to update</param>
        /// <returns>true if state was updated</returns>
        /// <remarks>Tests in ServerTests project</remarks>
        bool SetUserState(string username, UserState state);

        /// <summary>
        /// Returns a list of all posts of given username
        /// </summary>
        /// <param name="username">Username to return its Posts</param>
        /// <returns>List of all Posts of username</returns>
        /// <exception cref="Exception">For any case. No special exception shuld accour</exception>
        /// <remarks>Tests in ServerTests project</remarks>
        List<Post> GetUserPosts(string username);

        /// <summary>
        /// Add admin to users, sets its AuthenticationLevel to ADMIN, update adminName in dataManager
        /// </summary>
        /// <param name="admin">User to be admin</param>
        /// <exception cref="Exception"></exception>
        bool SetAdmin(User admin);

        /// <summary>
        /// Returns the admin of forum
        /// </summary>
        /// <returns>admin</returns>
        /// <exception cref="UserNotFoundException"></exception>
        User GetAdmin();

        #endregion

    }
}
