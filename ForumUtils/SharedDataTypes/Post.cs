using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace ForumShared.SharedDataTypes
{
    [DataContract]
    public class Post : IComparable
    {
        private Postkey key;
        private string title;
        private Postkey parentPost;
        private string body;
        private string subforum;
        private Post postToUpdate;
        private bool hasReplies;

        

        [NonSerialized]
        private Dictionary<Postkey, Post> replies;

        /// <summary>
        /// Default constructor. Needed for DataManger.
        /// </summary>
        public Post(){}

        public Post(Postkey postKey, string title, string body, Postkey parentPost, string subforum)
        {
            this.key = postKey;
            this.title = title;
            this.parentPost = parentPost;
            this.subforum = subforum;
            this.replies = new Dictionary<Postkey, Post>();
            this.body = body;
        }

        public Post(Post postToCopy)
        {
            this.postToUpdate = postToCopy;
        }

        #region Fields Properties

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
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

        [DataMember]
        public string Subforum
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

        [DataMember]
        public bool HasReplies
        {
            get
            {
                return hasReplies;
            }
            set
            {
                hasReplies = value;
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