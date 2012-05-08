using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.DataLayer
{
    interface IDataManager
    {

         bool addPost(Post post, string subforum);

         bool addReply(Post reply, Postkey originalPost);

        /// <summary>
        /// Changing title & content of postKey. Not changing post`s TimeStamp (keypost)
        /// </summary>
        /// <param name="postToUpdate"></param>
        /// <param name="postKey"></param>
        /// <returns></returns>
         bool editPost(Post postToUpdate, Postkey originalPost);

         Dictionary<string, Subforum> getSubforumsDic();

         Subforum getSubforum(string subforum);

         User getUser(string username);

         bool updateUser(User user);

         List<string> getModerators(string subforum);

         bool setModerators(string subforum);

    }
}
