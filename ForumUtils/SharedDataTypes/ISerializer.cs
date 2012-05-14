using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumUtils.SharedDataTypes;

namespace ForumUtils.NetworkLayer
{
    public interface ISerializer
    {
        string SerializePost(Post toSerialize);

        Post DeserializePost(string toDeserialize);

        string SerializePostkey(Postkey toSerialize);

        Postkey DeserializePostkey(string toDeserialize);

        string Serialize(object toSerialize);

        object Deserialize(string toDeserialize, Type type);

    }
}
