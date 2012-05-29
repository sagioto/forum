using ForumServer.DataLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataTypes;
using System.Collections.Generic;
using ForumUtils.SharedDataTypes;
using ForumShared.SharedDataTypes;

namespace ServerTests
{
    
    
    /// <summary>
    ///This is a test class for IDataManagerServerTests and is intended
    ///to contain all IDataManagerServerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class IDataManagerServerTests
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


        internal virtual IDataManager CreateIDataManager()
        {
            // TODO: Instantiate an appropriate concrete class.
            IDataManager target = null;
            return target;
        }

        /// <summary>
        ///A test for AddPost
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void AddPostServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
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
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void AddReplyServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
            Post reply = null; // TODO: Initialize to an appropriate value
            Postkey originalPost = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddReply(reply, originalPost);
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
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void EditPostServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
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
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void GetModeratorsServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            List<string> expected = null; // TODO: Initialize to an appropriate value
            List<string> actual;
            actual = target.GetModerators(subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetSubforum
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void GetSubforumServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            Subforum expected = null; // TODO: Initialize to an appropriate value
            Subforum actual;
            actual = target.GetSubforum(subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for GetUser
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void GetUserServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
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
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void SetModeratorsServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            List<string> moderators = new List<string>();
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
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void UpdateUserServerTests()
        {
            IDataManager target = CreateIDataManager(); // TODO: Initialize to an appropriate value
            User user = null; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.UpdateUser(user);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
