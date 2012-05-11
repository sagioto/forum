﻿using ForumServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ServerTests
{
    
    
    /// <summary>
    ///This is a test class for ServerControllerTest and is intended
    ///to contain all ServerControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServerControllerTest
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
        ///A test for ServerController Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ServerControllerConstructorTest()
        {
            ServerController target = new ServerController();
            Assert.Inconclusive("TODO: Implement code to verify target");
        }

        /// <summary>
        ///A test for AddModerator
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void AddModeratorTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string usernameToAdd = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddModerator(adminUsername, adminPassword, usernameToAdd, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for AddSubforum
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void AddSubforumTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string subforumName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddSubforum(adminUsername, adminPassword, subforumName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for CheckIfModerator
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        [DeploymentItem("ForumServer.dll")]
        public void CheckIfModeratorTest()
        {
            ServerController_Accessor target = new ServerController_Accessor(); // TODO: Initialize to an appropriate value
            string usernameToRemove = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.CheckIfModerator(usernameToRemove);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for EditPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void EditPostTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            Postkey currPost = null; // TODO: Initialize to an appropriate value
            Post post = null; // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.EditPost(currPost, post, username, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Enter
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void EnterTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            Subforum[] expected = null; // TODO: Initialize to an appropriate value
            Subforum[] actual;
            actual = target.Enter();
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
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void GetPostTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            Postkey key = null; // TODO: Initialize to an appropriate value
            Post expected = null; // TODO: Initialize to an appropriate value
            Post actual;
            actual = target.GetPost(key);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubForum
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void GetSubForumTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            Subforum expected = null; // TODO: Initialize to an appropriate value
            Subforum actual;
            actual = target.GetSubForum(subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Login
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void LoginTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Login(username, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Logout
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void LogoutTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Logout(username);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Post
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void PostTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            Post post = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Post(subforum, post);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Register
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void RegisterTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Register(username, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RemoveModerator
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void RemoveModeratorTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string usernameToRemove = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RemoveModerator(adminUsername, adminPassword, usernameToRemove, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RemovePost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void RemovePostTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            Postkey originalPostKey = null; // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string password = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RemovePost(originalPostKey, username, password);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for RemoveSubforum
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void RemoveSubforumTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string subforumName = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RemoveSubforum(adminUsername, adminPassword, subforumName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReplaceAdmin
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ReplaceAdminTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string oldAdminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string oldAdminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string newAdminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string newAdminPassword = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ReplaceAdmin(oldAdminUsername, oldAdminPassword, newAdminUsername, newAdminPassword);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReplaceModerator
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ReplaceModeratorTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string usernameToAdd = string.Empty; // TODO: Initialize to an appropriate value
            string usernameToRemove = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ReplaceModerator(adminUsername, adminPassword, usernameToAdd, usernameToRemove, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Reply
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ReplyTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            Postkey currPost = null; // TODO: Initialize to an appropriate value
            Post post = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.Reply(currPost, post);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReportSubForumTotalPosts
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ReportSubForumTotalPostsTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string subforumName = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.ReportSubForumTotalPosts(adminUsername, adminPassword, subforumName);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ReportUserTotalPosts
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ReportUserTotalPostsTest()
        {
            ServerController target = new ServerController(); // TODO: Initialize to an appropriate value
            string adminUsername = string.Empty; // TODO: Initialize to an appropriate value
            string adminPassword = string.Empty; // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            int expected = 0; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.ReportUserTotalPosts(adminUsername, adminPassword, username);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}