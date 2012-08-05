using ForumServer.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataTypes;
using System.Collections.Generic;
using System.Threading;
using ForumShared.SharedDataTypes;
using ForumUtils.SharedDataTypes;
using ForumServer;

namespace ServerTests
{
    /// <summary>
    ///This is a test class for DataManagerServerTests and is intended
    ///to contain all DataManagerServerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class DataManagerServerTests
    {


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
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for DataManager Constructor
        ///</summary>
        [TestMethod()]
        public void DataManagerConstructorServerTests()
        {
            DataManager target = new DataManager();
            Assert.IsNotNull(target);

            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
        }


        /// <summary>
        ///A test for AddPost
        ///</summary>
        [TestMethod()]
        public void AddPostServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("user", DateTime.Now);
            Post p = new Post(pk, "Post", "body", null, "subforumName");
            bool actual = target.AddPost(p, "subforumName");
            Post p2 = target.GetPost(pk);
            Assert.IsTrue(actual);
            Assert.AreEqual(p.Key.Time, p2.Key.Time);
            Assert.AreEqual(p.Key.Username, p2.Key.Username);
            Assert.AreEqual(p.Title, p2.Title);
            Assert.AreEqual(p.Body, p2.Body);
            target.RemoveSubforum("subforumName");
        }

        /// <summary>
        ///A test for AddReply
        ///</summary>
        [TestMethod()]
        public void AddReplyServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            Post post = new Post(new Postkey("user", DateTime.Now), "MainPost", "", null, "subforumName");
            Thread.Sleep(1000);
            Post reply = new Post(new Postkey("user", DateTime.Now), "ReplyToMainPost", "", post.Key, "subforumName");
            bool actualPost = target.AddPost(post, "subforumName");
            bool actualReply = target.AddReply(reply, post.Key);
            Post replyFromDB =  target.GetPost(reply.Key);
            Assert.IsTrue(actualPost);
            Assert.IsTrue(actualReply);
            Assert.AreEqual(reply.Key.Time, replyFromDB.Key.Time);
            Assert.AreEqual(reply.Key.Username, replyFromDB.Key.Username);
            Assert.AreEqual(reply.Title, replyFromDB.Title);
            Assert.AreEqual(reply.Body, replyFromDB.Body);
            Post postFromDB = target.GetPost(target.GetPost(reply.ParentPost).Key);
            Assert.AreEqual(post.Key.Time, postFromDB.Key.Time);
            Assert.AreEqual(post.Key.Username, postFromDB.Key.Username);
            Assert.AreEqual(post.Title, postFromDB.Title);
            Assert.AreEqual(post.Body, postFromDB.Body);
            target.RemoveSubforum("subforumName");
        }

        /// <summary>
        ///A test for EditPost
        ///</summary>
        [TestMethod()]
        public void EditPostServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("user", DateTime.Now);
            Thread.Sleep(1000);
            target.AddPost(new Post(pk, "Post", "", null, null), "subforumName");
            Post reply = new Post(new Postkey("user", DateTime.Now), "Reply", "", pk, null);
            Thread.Sleep(1000);
            reply.Body = "reply body";
            bool ans = target.AddReply(reply, pk);
            Post reply2 = new Post(new Postkey("user", DateTime.Now), "Reply2 - new Update", "", pk, null);
            reply2.Body = " reply 2 body";
            bool ans2 = target.EditPost(reply2, reply.Key);
            Assert.IsTrue(ans2); // Need to check with debugger the content of reply
            Post editedPost = target.GetPost(reply.Key);
            Assert.AreEqual(reply.Key.Time, editedPost.Key.Time);
            Assert.AreEqual(reply.Key.Username, editedPost.Key.Username);
            Assert.AreEqual(reply2.Title, editedPost.Title);
            Assert.AreEqual(reply2.Body, editedPost.Body);
            target.RemoveSubforum("subforumName");
        }

        /// <summary>
        ///A test for GetModerators
        ///</summary>
        [TestMethod()]
        public void GetModeratorsServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            List<string> actual;
            List<string> moderators = new List<string>();
            moderators.Add("user");
            target.SetModerators("subforumName", moderators);
            actual = target.GetModerators("subforumName");
            Assert.AreEqual(moderators.Count, actual.Count);
            Assert.AreEqual(moderators[0], actual[0]);
            target.RemoveModerator("subforumName", "user");
            target.RemoveSubforum("subforumName");
        }


        /// <summary>
        ///A test for GetSubforum
        ///</summary>
        [TestMethod()]
        public void GetSubforumServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            string subforum = "subforum";
            Subforum actual = null;
            try
            {
                actual = target.GetSubforum(subforum);
            }
            catch (Exception)
            {
                Assert.IsNull(actual);
            }
            bool ans = target.AddSubforum(new Subforum(subforum));
            actual = target.GetSubforum(subforum);
            Assert.IsNotNull(actual);
            target.RemoveSubforum("subforum");
            target.RemoveSubforum("subforumName");
        }


        /// <summary>
        ///A test for GetSubforum
        ///</summary>
        [TestMethod()]
        public void RemoveSubforumServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            string subforum = "subforum";
            Subforum actual = null;
            bool ans = target.AddSubforum(new Subforum(subforum));
            actual = target.GetSubforum(subforum);
            Assert.IsNotNull(actual);
            Assert.IsTrue(target.RemoveSubforum(subforum));
            try
            {
                actual = target.GetSubforum(subforum);
            }
            catch (Exception)
            {
                Assert.IsTrue(true);
            }
            target.RemoveSubforum("subforumName");
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User userTemp = new User("user", "user");
            target.AddUser(userTemp);
            target.AddSubforum(new Subforum("subforumName"));
            User user = target.GetUser("user");
            user.Password = "bla";
            bool actual;
            actual = target.UpdateUser(user);
            Assert.IsTrue(actual);
            Assert.AreEqual(user.Password, target.GetUser("user").Password);
            target.RemoveSubforum("subforumName");
        }


        /// <summary>
        ///A test for RemovePost
        ///</summary>
        [TestMethod()]
        public void RemovePostServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("user", DateTime.Now);
            Thread.Sleep(1001);
            Postkey pk2 = new Postkey("user", DateTime.Now);
            target.AddPost(new Post(pk, "post-TEST", "", null, ""), "SubforumName");
            target.AddReply(new Post(pk2, "reply-TEST", "body", pk, "SubforumName"), pk);
            bool actual;
            actual = target.RemovePost(pk2);
            Assert.IsTrue(actual);
            try
            {
                Assert.IsNull(target.GetPost(pk2));
            }
            catch (Exception)
            {

                Assert.IsTrue(true);
            }
            target.RemoveSubforum("subforumName");
        }


        [TestMethod()]
        public void RemoveModeratorsServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User user = new User("user", "user");
            target.AddUser(user);
            target.AddSubforum(new Subforum("subforumName"));
            List<string> actual;
            List<string> moderators = new List<string>();
            moderators.Add("user");
            target.SetModerators("subforumName", moderators);
            actual = target.GetModerators("subforumName");
            Assert.AreEqual(moderators.Count, actual.Count);
            Assert.AreEqual(moderators[0], actual[0]);
            Assert.IsTrue(target.RemoveModerator("subforumName", "user"));
            actual = target.GetModerators("subforumName");
            Assert.IsTrue(actual.Count == 0);
            target.RemoveSubforum("subforumName");
        }

        /// <summary>
        ///A test for DataManager Constructor
        ///</summary>
        [TestMethod()]
        public void AddUserServerTests()
        {
            DataManager target = new DataManager();
            target.CleanForumData();
            User user = new User("user2", "user2");
            target.AddUser(user);
            User user2 = target.GetUser("user2");
            Assert.AreEqual(user.Username, user2.Username);
            Assert.AreEqual(user.Password, user2.Password);
            target.CleanForumData();
        }

    }
}
