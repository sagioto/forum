using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;

namespace ForumServer.DataLayer
{
    public class DataManager : IDataManager
    {
        private Dictionary<string, User> users;
        private Dictionary<string, Subforum> subforumsList;

        #region IDataManager methods

        public bool addPost(Post post, string subforum)
        {
            return subforumsList[subforum].addPost(post);
        }

        public bool addReply(Post reply, Postkey originalPost)
        {
            string subforum = GetSubforumOfPost(originalPost);
            if (subforum == null)
                throw new PostNotFoundException();
            try
            {
                subforumsList[subforum].Posts[originalPost].Replies.Add(reply.Key, reply);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }


        public bool editPost(Post postToUpdate, Postkey originalPost)
        {
            string subforum = GetSubforumOfPost(originalPost);
            if (subforum == null)
                throw new PostNotFoundException();
            try
            {
                //TODO Continue
            //    subforumsList[subforum].Posts[originalPost].
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public Dictionary<string, Subforum> getSubforumsDic()
        {
            throw new NotImplementedException();
        }

        public Subforum getSubforum(string subforum)
        {
            throw new NotImplementedException();
        }

        public User getUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool updateUser(User user)
        {
            throw new NotImplementedException();
        }

        public List<string> getModerators(string subforum)
        {
            throw new NotImplementedException();
        }

        public bool setModerators(string subforum)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private methods

        private string GetSubforumOfPost(Postkey postKey)
        {
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                if (subforumEntry.Value.Posts.ContainsKey(postKey))
                {
                    return subforumEntry.Key;
                }
            }
            return null;
        }

        private string GetPost(Postkey postKey)
        {
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                if (subforumEntry.Value.Posts.ContainsKey(postKey))
                {
                    return subforumEntry.Key;
                }
            }
            return null;
        }

        #endregion

    }
}