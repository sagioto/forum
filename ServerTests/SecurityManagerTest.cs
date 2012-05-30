//using ForumServer.Security;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
//using ForumServer.DataLayer;
//using ForumServer.DataTypes;
//using ForumUtils.SharedDataTypes;
//using ForumShared.SharedDataTypes;

//namespace ServerTests
//{
    
    
//    /// <summary>
//    ///This is a test class for SecurityManagerTest and is intended
//    ///to contain all SecurityManagerTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class SecurityManagerTest
//    {


//        private TestContext testContextInstance;

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        #region Additional test attributes
//        // 
//        //You can use the following additional attributes as you write your tests:
//        //
//        //Use ClassInitialize to run code before running the first test in the class
//        //[ClassInitialize()]
//        //public static void MyClassInitialize(TestContext testContext)
//        //{
//        //}
//        //
//        //Use ClassCleanup to run code after all tests in a class have run
//        //[ClassCleanup()]
//        //public static void MyClassCleanup()
//        //{
//        //}
//        //
//        //Use TestInitialize to run code before running each test
//        //[TestInitialize()]
//        //public void MyTestInitialize()
//        //{
//        //}
//        //
//        //Use TestCleanup to run code after each test has run
//        //[TestCleanup()]
//        //public void MyTestCleanup()
//        //{
//        //}
//        //
//        #endregion


//        /// <summary>
//        ///A test for AuthorizedLogin
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AuthorizedLoginFalseTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            SecurityManager target = new SecurityManager(dataManager); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            string password = "some"; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.AuthorizedLogin(username, password);
//            Assert.AreEqual(expected, actual);
//         }

//        /// <summary>
//        ///A test for AuthenticateAdmin
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AuthenticateAdminTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            SecurityManager target = new SecurityManager(dataManager); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            string password = "one"; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.AuthenticateAdmin(username, password);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for SecurityManager Constructor
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void SecurityManagerConstructorTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            SecurityManager target = new SecurityManager(dataManager);
//            Assert.IsNotNull(dataManager.GetAdmin());
//            Assert.AreEqual("admin", dataManager.GetAdmin().Username);
//            Assert.AreEqual("admin", dataManager.GetAdmin().Password);
//            Assert.AreEqual(AuthorizationLevel.ADMIN, dataManager.GetAdmin().Level);
            
//        }

//        /// <summary>
//        ///A test for AuthorizedLogout
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AuthorizedLogoutFalseTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = string.Empty; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.AuthorizedLogout(username);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for AuthorizedRegister
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AuthorizedRegisterTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            string password = "some"; // TODO: Initialize to an appropriate value
//            bool expected = true; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.AuthorizedRegister(username, password);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsAuthorizedToEdit
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        [ExpectedException(typeof(PostNotFoundException), "no postkey found")]
//        public void IsAuthorizedToEditFalseTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            Postkey postkey = new Postkey("", DateTime.Now); // TODO: Initialize to an appropriate value
//            string password = "some"; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual = target.IsAuthorizedToEdit(username, postkey, password);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsAuthorizedToEditSubforums
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsAuthorizedToEditSubforumsTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            SecurityManager target = new SecurityManager(dataManager);
//            bool expected = true; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.IsAuthorizedToEditSubforums(dataManager.GetAdmin().Username);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsAuthorizedToEditSubforums
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsAuthorizedToEditSubforumsFalseTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = string.Empty; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.IsAuthorizedToEditSubforums(username);
//            Assert.AreEqual(expected, actual);
 
//        }

//        /// <summary>
//        ///A test for IsAuthorizedToPost
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsAuthorizedToPostTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = string.Empty; // TODO: Initialize to an appropriate value
//            string subforum = string.Empty; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.IsAuthorizedToPost(username, subforum);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsLoggedin
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsLoggedinTest()
//        {
//            SecurityManager target = new SecurityManager(new DataManager()); // TODO: Initialize to an appropriate value
//            string username = string.Empty; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.IsLoggedin(username);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsUserLoggendIn
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsUserLoggendInTest()
//        {
//            User user = null; // TODO: Initialize to an appropriate value
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = SecurityManager_Accessor.IsUserLoggendIn(user);
//            Assert.AreEqual(expected, actual);
//        }
//    }
//}
