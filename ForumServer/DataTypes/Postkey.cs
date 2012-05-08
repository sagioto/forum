using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataTypes
{
    public class Postkey : IComparable
    {
        private string username;
        private DateTime time;

        #region Parameter Properties

        public string Username
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
        public int CompareTo(object pk)
        {
            Postkey postkey = (Postkey)pk;
            if (this.time.CompareTo(postkey.Time) < 0)  //this.time < postkey.Time
            {
                return -1;
            }
            else    //this.time > postkey.Time
            {
                if (this.time.CompareTo(postkey.Time) > 0)  //this.time > postkey.Time
                {
                    return 1;
                }
                else    //this.time == postkey.Time
                {
                    return this.time.CompareTo(postkey.Time);
                }
            }
        }
        #endregion
    }
}