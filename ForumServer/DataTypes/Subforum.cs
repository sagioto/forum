using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataTypes
{
    public class Subforum
    {
        private string name;
        private List<string> moderatorsList;
        private Dictionary<Postkey, Post> posts;

        #region Parameters Properties

        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
            }
        }


        public List<string> ModeratorsList
        {
            get
            {
                return moderatorsList;
            }
            set
            {
                moderatorsList = value;
            }
        }


        public Dictionary<Postkey, Post> Posts
        {
            get
            {
                return posts;
            }
            set
            {
                posts = value;
            }
        }

        #endregion

        #region Subforum methods

        public bool addPost(Post p)
        {
            try
            {
                posts.Add(p.Key, p);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}