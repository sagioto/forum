using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumServer;
using ForumShared.SharedDataTypes;

namespace ServerTests
{
    [TestClass]
    public class subscribeTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            ServerNetworkAdaptor a = new ServerNetworkAdaptor();
            Post p = a.Subscribe("guest");

        }
    }
}
