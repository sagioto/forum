using ForumServer.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataTypes;
using System.Collections.Generic;

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
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void DataManagerConstructorServerTests()
        {
            DataManager target = new DataManager();
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for AddPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void AddPostServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            Post post = null; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddPost(post, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
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
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            
            target.addSubforum(new Subforum("subforum"));
            Postkey pk = new Postkey("dor", DateTime.Now);
            target.AddPost(new Post(pk, "Post", null, null), "subforum");
            Post reply = new Post(new Postkey("dor", DateTime.Now), "Reply", null, null);
            bool ans = target.AddReply(reply, pk);
            Post reply2 = new Post(new Postkey("dor", DateTime.Now), "Reply2 - new Update", null, null);
            bool ans2 = target.EditPost(reply2, reply.Key);
            Assert.IsTrue(ans);
            Assert.IsTrue(ans2);
            //Postkey originalPost = null; // TODO: Initialize to an appropriate value
            //bool expected = false; // TODO: Initialize to an appropriate value
            //bool actual;
            //actual = target.AddReply(reply, originalPost);
            //Assert.AreEqual(expected, actual);
            //Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EditPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void EditPostServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            Post postToUpdate = null; // TODO: Initialize to an appropriate value
            Postkey originalPost = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.EditPost(postToUpdate, originalPost);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetModerators
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void GetModeratorsServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.GetModerators(subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        [DeploymentItem("ForumServer.dll")]
        public void GetPostServerTests()
        {
            DataManager_Accessor target = new DataManager_Accessor(); // TODO: Initialize to an appropriate value
            Postkey postKey = null; // TODO: Initialize to an appropriate value
            Post expected = target.GetPost(postKey); // TODO: Initialize to an appropriate value
            Post p = target.GetPost(postKey);
            Assert.AreEqual(expected, p);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubforum
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void GetSubforumServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            Subforum expected = null; // TODO: Initialize to an appropriate value
            Subforum actual;
            actual = target.GetSubforum(subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        ///// <summary>
        /////A test for GetSubforumOfPost
        /////</summary>
        //// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        //// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        //// whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        //[DeploymentItem("ForumServer.dll")]
        //public void GetSubforumOfPostServerTests()
        //{
        //    DataManager_Accessor target = new DataManager_Accessor(); // TODO: Initialize to an appropriate value
        //    Postkey postKey = null; // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    actual = target.GetSubforumOfPost(postKey);
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        /// <summary>
        ///A test for GetUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void GetUserServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            User expected = null; // TODO: Initialize to an appropriate value
            User actual;
            actual = target.GetUser(username);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for SetModerators
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void SetModeratorsServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            List<string> moderators = null;
            actual = target.SetModerators(subforum, moderators);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for UpdateUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void UpdateUserServerTests()
        {
            DataManager target = new DataManager(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.UpdateUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
