using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataTypes
{
    public class Subforum : IComparable
    {
        private string name;
        private List<string> moderatorsList;
        private Dictionary<Postkey, Post> posts;

        public Subforum(string name)
        {
            this.name = name;
            moderatorsList = new List<string>();
            posts = new Dictionary<Postkey, Post>();

        }

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

        #region Subforum public methods

        public bool AddPost(Post p)
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

        public int CompareTo(object subforum)
        {
            return this.name.CompareTo(((Subforum)subforum).Name);
        }
    }
}