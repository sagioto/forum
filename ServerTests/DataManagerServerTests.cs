using ForumServer.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataTypes;
using System.Collections.Generic;
using ForumUtils.SharedDataTypes;

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
        }

        /// <summary>
        ///A test for AddPost
        ///</summary>
        [TestMethod()]
        public void AddPostServerTests()
        {
            DataManager target = new DataManager();
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("dor", DateTime.Now);
            bool actual = target.AddPost(new Post(pk, "Post", null, null), "subforumName");
            Assert.AreEqual(target.GetSubforum("subforumName")!=null, actual);
        }

        /// <summary>
        ///A test for AddReply
        ///</summary>
        [TestMethod()]
        public void AddReplyServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.AddSubforum(new Subforum("subforum"));
            Postkey pk = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "Post", null, null), "subforumName");
            //Post reply = new Post(new Postkey("dor", DateTime.Now), "Reply", null, null);
            Post reply = new Post(new Postkey("dor222", pk.Time), "Reply", null, null);
            reply.Body = "reply body";
            bool ans = target.AddReply(reply, pk);
            //Post reply2 = new Post(new Postkey("dor", DateTime.Now), "Reply2 - new Update", null, null);
            Post reply2 = new Post(new Postkey("dor222", pk.Time), "Reply2 - new Update", null, null);
            reply2.Body = " reply 2 body";
            //ans = target.AddReply(reply2, reply.Key);
            //reply.Replies.Add(reply2.Key, reply2);
            //reply.Replies.ContainsKey(reply2.Key);
            //bool ans2 = target.EditPost(reply2, reply.Key);
            Assert.IsTrue(ans);
            Assert.AreEqual(target.GetPost(reply.Key) != null, ans);
        }

        /// <summary>
        ///A test for EditPost
        ///</summary>
        [TestMethod()]
        public void EditPostServerTests()
        {
            DataManager target = new DataManager();
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "Post", null, null), "subforumName");
            //Post reply = new Post(new Postkey("dor", DateTime.Now), "Reply", null, null);
            Post reply = new Post(new Postkey("dor222", pk.Time), "Reply", null, null);
            reply.Body = "reply body";
            bool ans = target.AddReply(reply, pk);
            //Post reply2 = new Post(new Postkey("dor", DateTime.Now), "Reply2 - new Update", null, null);
            Post reply2 = new Post(new Postkey("dor222", pk.Time), "Reply2 - new Update", null, null);
            reply2.Body = " reply 2 body";
            bool ans2 = target.EditPost(reply2, reply.Key);
            Assert.IsTrue(ans2); // Need to check with debugger the content of reply
        }

        /// <summary>
        ///A test for GetModerators
        ///</summary>
        [TestMethod()]
        public void GetModeratorsServerTests()
        {
            DataManager target = new DataManager();
            string subforum = "subforum";
            List<string> actual;
            target.AddSubforum(new Subforum(subforum));
            List<string> moderators = new List<string>();
            moderators.Add("dagan");
            moderators.Add("sagi");
            moderators.Add("romi");
            target.SetModerators(subforum,moderators);
            actual = target.GetModerators(subforum);
            Assert.AreEqual(moderators, actual);
        }

        /// <summary>
        ///A test for GetPost
        ///</summary>
        [TestMethod()]
        public void GetPostServerTests()
        {
            DataManager_Accessor target = new DataManager_Accessor();
            target.AddSubforum(new Subforum("subforumName"));
            Postkey pk = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "Post", null, null), "subforumName");
            Post p = target.GetPost(pk);
            Assert.IsNotNull(p);
        }

        /// <summary>
        ///A test for GetSubforum
        ///</summary>
        [TestMethod()]
        public void GetSubforumServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            string subforum = "subforum";
            Subforum actual = null;
            try
            {
                actual = target.GetSubforum(subforum);
            }
            catch (SubforumNotFoundException)
            {
                Assert.IsNull(actual);
            }
            bool ans = target.AddSubforum(new Subforum(subforum));
            actual = target.GetSubforum(subforum);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        [TestMethod()]
        public void GetUserServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.AddUser(new User("dor", "dor"));
            string username = "dor";
            User actual;
            actual = target.GetUser(username);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SetModerators
        ///</summary>
        [TestMethod()]
        public void SetModeratorsServerTests()
        {
            DataManager target = new DataManager();
            string subforum = "subforum";
            List<string> actual;
            target.AddSubforum(new Subforum(subforum));
            List<string> moderators = new List<string>();
            moderators.Add("dagan");
            moderators.Add("sagi");
            moderators.Add("romi");
            target.SetModerators(subforum, moderators);
            actual = target.GetModerators(subforum);
            Assert.AreEqual(moderators, actual);
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        [TestMethod()]
        public void UpdateUserServerTests()
        {
            DataManager target = new DataManager();
            User user = new User("dor", "dor");
            target.AddUser(user);
            user.Level = AuthorizationLevel.ADMIN;
            user.Password = "bla";
            bool actual;
            actual = target.UpdateUser(user);
            Assert.AreEqual(user, target.GetUser("dor"));
        }

        /// <summary>
        ///A test for GetUserPosts
        ///</summary>
        [TestMethod()]
        public void GetUserPostsServerTests()
        {
            DataManager target = new DataManager();
            string username = "dor";
            Subforum subforum = new Subforum("subforum1");
            target.AddSubforum(subforum);
            Postkey pk = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "post1", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor", DateTime.Now), "post2", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor2", DateTime.Now), "post3", null, null), "subforum1");
            target.AddReply(new Post(new Postkey("dor", DateTime.Now), "reply1 to post1", null, null), pk);
            List<Post> actual;
            actual = target.GetUserPosts(username);
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for RemovePost
        ///</summary>
        [TestMethod()]
        public void RemovePostServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            Subforum subforum = new Subforum("subforum1");
            target.AddSubforum(subforum);
            Postkey pk = new Postkey("dor", DateTime.Now);
            Postkey pk2 = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "post1", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor", DateTime.Now), "post2", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor2", DateTime.Now), "post3", null, null), "subforum1");
            target.AddReply(new Post(pk2, "reply1 to post1", null, null), pk);
            bool actual;
            actual = target.RemovePost(pk2);
            Assert.IsTrue(actual);
        }

        /// <summary>
        ///A test for InitForumData
        ///</summary>
        [TestMethod()]
        public void InitForumDataServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            target.InitForumData();
            Assert.IsNotNull(target.GetSubforums());
        }

        /// <summary>
        ///A test for GetAllPosts
        ///</summary>
        [TestMethod()]
        public void GetAllPostsServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            Subforum subforum = new Subforum("subforum1");
            target.AddSubforum(subforum);
            Postkey pk = new Postkey("dor", DateTime.Now);
            Postkey pk2 = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "post1", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor", DateTime.Now), "post2", null, null), "subforum1");
            target.AddPost(new Post(new Postkey("dor2", DateTime.Now), "post3", null, null), "subforum1");
            target.AddReply(new Post(pk2, "reply1 to post1", null, null), pk);
            List<Post> actual;
            actual = target.GetAllPosts();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for SetAdmin
        ///</summary>
        [TestMethod()]
        public void SetAdminServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            User admin = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.SetAdmin(admin);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetAdmin
        ///</summary>
        [TestMethod()]
        public void GetAdminServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = target.GetAdmin();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
