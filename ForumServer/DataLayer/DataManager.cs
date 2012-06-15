using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ForumServer.DataTypes;
using System.Collections.Concurrent;
using System.Configuration;
using ForumUtils.SharedDataTypes;
using System.Threading;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumServer.DataLayer
{
    public class DataManager : IDataManager
    {
        ForumEntities ForumContext;
        private int currentPostKeyId;
        public DataManager()
        {
            ForumContext = new ForumEntities();
            ForumContext.Connection.Open();
           //CleanForumData();
            currentPostKeyId = 0;
            
        }

        public void CleanForumData()
        {
            try
            {
                List<string> tblNames = new List<string>();
                tblNames.Add("tblSubforums");
                tblNames.Add("tblPostKeys");
                tblNames.Add("tblPosts");
                tblNames.Add("tblUsers");
                currentPostKeyId = 0;
                //ForumContext.ExecuteStoreCommand(@"DBCC CHECKIDENT (tblPostKeys, RESEED, 0)");
                foreach (var tableName in tblNames)
                {
                    ForumContext.ExecuteStoreCommand("DELETE FROM " + tableName);
                }
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public void InitForumData()
        {
            try
            {
                int numberOfSubforums = Convert.ToInt32(ConfigurationManager.AppSettings["initializeNumberOfSubforums"].ToString());
                string[] subforumsNamesList = ConfigurationManager.AppSettings["subforumsNamesList"].ToString().Split(',');
                int numberOfPosts = Convert.ToInt32(ConfigurationManager.AppSettings["numberOfPostsInEachSubforum"].ToString());
                // Add admin:
                string adminName = ConfigurationManager.AppSettings["adminName"];
                string adminPass = ConfigurationManager.AppSettings["adminPassword"];
                User admin = new User(adminName, adminPass);
                AddUser(admin);
                SetAdmin(admin);

                for (int i = 0 ; i < numberOfSubforums ; i++)
                {
                    Subforum s = new Subforum(subforumsNamesList[i]);
                    this.AddSubforum(s);
                    for (int j = 0 ; j < numberOfPosts ; j++)
                    {
                        Thread.Sleep(1000);
                        this.AddPost(new Post(new Postkey(adminName, DateTime.Now),
                            "Post" + j + " in Subforum: " + s.Name, "content", new Postkey("", DateTime.Now), s.Name), s.Name);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Post GetPost(Postkey postkey)
        {
            try
            {
                IEnumerable<PostEntity> postsQuery = GetPostEntity(postkey);

                if (postsQuery.Count() != 1)
                {
                    // Post wasn't found or more than one was found
                    return null;
                }
                else
                {
                    PostEntity pe = postsQuery.First();
                    return PostEntityToPost(pe);
                }
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }



        private Post PostEntityToPost(PostEntity pe)
        {
            Postkey parentPostKey = null;

            // Finding parent`s postkey:
            if (pe.ParentPostKeyId != -1)
            {
                IEnumerable<PostKeyEntity> parentPostkeyQuery = from pk in ForumContext.PostKeyEntities
                                                                where pk.PostKeyId == pe.ParentPostKeyId
                                                                select pk;
                PostKeyEntity pke = parentPostkeyQuery.ElementAt<PostKeyEntity>(0);
                parentPostKey = new Postkey(pke.Username, pke.Time);
            }

            // Finding PostKey:
            IEnumerable<PostKeyEntity> PostkeyQuery = from pk in ForumContext.PostKeyEntities
                                                      where pk.PostKeyId == pe.PostKeyId
                                                      select pk;
            PostKeyEntity postkeyEntity = PostkeyQuery.First();
            Postkey postkey = new Postkey(postkeyEntity.Username, postkeyEntity.Time);

            Post PostToReturn = new Post(postkey, pe.Title, pe.Body, parentPostKey, pe.SubforumName);

            // Replies:
            IEnumerable<PostEntity> repliesList = from r in ForumContext.PostEntities
                                                  where r.ParentPostKeyId == pe.PostKeyId
                                                  select r;
            Dictionary<Postkey, Post> repliesDictionary = new Dictionary<Postkey, Post>();
            if (repliesList.Count() != 0)
            {
                foreach (PostEntity reply in repliesList)
                {
                    Post p = PostEntityToPost(reply);
                    repliesDictionary.Add(p.Key, p);
                }
                PostToReturn.HasReplies = true;
            }
            else
            {
                PostToReturn.HasReplies = false;
            }
            PostToReturn.Replies = repliesDictionary;
            return PostToReturn;
        }

        private IEnumerable<PostEntity> GetPostEntity(Postkey postkey)
        {
            IEnumerable<PostEntity> postsQuery = (from pk in ForumContext.PostKeyEntities
                                                  from post in ForumContext.PostEntities
                                                  where pk.Username == postkey.Username &&
                                                  pk.Time.Hour == postkey.Time.Hour &&
                                                  pk.Time.Minute == postkey.Time.Minute && pk.Time.Second == postkey.Time.Second
                                                  && pk.Time.Year == postkey.Time.Year && pk.Time.Month == postkey.Time.Month &&
                                                  pk.Time.Day == postkey.Time.Day
                                                  && pk.PostKeyId == post.PostKeyId
                                                  select post);
            return postsQuery;
        }

        public bool AddPost(Post post, string subforum)
        {
            //Getting last id:
            IEnumerable<int> postKeysList = (from m in ForumContext.PostKeyEntities
                                             select m.PostKeyId);
            int lastId = 0;
            if (postKeysList.Count() != 0)
            {
                lastId = postKeysList.Max();
            }
            currentPostKeyId = lastId + 1;

            PostKeyEntity pke = new PostKeyEntity();
            pke.PostKeyId = currentPostKeyId;
            pke.Username = post.Key.Username;
            pke.Time = post.Key.Time;

            PostEntity pe = new PostEntity();
            pe.PostKeyId = currentPostKeyId;
            pe.ParentPostKeyId = -1;
            pe.Title = post.Title;
            pe.Body = post.Body;
            pe.SubforumName = subforum; //TODO - Why do we need 'subforum'?
            try
            {
                ForumContext.AddToPostKeyEntities(pke);
                ForumContext.AddToPostEntities(pe);
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }

        }

        public bool RemovePost(Postkey postkey)
        {
            try
            {
                // Find Postkey
                IEnumerable<PostKeyEntity> postkeyQuery = GetPostKeyEntity(postkey);

                // Find Post
                PostKeyEntity pke = postkeyQuery.First();
                IEnumerable<PostEntity> postQuery = GetPostEntity(postkey);

                ForumContext.PostKeyEntities.DeleteObject(pke);
                ForumContext.PostEntities.DeleteObject(postQuery.ElementAt<PostEntity>(0));
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
                //TODO
                throw;
            }
        }

        private IEnumerable<PostKeyEntity> GetPostKeyEntity(Postkey postkey)
        {
            IEnumerable<PostKeyEntity> postkeyQuery = from pk in ForumContext.PostKeyEntities
                                                      where pk.Username == postkey.Username &&
                                                      pk.Time.Hour == postkey.Time.Hour &&
                                                      pk.Time.Minute == postkey.Time.Minute && pk.Time.Second == postkey.Time.Second
                                                      && pk.Time.Year == postkey.Time.Year && pk.Time.Month == postkey.Time.Month &&
                                                      pk.Time.Day == postkey.Time.Day
                                                      select pk;
            return postkeyQuery;
        }

        public bool AddReply(Post reply, Postkey postKey)
        {
            try
            {
                // Find parent postkey:
                IEnumerable<PostKeyEntity> postkeyQuery = GetPostKeyEntity(postKey);
                IEnumerable<PostEntity> postQuery = GetPostEntity(postKey);

                //Getting last id:
                IEnumerable<int> postKeysList = (from m in ForumContext.PostKeyEntities
                                                 select m.PostKeyId);
                int lastId = 0;
                if (postKeysList.Count() != 0)
                {
                    lastId = postKeysList.Max();
                }
                currentPostKeyId = lastId + 1;


                PostKeyEntity pke = new PostKeyEntity();
                pke.PostKeyId = currentPostKeyId;
                pke.Username = reply.Key.Username;
                pke.Time = reply.Key.Time;
                ForumContext.AddToPostKeyEntities(pke);
                PostEntity pe = new PostEntity();

                
                pe.PostKeyId = currentPostKeyId;
                //currentPostKeyId++;
                pe.ParentPostKeyId = postkeyQuery.ElementAt(0).PostKeyId;
                pe.Title = reply.Title;
                pe.Body = reply.Body;
                pe.SubforumName = postQuery.ElementAt(0).SubforumName;

                ForumContext.AddToPostEntities(pe);
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public bool EditPost(Post postToUpdate, Postkey oldPostKey)
        {
            try
            {
                PostEntity post = GetPostEntity(oldPostKey).First();
                post.Title = postToUpdate.Title;
                post.Body = postToUpdate.Body;
                //TODO - Do username & datetime (Postkey) change on update?
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public List<Post> GetAllPosts()
        {
            List<Post> result = new List<Post>();
            try
            {
                IEnumerable<PostEntity> postsQuery = from post in ForumContext.PostEntities
                                                     select post;

                foreach (PostEntity pe in postsQuery)
                {
                    Post p = PostEntityToPost(pe);
                    result.Add(p);

                    //Postkey parentPostKey = null;

                    //// Create PostKey

                    //IEnumerable<PostKeyEntity> PostkeyQuery = from pk in ForumContext.PostKeyEntities
                    //                                          where pk.PostKeyId == pe.PostKeyId
                    //                                          select pk;
                    //PostKeyEntity pke = PostkeyQuery.First();
                    //Postkey postKey = new Postkey(pke.Username, pke.Time);

                    //// Create parent postKey
                    //if (pe.ParentPostKeyId != -1)
                    //{
                    //    IEnumerable<PostKeyEntity> parentPostkeyQuery = from pk in ForumContext.PostKeyEntities
                    //                                                    where pk.PostKeyId == pe.ParentPostKeyId
                    //                                                    select pk;
                    //    pke = parentPostkeyQuery.First();
                    //    parentPostKey = new Postkey(pke.Username, pke.Time);
                    //}
                    //Post p = new Post(postKey, pe.Title, pe.Body, parentPostKey, pe.SubforumName);
                    //result.Add(p);
                }
                return result;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }




        public bool AddSubforum(Subforum subforum)
        {
            try
            {
                SubforumEntity se = new SubforumEntity();
                se.Name = subforum.Name;
                se.Description = subforum.Description;
                ForumContext.SubforumEntities.AddObject(se);
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public bool RemoveSubforum(string subforum)
        {
            try
            {
                IEnumerable<SubforumEntity> subforumsQuery = from s in ForumContext.SubforumEntities
                                                             where s.Name == subforum
                                                             select s;
                ForumContext.SubforumEntities.DeleteObject(subforumsQuery.First());
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }

        }

        public Subforum GetSubforum(string subforumName)
        {
            try
            {
                // Get SubforumEntity
                IEnumerable<SubforumEntity> subforumsQuery = from s in ForumContext.SubforumEntities
                                                             where s.Name == subforumName
                                                             select s;
                return GetSubforum(subforumsQuery.First());
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        private Subforum GetSubforum(SubforumEntity subforumEntity)
        {
            string subforumName = subforumEntity.Name;
            SubforumEntity se = subforumEntity;
            Subforum subforum = new Subforum(se.Name);
            subforum.Description = se.Description;
            Dictionary<Postkey, Post> subforumPostsDic = new Dictionary<Postkey, Post>();
            // Get subforum`s posts
            IEnumerable<PostEntity> postsQuery = from p in ForumContext.PostEntities
                                                 where p.SubforumName == subforumName && p.ParentPostKeyId == -1
                                                 select p;
            foreach (PostEntity post in postsQuery)
            {
                Post p = PostEntityToPost(post);
                subforumPostsDic.Add(p.Key, p);
            }
            subforum.Posts = subforumPostsDic;
            subforum.TotalPosts = postsQuery.Count();

            // Get subforum`s moderator:
            List<string> moderatorsList = new List<string>();
            IEnumerable<ModeratorEntity> moderatorsQuery = from m in ForumContext.ModeratorEntities
                                                           where m.Subforum == subforumName
                                                           select m;
            foreach (ModeratorEntity moderator in moderatorsQuery)
            {
                moderatorsList.Add(moderator.Username);
            }
            subforum.ModeratorsList = moderatorsList;

            return subforum;
        }

        public List<Subforum> GetSubforums()
        {
            List<Subforum> result = new List<Subforum>();
            IEnumerable<SubforumEntity> allSubforums = from s in ForumContext.SubforumEntities
                                                       select s;
            foreach (SubforumEntity subforum in allSubforums)
            {
                result.Add(GetSubforum(subforum));
            }
            return result;
        }


        // TODO - Can a user be moderator on more than one subforum?


        public List<string> GetModerators(string subforum)
        {
            try
            {
                List<string> result = new List<string>();
                IEnumerable<ModeratorEntity> moderatorsQuery = from m in ForumContext.ModeratorEntities
                                                               where m.Subforum == subforum
                                                               select m;
                foreach (ModeratorEntity moderator in moderatorsQuery)
                {
                    result.Add(moderator.Username);
                }
                return result;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public bool SetModerators(string subforum, List<string> moderatorsList)
        {
            try
            {
                foreach (string modName in moderatorsList)
                {
                    ModeratorEntity me = new ModeratorEntity();
                    me.Username = modName;
                    me.Subforum = subforum;
                    ForumContext.ModeratorEntities.AddObject(me);

                    IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                         where u.UserName == modName
                                                         select u;
                    usersQuery.First().Authentication = AuthorizationLevel.MODERATOR.ToString();

                }
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }


        // Add to interface
        public bool RemoveModerator(string subforum, string moderatorName)
        {
            try
            {
                // Update in moderators table
                IEnumerable<ModeratorEntity> getModeratorQuery = from m in ForumContext.ModeratorEntities
                                                                 where m.Subforum == subforum && m.Username == moderatorName
                                                                 select m;


                // Update in users table

                IEnumerable<ModeratorEntity> userIsStillModeratorQuery = from m in ForumContext.ModeratorEntities
                                                                         where m.Username == moderatorName
                                                                         select m;
                UserEntity ue = null;
                if (userIsStillModeratorQuery.Count() == 1)     // If user was just moderator of subforum then change his status in yblUsers
                {
                    IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                         where u.UserName == moderatorName
                                                         select u;
                    ue = usersQuery.First();
                    ue.Authentication = AuthorizationLevel.MEMBER.ToString();
                }
                ForumContext.ModeratorEntities.DeleteObject(getModeratorQuery.First());

                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                UserEntity ue = new UserEntity();
                ue.UserName = user.Username;
                ue.Password = user.Password;
                ue.Authentication = user.Level.ToString();
                ue.State = user.CurrentState.ToString();
                //TODO - Add friends
                ForumContext.UserEntities.AddObject(ue);
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.UserName == username
                                                     select u;
                if (usersQuery.Count() != 1)
                {
                    // User wasn't found or more than one was found
                    return null;
                }
                else
                {
                    UserEntity ue = usersQuery.First();
                    User user = new User(ue.UserName.Trim(), ue.Password.Trim());
                    switch (ue.Authentication.Trim())
                    {
                        case "GUEST":
                            user.Level = AuthorizationLevel.GUEST;
                            break;
                        case "MEMBER":
                            user.Level = AuthorizationLevel.MEMBER;
                            break;
                        case "MODERATOR":
                            user.Level = AuthorizationLevel.MODERATOR;
                            break;
                        case "ADMIN":
                            user.Level = AuthorizationLevel.ADMIN;
                            break;
                        default:
                            break;
                    }


                    switch (ue.State.Trim())
                    {
                        case "Login":
                            user.CurrentState = UserState.Login;
                            break;
                        case "Logout":
                            user.CurrentState = UserState.Logout;
                            break;
                        default:
                            break;
                    }
                    // TODO - Add friends:
                    user.Friends = null;

                    return user;
                }
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.UserName == user.Username
                                                     select u;
                UserEntity ue = usersQuery.First();
                ue.Password = user.Password;
                ue.State = user.CurrentState.ToString();
                ue.Authentication = user.Level.ToString();
                //TODO:
                //ue.friends - TODO
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public bool SetUserState(string username, UserState state)
        {
            try
            {
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.UserName == username
                                                     select u;
                UserEntity ue = usersQuery.First();
                ue.State = state.ToString();
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public List<Post> GetUserPosts(string username)
        {
            List<Post> result = new List<Post>();
            try
            {
                IEnumerable<PostEntity> postsQuery = from pk in ForumContext.PostKeyEntities
                                                     from post in ForumContext.PostEntities
                                                     where pk.PostKeyId == post.PostKeyId && pk.Username == username
                                                     select post;
                foreach (PostEntity pe in postsQuery)
                {
                    result.Add(PostEntityToPost(pe));
                }
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool SetAdmin(User admin)
        {
            try
            {
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.UserName == admin.Username
                                                     select u;
                UserEntity ue = usersQuery.First();
                ue.Authentication = AuthorizationLevel.ADMIN.ToString();
                ForumContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        public User GetAdmin()
        {
            try
            {
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.Authentication == "ADMIN"
                                                     select u;
                if (usersQuery.Count() == 1)
                {

                    UserEntity ue = usersQuery.First();
                    return GetUser(ue.UserName);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                //TODO
                throw;
            }
        }

        // Add to interface
        public bool ChangeAdmin(User newAdmin, User oldAdmin)
        {
            throw new NotImplementedException();
        }




        public List<User> GetAllLoggedInUsers()
        {
            try
            {
                List<User> result = new List<User>();
                IEnumerable<UserEntity> usersQuery = from u in ForumContext.UserEntities
                                                     where u.State == "Login"
                                                     select u;
                foreach (UserEntity user in usersQuery)
                {
                    result.Add(GetUser(user.UserName));
                }
                return result;
            }
            catch (Exception)
            {
                //TODO
                throw;
            }

        }
    }
}