﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Runtime.Serialization;

namespace ForumUtils.SharedDataTypes
{
    [DataContract]
    public class Postkey : IEqualityComparer<Postkey>// IEquatable<Postkey>//IComparer //IEqualityComparer
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
            if (this.time.Millisecond < postkey.Time.Millisecond)  //this.time < postkey.Time
            {
                return -1;
            }
            else    //this.time > postkey.Time
            {
                if (this.time.Millisecond > postkey.Time.Millisecond)  //this.time > postkey.Time
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


        //public override bool Equals(Postkey otherPk)
        //{
        //    return ((this.username == otherPk.Username) && (this.time.CompareTo(otherPk.Time) == 0));
        //}

        //public override int GetHashCode()
        //{
        //    if (time == null)
        //        return 0;
        //    return time.GetHashCode();
        //}

        public bool Equals(Postkey x, Postkey y)
        {

            if (x.time.CompareTo(y.Time) != 0)
            {
                return false;
            }
            else
            {
                return this.username.Equals(y.Username);
            }
        }


        public int GetHashCode(Postkey obj)
        {
            return obj.Time.Millisecond;
        }



    }
}