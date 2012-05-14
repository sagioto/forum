using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ForumUtils.SharedDataTypes;

namespace ForumUtils.NetworkLayer
{
    public class JsonSerializer : ISerializer
    {
        private JavaScriptSerializer serializer;

        public JsonSerializer()
        {
            serializer = new JavaScriptSerializer();
            //serializer.RecursionLimit = 100;
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