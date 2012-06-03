using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumServer;
using ForumShared.SharedDataTypes;
using System.Threading;
using ForumShared.ForumAPI;

namespace ServerTests
{
    [TestClass]
    public class subscribeTest
    {
        ServerNetworkAdaptor sna;
        [TestMethod]
        public void TestMethod1()
        {
           sna = new ServerNetworkAdaptor();
           // Thread t = new Thread(getResult);
           // t.Start();
            Result r = sna.Post("Cars", new Post(new Postkey("dor", DateTime.Now), "Test", "Body", null, "Cars"));
            Console.WriteLine(r.ToString());
        }
        private void getResult()
        {
            Post p = sna.Subscribe("guest");
            Console.WriteLine();
        }
    }
}
