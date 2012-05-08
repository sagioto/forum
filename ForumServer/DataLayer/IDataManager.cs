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
        /// Changing title & content of postKey. Not changing post`s TimeStamp (keypost)
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <param name="postKey"></param>
        /// <returns></returns>
        bool EditPost(Post postToUpdate, Postkey originalPost);

        Subforum GetSubforum(string subforum);

        /// <summary>
        /// returns all sub forms sorted by title
        /// </summary>
        /// <returns></returns>
        Subforum[] GetSubforums();

        User GetUser(string username);

        bool UpdateUser(User user);

        List<string> GetModerators(string subforum);

        bool SetModerators(string subforum);

    }
}
