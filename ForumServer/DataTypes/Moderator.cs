using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataTypes
{
    public class Moderator : User
    {
        private List<Subforum> mananged = new List<Subforum>();

        public Moderator(string username, string password) : 
        base(username, password)
        {
            this.Level = AuthorizationLevel.MODERATOR;
        }

        public List<Subforum> Mananged
        {
            get { return mananged; }
            set { mananged = value; }
        }
    }
}