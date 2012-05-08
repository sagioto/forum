using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;

namespace ForumServer.DataLayer
{
    public class DataManager : IDataManager
    {
<<<<<<< HEAD
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
=======

        public bool AddPost(DataTypes.Post post, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool AddReply(DataTypes.Post reply, DataTypes.Postkey originalPost)
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public Subforum getSubforum(string subforum)
=======
        public bool EditPost(DataTypes.Post postToUpdate, DataTypes.Postkey originalPost)
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public User getUser(string username)
=======
        public Dictionary<string, DataTypes.Subforum> getSubforumsDic()
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public bool updateUser(User user)
=======
        public DataTypes.Subforum GetSubforum(string subforum)
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public List<string> getModerators(string subforum)
=======
        public DataTypes.User GetUser(string username)
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
        public bool setModerators(string subforum)
=======
        public bool UpdateUser(DataTypes.User user)
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
        {
            throw new NotImplementedException();
        }

<<<<<<< HEAD
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

=======
        public List<string> GetModerators(string subforum)
        {
            throw new NotImplementedException();
        }

        public bool SetModerators(string subforum)
        {
            throw new NotImplementedException();
        }
>>>>>>> 95b5b5c04d483a3301c550c0defc830854d32edb
    }
}