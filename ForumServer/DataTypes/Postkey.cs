﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ForumServer.DataTypes
{
    public class Postkey : IComparable, IComparer //IEqualityComparer
    {
        private string username;
        private DateTime time;

        public Postkey(string username, DateTime time)
        {
            this.username = username;
            this.time = time;
        }

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
                    return this.username.CompareTo(postkey.Username);
                }
            }
        }
        #endregion

        public bool Equals(Postkey otherPk)
        {
            return (this.username == otherPk.Username && this.time.ToString() == otherPk.Time.ToString());
        }

        public bool Equals(object x, object y)
        {
            Postkey pk1 = (Postkey)x;
            Postkey pk2 = (Postkey)y;
            return (pk1.username == pk2.Username && pk1.time.ToString() == pk2.Time.ToString());
        }

        public int GetHashCode(object obj)
        {
            return ((Postkey)obj).Time.Minute;  //TODO Think about it again
        }

        //TODO - Only for test
        public int Compare(object x, object y)
        {
            Postkey pk1 = (Postkey)x;
            Postkey pk2 = (Postkey)y;
            if (pk1.username == pk2.Username && pk1.time.ToString() == pk2.Time.ToString())
                return 0;
            else
            {
                return 1;
            }
        }
    }
}