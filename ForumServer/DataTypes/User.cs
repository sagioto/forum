using System.Collections.Generic;

namespace ForumServer.DataTypes
{
    public class User
    {
        private string username;
        private string password;
        private List<string> friends;
        //private Post currentPost;
        //private Set<Post> posts;
        private AuthorizationLevel level;
        private UserState currentState;



        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            friends = new List<string>();
            this.level = AuthorizationLevel.GUEST;
            currentState = UserState.Logout;
        }

        #region Parameters Properties

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


        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                password = value;
            }
        }


        public List<string> Friends
        {
            get
            {
                return friends;
            }
            set
            {
                friends = value;
            }
        }


        public AuthorizationLevel Level
        {
            get
            {
                return level;
            }
            set
            {
                level = value;
            }
        }


        public UserState CurrentState
        {
            get
            {
                return currentState;
            }
            set
            {
                currentState = value;
            }

        #endregion

        }
    }
}