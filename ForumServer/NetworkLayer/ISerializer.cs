﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ForumServer.DataTypes;

namespace ForumServer.NetworkLayer
{
    interface ISerializer
    {
        string SerializeSubforum(Subforum toSerialize);

        Subforum DeserializeSubforum(string toDeserialize);

        string SerializePost(Post toSerialize);

        Post DeserializePost(string toDeserialize);

        string SerializePostkey(Postkey toSerialize);

        Postkey DeserializePostkey(string toDeserialize);

    }
}