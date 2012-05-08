using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ForumServer.DataTypes;

namespace ForumServer.NetworkLayer
{
    public class JsonSerializer : ISerializer
    {
        private JavaScriptSerializer serializer;

        public JsonSerializer()
        {
            serializer = new JavaScriptSerializer();
        }


        public string SerializeSubforum(Subforum toSerialize)
        {
            return serializer.Serialize(toSerialize);
        }

        public Subforum DeserializeSubforum(string toDeserialize)
        {
            return serializer.Deserialize(toDeserialize, typeof(Subforum)) as Subforum;
        }

        public string SerializePost(Post toSerialize)
        {
            return serializer.Serialize(toSerialize);
        }

        public Post DeserializePost(string toDeserialize)
        {
            return serializer.Deserialize(toDeserialize, typeof(Post)) as Post;
        }

        public string SerializePostkey(Postkey toSerialize)
        {
            return serializer.Serialize(toSerialize);
        }

        public Postkey DeserializePostkey(string toDeserialize)
        {
            return serializer.Deserialize(toDeserialize, typeof(Postkey)) as Postkey;
        }

        public string Serialize(object toSerialize)
        {
            return serializer.Serialize(toSerialize);
        }

        public object Deserialize(string toDeserialize, Type type)
        {
            return serializer.Deserialize(toDeserialize, type);
        }


    }
}