using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumClientCore;

namespace ForumTests
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ClientTests
    {
        public ClientTests()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion
        
        [TestMethod]
        public void LoginRegisterTests()
        {
            ClientController cc = new ClientController();
            Assert.IsFalse(cc.Login("user1", "123456"));
            Assert.IsTrue(cc.Register("alice", "123456"));

            //login tests
            for (int i = 0; i < 100; i++){

                Assert.IsTrue(cc.Register("alice" + i, "123456"));
                Assert.IsFalse(cc.Register("alice" + i, "123456"));

                Assert.IsTrue(cc.Login("alice" + i, "123456"));
                //failed: try to login twice with same userName return true , should return false.
            //    Assert.IsFalse(cc.Login("alice" + i, "123456"));

                Assert.IsFalse(cc.Login("bob" + i, "123456"));
                Assert.IsFalse(cc.Login("alice" + i, "123456"+i));
            }

            
        }

        [TestMethod]
        public void LogoutTests()
        {
            // logout tests
            ClientController cc2 = new ClientController();
            Assert.IsFalse(cc2.Logout());//try to logout without any register and login

            cc2.Register("alice", "123456");
            Assert.IsTrue(cc2.Logout());//try to logout without login
        }

        [TestMethod]
        public void postTest()
        {
            ClientController cc = new ClientController();
            cc.Register("test1","123456");
            cc.Login("test1","123456");

            Assert.IsInstanceOfType(cc.GetSubforumsList(), typeof(String[]));

            String[] SubForumArray = cc.GetSubforumsList();


            for (int i = 0; i < SubForumArray.Length; i++)
            {
                Assert.IsTrue(cc.Post(SubForumArray[i],"title"+i,"body"+i)); //post message in all sub forums
            }

            Assert.IsFalse(cc.Post("XXXYYYZZZ","badTitle","badBody"));//post message in sub forum that isn"t exists


        }

    }
}
