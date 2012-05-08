using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ForumServer.DataLayer
{
    public class DataManager : IDataManager
    {

        public bool AddPost(DataTypes.Post post, string subforum)
        {
            throw new NotImplementedException();
        }

        public bool AddReply(DataTypes.Post reply, DataTypes.Postkey originalPost)
        {
            throw new NotImplementedException();
        }

        public bool EditPost(DataTypes.Post postToUpdate, DataTypes.Postkey originalPost)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, DataTypes.Subforum> getSubforumsDic()
        {
            throw new NotImplementedException();
        }

        public DataTypes.Subforum GetSubforum(string subforum)
        {
            throw new NotImplementedException();
        }

        public DataTypes.User GetUser(string username)
        {
            throw new NotImplementedException();
        }

        public bool UpdateUser(DataTypes.User user)
        {
            throw new NotImplementedException();
        }

        public List<string> GetModerators(string subforum)
        {
            throw new NotImplementedException();
        }

        public bool SetModerators(string subforum)
        {
            throw new NotImplementedException();
        }
    }
}