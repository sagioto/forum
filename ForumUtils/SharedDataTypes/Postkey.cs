﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Runtime.Serialization;

namespace ForumUtils.SharedDataTypes
{
    [DataContract]
    public class Postkey : IEquatable<Postkey>//IComparer //IEqualityComparer
    {
        private string username;
        private DateTime time;

        public Postkey(string username, DateTime time)
        {
            this.username = username;
            this.time = time;
        }

        #region Parameter Properties

        [DataMember]
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

        [DataMember]
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
                    return this.username.CompareTo(postkey.Username);
                }
            }
        }
        #endregion


        public bool Equals(Postkey otherPk)
        {
            return ((this.username == otherPk.Username) && (this.time.CompareTo(otherPk.Time) == 0));
        }
    }
}