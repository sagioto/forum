using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Concurrent;
using System.Configuration;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;
using System.Threading;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumServer.DataLayer
{
    // This is assignment 2 Data Manager - NOT USED ANY MORE
    public class DataManagerLocal : IDataManager
    {

         
        // Data structures:
        private ConcurrentDictionary<string, User> users;
        private ConcurrentDictionary<string, Subforum> subforumsList;
        private string adminName;
         
        public DataManagerLocal()
        {
            users = new ConcurrentDictionary<string, User>();
            subforumsList = new ConcurrentDictionary<string, Subforum>();
            string adminName = ConfigurationManager.AppSettings["adminName"];
            string adminPass = ConfigurationManager.AppSettings["adminPassword"];
            User admin = new User(adminName, adminPass);
            SetAdmin(admin);
        }

        #region IDataManager methods

        #region Init methods

        public void InitForumData()
        {
            try
            {
                int numberOfSubforums = Convert.ToInt32(ConfigurationManager.AppSettings["initializeNumberOfSubforums"].ToString());
                string[] subforumsNamesList = ConfigurationManager.AppSettings["subforumsNamesList"].ToString().Split(',');
                int numberOfPosts = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfPostsInEachSubforum"].ToString());
                string adminName = ConfigurationManager.AppSettings["adminName"].ToString();

                for (int i = 0 ; i < numberOfSubforums ; i++)
                {
                    Subforum s = new Subforum(subforumsNamesList[i]);
                    this.AddSubforum(s);
                    for (int j = 0 ; j < numberOfPosts ; j++)
                    {
                        Thread.Sleep(100);
                        string content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ligula libero, rhoncus ac sollicitudin ut, pulvinar nec risus. Nunc laoreet hendrerit mollis. Sed at quam sit amet lacus vehicula hendrerit sit amet sed tellus. In accumsan turpis id justo scelerisque auctor. Sed ultricies, felis vitae hendrerit viverra, nunc arcu rutrum orci, eu tincidunt urna quam vel nulla. Praesent ut suscipit massa. Proin cursus egestas interdum. Maecenas at enim nibh. Nunc dictum mi non neque eleifend quis vulputate velit tincidunt. Integer fringilla sapien quis ipsum lobortis vel mollis ante rhoncus.";
                        this.AddPost(new Post(new Postkey(adminName, DateTime.Now),
                            "Post" + j + " in Subforum: " + s.Name,content, null, s.Name), s.Name);
                    }
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        #endregion

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
                subforumsList[subforum].TotalPosts++;
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
                        subforumsList[subforumEntry.Key].TotalPosts--;
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
                post.HasReplies = true;
                subforumsList[post.Subforum].TotalPosts++;
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

        public List<Post> GetAllPosts()
        {
            List<Post> allPosts = new List<Post>();
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                try
                {
                    allPosts = allPosts.Union(subforumEntry.Value.Posts.Values.ToList<Post>()).ToList<Post>();
                    foreach (Post post in subforumEntry.Value.Posts.Values)
                    {
                        allPosts = allPosts.Union(GetAllReplyOfPost(post)).ToList<Post>();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return allPosts;
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
                if (users.ContainsKey(username))
                {
                    return users[username];
                }
                else
                {
                    return null;
                }
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

        public bool SetAdmin(User admin)
        {
            try
            {
                admin.Level = AuthorizationLevel.ADMIN;
                users.TryAdd(admin.Username, admin);
                this.adminName = admin.Username;
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public User GetAdmin()
        {
            try
            {
                return users[adminName];
            }
            catch (Exception)
            {
                throw new UserNotFoundException();
            }
        }

        #endregion

        #endregion //IDataManager methods

        #region Internal Private methods

        private void GetPost(Postkey postkey, out Post returnedPost)
        {
            returnedPost = null;
            foreach (KeyValuePair<string, Subforum> subforumEntry in subforumsList)
            {
                if (returnedPost != null)
                {
                    break;
                }
                try
                {
                    foreach (Post PostToReturn in subforumEntry.Value.Posts.Values)
                    {
                        if (PostToReturn.Key.CompareTo(postkey) == 0)
                        {
                            returnedPost = PostToReturn;
                            break;

                        }
                    }
                    if (returnedPost != null)
                    {
                        break;
                    }
                    else
                    {

                    
                       
                    //if (subforumEntry.Value.Posts.ContainsKey(postkey))    // If oldPost is main oldPost in subforumName
                    //{
                    //    returnedPost = subforumsList[subforumEntry.Key].Posts[postkey];
                    //    break;
                    //}
                    //else    // If oldPost is not main oldPost, search oldPost in replies & update

                    //{
                        GetReply(postkey, subforumsList[subforumEntry.Key].Posts, out returnedPost);
                        if (returnedPost != null)
                            break;
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
                    subforumsList[reply.Subforum].TotalPosts--;
                    reply.HasReplies = !(reply.Replies.Values.Count == 0); // If reply.Replies.Values.Count > 0 then HasReplies==true;
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
            foreach (Post PostToReturn in postsList.Values)
            {
                if (PostToReturn.Key.CompareTo(postkey) == 0)
                {
                    returnedPost = PostToReturn;
                    break;
                }
            }
            if (returnedPost != null)
            {
                return;
            }
            //if (postsList.ContainsKey(postkey))
            //{
            //    returnedPost = postsList[postkey];
            //    return;
            //}
            //else
            //{
                foreach (Post reply in postsList.Values)
                {
                    if (returnedPost == null)
                    {
                        GetReply(postkey, reply.Replies, out returnedPost);
                    }
                }
            //}
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

        private List<Post> GetAllReplyOfPost(Post post)
        {
            List<Post> allReplies = new List<Post>();
            allReplies = allReplies.Union(post.Replies.Values).ToList<Post>();
            foreach (Post PostToReturn in post.Replies.Values)
            {
                allReplies = allReplies.Union(GetAllReplyOfPost(PostToReturn)).ToList<Post>();
            }
            return allReplies;
        }
        #endregion




        public List<User> GetAllLoggedInUsers()
        {
            throw new NotImplementedException();
        }
    }
}