using ForumServer.Policy;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumServer.DataLayer;
using ForumServer.DataTypes;
using ForumUtils.SharedDataTypes;

namespace ServerTests
{
    
    
    /// <summary>
    ///This is a test class for PolicyManagerTest and is intended
    ///to contain all PolicyManagerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class PolicyManagerTest
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
        ///A test for PolicyManager Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void PolicyManagerConstructorTest()
        {
            DataManager dataManager = null; // TODO: Initialize to an appropriate value
            PolicyManager target = new PolicyManager(dataManager);
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
            DataManager dataManager = null; // TODO: Initialize to an appropriate value
            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.AddModerator(username, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for ChangeModerator
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void ChangeModeratorTest()
        {
            DataManager dataManager = null; // TODO: Initialize to an appropriate value
            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
            string oldUsername = string.Empty; // TODO: Initialize to an appropriate value
            string newUsername = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.ChangeModerator(oldUsername, newUsername, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for IsAuthorizedToEdit
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("C:\\workspace\\forum\\ForumServer", "/")]
        [UrlToTest("http://localhost:52644/")]
        public void IsAuthorizedToEditTest()
        {
            DataManager dataManager = null; // TODO: Initialize to an appropriate value
            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
            Postkey originalPostKey = null; // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.IsAuthorizedToEdit(originalPostKey, username);
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
            DataManager dataManager = null; // TODO: Initialize to an appropriate value
            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
            string username = string.Empty; // TODO: Initialize to an appropriate value
            string subforum = string.Empty; // TODO: Initialize to an appropriate value
            bool expected = false; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.RemoveModerator(username, subforum);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
