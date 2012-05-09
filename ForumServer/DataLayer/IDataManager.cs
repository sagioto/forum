using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.DataLayer
{
    interface IDataManager
    {

        bool AddPost(Post post, string subforum);

        bool AddReply(Post reply, Postkey originalPost);

        /// <summary>
        /// Changing title & content of postKey. Not changing oldPost`s TimeStamp (keypost)
        /// </summary>
        /// <param name="oldPost"></param>
        /// <param name="postKey"></param>
        /// <returns></returns>
        bool EditPost(Post postToUpdate, Postkey originalPost);

        Subforum GetSubforum(string subforum);

        /// <summary>
        /// Returns all sub forms sorted by title
        /// </summary>
        /// <returns></returns>
        List<Subforum> GetSubforums();

        User GetUser(string username);

        Post GetPost(Postkey postkey);
        
        bool UpdateUser(User user);

        List<string> GetModerators(string subforum);

        bool SetModerators(string subforum, List<string> moderatorsList);

        bool SetUserState(string username, UserState state);

        List<Post> GetUserPosts(string username);

        bool RemoveSubforum(string subforum);

        bool RemovePost(Postkey postkey);

        bool AddSubforum(Subforum subforum);

        void SetAdmin(User admin);

        List<Post> GetAllPosts();
    }
}
