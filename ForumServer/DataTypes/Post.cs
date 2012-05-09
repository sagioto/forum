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
        private Postkey parentPost;
        private string body;
        private Dictionary<Postkey, Post> replies;
        private Subforum subforum;


        //TODO - Why Subforum needed? (To Sagi)
        public Post(Postkey postKey, string title, Postkey parentPost, Subforum subforum)
        {
            this.key = postKey;
            this.title = title;
            this.parentPost = parentPost;
            this.subforum = subforum;
            this.replies = new Dictionary<Postkey, Post>();
        }

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


        public Postkey ParentPost
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

        public Subforum Subforum
        {
            get
            {
                return subforum;
            }
            set
            {
                subforum = value;
            }
        }




        #endregion

        #region Public methods

        public bool AddReply(Post reply)
        {
            try
            {
                this.replies.Add(reply.Key, reply);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
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