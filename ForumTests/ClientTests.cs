using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForumClientCore;
using ForumShared.ForumAPI;
using ForumShared.SharedDataTypes;

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

            Assert.IsFalse(cc.Login("user1", "123456"));//login before register

            Assert.AreEqual(Result.OK, cc.Register("alice", "123456"));

            //login tests - torture test
            for (int i = 0; i < 100; i++)
            {
                // try to register twice with same userName
                Assert.AreEqual(Result.OK, cc.Register("alice" + i, "123456"));
                Assert.AreEqual(Result.SECURITY_ERROR, cc.Register("alice" + i, "123456"));

                //failed: try to login twice with same userName return true , should return false.
                Assert.IsTrue(cc.Login("alice" + i, "123456"));
                Assert.IsFalse(cc.Login("alice" + i, "123456"));

                Assert.IsFalse(cc.Login("bob" + i, "123456"));//try to login with bad unknown user
                Assert.IsFalse(cc.Login("alice" + i, "123456" + i));//try to login with bad password
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

            cc2.Login("alice", "123456");
            Assert.IsTrue(cc2.Logout());//try to logout after login
        }

        [TestMethod]
        public void postTest()
        {
            ClientController cc = new ClientController();
            cc.Register("test1", "123456");
            cc.Login("test1", "123456");

            //Assert.IsInstanceOfType(cc.GetSubforumsList(), typeof(String[]));

            string[] SubForumArray = cc.GetSubforumsList();

            for (int i = 0; i < SubForumArray.Length; i++)
            {
                Assert.AreEqual(Result.OK, cc.Post(SubForumArray[i], "title" + i, "body" + i)); //post message in all sub forums
            }

            Assert.AreEqual(Result.ILLEGAL_POST, cc.Post("XXXYYYZZZ", "badTitle", "badBody"));//post message in sub forum that isn"t exists

        }


        [TestMethod]
        public void UserIntegration1()
        {

            ClientController cc1 = new ClientController();

            cc1.Login("admin", "admin");

            ClientController cc2 = new ClientController();
            cc2.Register("test2", "123456");
            cc2.Login("test2", "123456");


            ClientController cc3 = new ClientController();
            cc2.Register("test3", "123456");
            cc2.Login("test3", "123456");

            //try to add moderator by non-admin
            Assert.AreEqual(Result.SECURITY_ERROR, cc2.AddModerator("test2", "Woman"));

            cc1.AddModerator("test2", "Woman");

            //to this subforum has already moderator
            Assert.AreEqual(Result.OK, cc1.AddModerator("test2", "Woman"));

            cc2.Post("Woman", "msg2", "body2");
            cc3.Post("Woman", "msg3", "body3");

            //try to replace moderator by non-admin (see: cc2 is the contrller of userName test2)
            Assert.AreEqual(Result.SECURITY_ERROR, cc2.ReplaceModerator("test3", "test2", "Woman"));

            cc1.ReplaceModerator("test3", "test2", "Woman");

            //try to edit message by non-moderator
          //  Assert.AreEqual(Result.SECURITY_ERROR, cc2.EditPost("hehe", "bebe"), "hellow evil world");

            //try to add subform by non-admin
            Assert.AreEqual(Result.SECURITY_ERROR, cc2.AddSubforum("badSubForum"));

            //try to add subform by admin
            Assert.AreEqual(Result.OK, cc1.AddSubforum("bestFrum"));

        }

        [TestMethod]
        public void UserIntegration2()
        {
            ClientController cc1 = new ClientController();

            cc1.Login("admin", "admin");

            ClientController cc2 = new ClientController();
            cc2.Register("test2", "123456");
            cc2.Login("test2", "123456");

            ClientController cc3 = new ClientController();
            cc2.Register("test3", "123456");
            cc2.Login("test3", "123456");

            cc2.Post("Woman", "title1", "body1");

            Post[] posts = cc2.GetSubforum("Woman");
            Postkey tmp_postKey = null;
            for (int i = 0; i < posts.Length; i++)
            {
                if (posts[i].Title.Equals("title1"))
                    tmp_postKey = posts[i].Key;
            }

                //try to edit message not by the writer, admin or moderator
                Assert.AreEqual(Result.SECURITY_ERROR, cc3.EditPost(tmp_postKey, "BadTitle", "BadBody"));

                //try to edit message by admin
                Assert.AreEqual(Result.OK, cc2.EditPost(tmp_postKey, "GoodTitle", "GoodBody"));

                ClientController cc4 = new ClientController();
                cc1.ReplaceAdmin("newAdmin", "newAdmin"); //the newAdmin will create at the server

                //try to login with the new admin (the registeration was at the replaceAdmin)
                Assert.IsTrue(cc4.Login("newAdmin", "newAdmin"));

                //logic:
                //  if the old admin was just admin (with out any moderator of any subforum)
                //      then he will become regular member.
                //  if the old admin was moderator of any forum
                //      then he will be just the relevant forum moderator (and no admin).

                //try to create subform by non-admin
                Assert.AreEqual(Result.SECURITY_ERROR, cc1.AddSubforum("badbadForum"));

                //try to edit message by non-admin or non moderator
                //   Assert.AreEqual(Result.SECURITY_ERROR, cc1.EditPost("xxx", "yyy"));

                //try to remove non existing subforum (by admin)
                Assert.AreEqual(Result.SECURITY_ERROR, cc4.RemoveSubforum("Forums"));

                //try to remove subforum by non-admin
                Assert.AreEqual(Result.SECURITY_ERROR, cc1.RemoveSubforum("Woman"));
            }

        }
    }

