using ForumServer.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Collections.Generic;

namespace ServerTests
{
    
    
    /// <summary>
    ///This is a test class for PostServerTests and is intended
    ///to contain all PostServerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class PostServerTests
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
        ///A test for Post Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void PostConstructorServerTests()
        {
            Postkey postKey = new Postkey("dor", DateTime.Now);
            string title = "New Post";
            Postkey parentPost = null;
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddReply
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void AddReplyServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); // TODO: Initialize to an appropriate value
            Post reply = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddReply(reply);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void CompareToServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); // TODO: Initialize to an appropriate value
            object p = null; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.CompareTo(p);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Body
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void BodyServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost,subforum); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Body = expected;
            actual = target.Body;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Key
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void KeyServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); // TODO: Initialize to an appropriate value
            Postkey expected = null; // TODO: Initialize to an appropriate value
            Postkey actual;
            target.Key = expected;
            actual = target.Key;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ParentPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ParentPostServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); 
            Postkey expected = null; // TODO: Initialize to an appropriate value
            Postkey actual;
            target.ParentPost = expected;
            actual = target.ParentPost;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Replies
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void RepliesServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); // TODO: Initialize to an appropriate value
            Dictionary<Postkey, Post> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<Postkey, Post> actual;
            target.Replies = expected;
            actual = target.Replies;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Title
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void TitleServerTests()
        {
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            string title = string.Empty; // TODO: Initialize to an appropriate value
            Postkey parentPost = null; // TODO: Initialize to an appropriate value
            Subforum subforum = null;
            Post target = new Post(postKey, title, parentPost, subforum); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            target.Title = expected;
            actual = target.Title;
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
