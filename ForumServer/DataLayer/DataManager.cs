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

        public DataManager()
        {
            users = new Dictionary<string, User>();
            subforumsList = new Dictionary<string, Subforum>();
        }

        #region IDataManager methods

        public bool EditPost(Post postToUpdate, Postkey originalPost)
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

        public bool AddPost(DataTypes.Post post, string subforum)
        {
            return subforumsList[subforum].AddPost(post);
        }

        public bool AddReply(DataTypes.Post reply, DataTypes.Postkey originalPost)
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


        public DataTypes.Subforum GetSubforum(string subforum)

        {
            throw new NotImplementedException();
        }


        public DataTypes.User GetUser(string username)
        {
            throw new NotImplementedException();
        }


        public bool UpdateUser(DataTypes.User user)
        {
            throw new NotImplementedException();
        }

              public List<string> GetModerators(string subforum)
        {
            throw new NotImplementedException();
        }

        public bool SetModerators(string subforum)
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