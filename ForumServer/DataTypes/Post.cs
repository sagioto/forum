using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataTypes
{
    public class Post : IComparable
    {
        private Postkey key;
        private string title;
        private Post parentPost;
        private string body;
        private Dictionary<Postkey, Post> replies;
        private string username;
        private DateTime time;

        #region Fields Properties


        public Postkey Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
        

        public Post ParentPost
        {
            get
            {
                return parentPost;
            }
            set
            {
                parentPost = value;
            }
        }


        public String Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
            }
        }

        public String Body
        {
            get
            {
                return body;
            }
            set
            {
                body = value;
            }
        }



        public Dictionary<Postkey, Post> Replies
        {
            get
            {
                return replies;
            }
            set
            {
                replies = value;
            }
        }


        public String Username
        {
            get
            {
                return username;
            }
            set
            {
                username = value;
            }
        }

        public DateTime Time
        {
            get
            {
                return time;
            }
            set
            {
                time = value;
            }
        }

#endregion

        #region IComparable Members
        public int CompareTo(object p)
        {
            return this.key.CompareTo(((Post)p).Key);
        }
        #endregion

    }
}