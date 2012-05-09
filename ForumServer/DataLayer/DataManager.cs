﻿using System;
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

        #region Posts & Reply methods

        public Post GetPost(Postkey postkey)
        {
            Post res;
            GetPost(postkey, out res);
            if (res != null)
            {
                return res;
            }
            else
            {
                throw new PostNotFoundException();
            }
        }

        public bool AddPost(Post post, string subforum)
        {
            try
            {
                return subforumsList[subforum].AddPost(post);
            }
            catch (Exception)
            {
                throw new SubforumNotFoundException();
            }
        }

        public bool RemovePost(Postkey postkey)
        {
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                try
                {
                    if (subforumEntry.Value.Posts.ContainsKey(postkey))
                    {
                        subforumsList[subforumEntry.Key].Posts.Remove(postkey);
                        return true;
                    }
                    else
                    {
                        return RemoveReply(postkey, subforumsList[subforumEntry.Key].Posts);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return false;
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

        public bool EditPost(Post postToUpdate, Postkey originalPost)
        {
            Post oldPost;
            GetPost(originalPost, out oldPost);
            if (oldPost == null)
                throw new PostNotFoundException();
            try
            {
                oldPost.Body = postToUpdate.Body;
                oldPost.Title = postToUpdate.Title;
                return true;
                //return UpdatePost(oldPost);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Subforms Getters & Setters

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

        public bool RemoveSubforum(string subforum)
        {
            try
            {
                Subforum s;
                return subforumsList.TryRemove(subforum, out s);
            }
            catch (Exception)
            {
                throw new SubforumNotFoundException();
            }
        }

        public Subforum GetSubforum(string subforumName)
        {
            try
            {
                return subforumsList[subforumName];
            }
            catch (Exception)
            {
                throw new SubforumNotFoundException();
            }

        }

        public List<Subforum> GetSubforums()
        {
            List<Subforum> result = subforumsList.Values.ToList<Subforum>();
            result.Sort();
            return result;
        }

        public List<string> GetModerators(string subforum)
        {
            try
            {
                return subforumsList[subforum].ModeratorsList;
            }
            catch (Exception)
            {
                throw new SubforumNotFoundException();
            }
        }

        public bool SetModerators(string subforum, List<string> moderatorsList)
        {
            try
            {
                subforumsList[subforum].ModeratorsList = moderatorsList;
                return true;
            }
            catch (Exception)
            {
                throw new SubforumNotFoundException();
            }
        }

        #endregion

        #region Users methods

        public bool AddUser(User user)
        {
            try
            {
                users.TryAdd(user.Username, user);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                return users[username];
            }
            catch (Exception)
            {
                throw new UserNotFoundException();
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                users[user.Username] = user;
                return true;
            }
            catch (Exception)
            {
                throw new UserNotFoundException();
            }
        }

        public bool SetUserState(string username, UserState state)
        {
            try
            {
                users[username].CurrentState = state;
                return true;
            }
            catch (Exception)
            {
                throw new UserNotFoundException();
            }
        }

        public List<Post> GetUserPosts(string username)
        {
            List<Post> postsOfUser;
            GetUserPosts(username, out postsOfUser);
            return postsOfUser;
        }

        #endregion

        #endregion //IDataManager methods

        #region Internal Private methods

        private void GetPost(Postkey postkey, out Post returnedPost)
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
                        GetReply(postkey, subforumsList[subforumEntry.Key].Posts, out returnedPost);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void GetRepliesOfUser(Post post, string username, List<Post> listOfUserPosts, out List<Post> returnedReplies)
        {
            returnedReplies = listOfUserPosts;
            foreach (Post reply in post.Replies.Values)
            {
                if (reply.Key.Username == username)
                {
                    returnedReplies.Add(reply);
                    GetRepliesOfUser(reply, username, listOfUserPosts.Union(returnedReplies).ToList(), out returnedReplies);
                }
            }
        }

        private void GetUserPosts(string username, out List<Post> returnedPosts)
        {
            returnedPosts = new List<Post>();
            List<Post> returnedReplies = null;
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                try
                {
                    foreach (Post post in subforumEntry.Value.Posts.Values)
                    {
                        if (post.Key.Username == username)
                        {
                            returnedPosts.Add(post);
                            GetRepliesOfUser(post, username, new List<Post>(), out returnedReplies);
                            returnedPosts = returnedPosts.Union(returnedReplies).ToList<Post>();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private bool RemoveReply(Postkey postkey, Dictionary<Postkey, Post> repliesList)
        {
            foreach (Post reply in repliesList.Values)
            {
                if (reply.Replies.ContainsKey(postkey))
                {
                    reply.Replies.Remove(postkey);
                    return true;
                }
                else
                {
                    return RemoveReply(postkey, reply.Replies);
                }
            }
            return false;
        }



        private void GetReply(Postkey postkey, Dictionary<Postkey, Post> postsList, out Post returnedPost)
        {
            returnedPost = null;
            if (postsList.ContainsKey(postkey))
            {
                returnedPost = postsList[postkey];
            }
            else
            {
                foreach (Post reply in postsList.Values)
                {
                    GetReply(postkey, reply.Replies, out returnedPost);
                }
            }
        }

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

        #endregion

    }
}