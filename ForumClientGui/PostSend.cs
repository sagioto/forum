using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ForumClientGui
{
    public class PostSend
    {
        private string subforum;
        private string title;
        private string body;

        public PostSend(string subforum, string title, string body)
        {
            this.subforum = subforum;
            this.title = title;
            this.body = body;
        }

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


        public string Title
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


        public string Body
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


    }
}
