using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Collections.Concurrent;

namespace ForumServer.DataLayer
{
    public class DataManager : IDataManager
    {
        // Data structurs:
        private ConcurrentDictionary<string, User> users;
        private ConcurrentDictionary<string, Subforum> subforumsList;


        public DataManager()
        {
            users = new ConcurrentDictionary<string, User>();
            subforumsList = new ConcurrentDictionary<string, Subforum>();
        }

        #region IDataManager methods

        /// <summary>
        /// Adding a oldPost to given subforumName
        /// </summary>
        /// <param name="oldPost"></param>
        /// <param name="subforumName"></param>
        /// <returns></returns>
        public bool AddPost(Post post, string subforum)
        {
            return subforumsList[subforum].AddPost(post);
        }

        public bool AddReply(Post reply, Postkey originalPost)
        {
            Post post;
            GetPost(originalPost, out post);
            if (post == null)
                throw new PostNotFoundException();
            try
            {
                post.Replies.Add(reply.Key, reply);
                return true;
                //return UpdatePost(oldPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Updates the body of oldPost & only this field. Postkey, title, replies & parent oldPost will remain   
        /// the same.
        /// </summary>
        /// <param name="oldPost"></param>
        /// <param name="originalPost"></param>
        /// <returns></returns>
        public bool EditPost(Post postToUpdate, Postkey originalPost)
        {
            Post oldPost;
            GetPost(postToUpdate.Key, out oldPost);
            if (oldPost == null)
                throw new PostNotFoundException();
            try
            {
                oldPost = postToUpdate;
                return true;
                //return UpdatePost(oldPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// Returns requested Subforum with title 'subforumName'
        /// </summary>
        /// <param name="subforumName"></param>
        /// <returns></returns>
        public Subforum GetSubforum(string subforumName)
        {
            try
            {
                return subforumsList[subforumName];
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        /// <summary>
        /// Returns the requested User with given 'user'
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetUser(string username)
        {
            try
            {
                return users[username];
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public bool UpdateUser(User user)
        {
            try
            {
                users[user.Username] = user;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<string> GetModerators(string subforum)
        {
            try
            {
                return subforumsList[subforum].ModeratorsList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool SetModerators(string subforum, List<string> moderatorsList)
        {
            try
            {
                subforumsList[subforum].ModeratorsList = moderatorsList;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetPost(Postkey postkey, out Post returnedPost)
        {
            returnedPost = null;
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                try
                {
                    if (subforumEntry.Value.Posts.ContainsKey(postkey))    // If oldPost is main oldPost in subforumName
                    {
                        returnedPost = subforumsList[subforumEntry.Key].Posts[postkey];
                    }
                    else    // If oldPost is not main oldPost, search oldPost in replies & update
                    {
                        GetReply(postkey, subforumsList[subforumEntry.Key].Posts,out returnedPost);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Returns an array of all existing subforums
        /// </summary>
        /// <returns></returns>
        public List<Subforum> GetSubforums()
        {
            List<Subforum> result = subforumsList.Values.ToList<Subforum>();
            result.Sort();
            return result;
        }
        
        public bool SetUserState(string username, UserState state)
        {
            try
            {
                users[username].CurrentState = state;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<Post> GetUserPosts(string username)
        {
            throw new NotImplementedException();
        }

        public bool RemoveSubforum(string subforum)
        {
            try
            {
                Subforum s;
                return subforumsList.TryRemove(subforum,out s);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemovePost(Postkey postkey)
        {
            throw new NotImplementedException();
        }


        #endregion

        #region Private methods

        //private string GetSubforumOfPost(Postkey postKey)
        //{
        //    foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
        //    {
        //        if (subforumEntry.Value.Posts.ContainsKey(postKey))
        //        {
        //            return subforumEntry.Key;
        //        }
        //    }
        //    return null;
        //}

        private void GetReply(Postkey postkey, Dictionary<Postkey, Post> postsList, out Post returnedPost)
        {
            if (postsList.ContainsKey(postkey))
            {
                returnedPost = postsList[postkey];
            }
            else
            {
                foreach (Post reply in postsList.Values)
                {
                    GetReply(postkey, reply.Replies,out returnedPost);
                }
                returnedPost = null;
            }
        }

        //private bool UpdatePost(Post postToUpdate)
        //{
            

            //foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            //{
            //    try
            //    {
            //        if (subforumEntry.Value.Posts.ContainsKey(oldPost.Key))    // If oldPost is main oldPost in subforumName
            //        {
            //            subforumsList[subforumEntry.Key].Posts[oldPost.Key] = oldPost;    // update oldPost
            //            return true;
            //        }
            //        else    // If oldPost is not main oldPost, search oldPost in replies & update
            //        {
            //            return UpdateReply(oldPost, subforumsList[subforumEntry.Key].Posts);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        throw ex;
            //    }
            //}
            //return false;
        //}


        private bool UpdateReply(Post replyToUpdate, Dictionary<Postkey, Post> postsList)
        {
            bool ans = false;
            if (postsList.ContainsKey(replyToUpdate.Key))
            {
                postsList[replyToUpdate.Key] = replyToUpdate;
                return true;
            }
            else
            {
                foreach (Post reply in postsList.Values)
                {
                    ans = UpdateReply(replyToUpdate, reply.Replies);
                    if (ans == true)
                    {
                        return ans;
                    }
                }
                return false;
            }
        }



        //private string GetPostString(Postkey postKey)
        //{
        //    foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
        //    {
        //        if (subforumEntry.Value.Posts.ContainsKey(postKey))
        //        {
        //            return subforumEntry.Key;
        //        }
        //    }
        //    return null;
        //}

        #endregion









        public Post GetPost(Postkey postkey)
        {
            Post res;
            GetPost(postkey,out res);
            return res;

        }


        public bool AddSubforum(Subforum subforum)
        {
            try
            {
                subforumsList.TryAdd(subforum.Name, subforum);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool AddSubforum(string subforumName)
        {
            throw new NotImplementedException();
        }

        public bool SetAdmin(User user)
        {
            throw new NotImplementedException();
        }

        public List<Post> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public User GetAdmin()
        {
            throw new NotImplementedException();
        }


        void IDataManager.SetAdmin(User admin)
        {
            throw new NotImplementedException();
        }
    }
}