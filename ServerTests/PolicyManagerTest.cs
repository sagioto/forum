//using ForumServer.Policy;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System;
//using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
//using ForumServer.DataLayer;
//using ForumServer.DataTypes;
//using ForumUtils.SharedDataTypes;
//using System.Threading;
//using ForumShared.SharedDataTypes;

//namespace ServerTests
//{
    
    
//    /// <summary>
//    ///This is a test class for PolicyManagerTest and is intended
//    ///to contain all PolicyManagerTest Unit Tests
//    ///</summary>
//    [TestClass()]
//    public class PolicyManagerTest
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
//        ///A test for PolicyManager Constructor
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void PolicyManagerConstructorTest()
//        {
//            PolicyManager target = new PolicyManager(new DataManager());
//        }


//        /// <summary>
//        ///A test for AddModerator
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AddModeratorFalseTest()
//        {
//            DataManager dataManager = new DataManager();
//            PolicyManager target = new PolicyManager(dataManager);
//            string subName = "subforum";
//            dataManager.AddSubforum(new Subforum(subName));
//            User user = new User("some", "some");
//            dataManager.AddUser(user);
//            for (int i = 0; i < 4; i++)
//            {
//                dataManager.AddPost(new Post(new Postkey("some", DateTime.Now), "", "", null, subName), subName);
//            }
//            bool actual = target.AddModerator(user.Username, subName);
//            Assert.AreEqual(false, actual);
//        }

//        /// <summary>
//        ///A test for AddModerator
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void AddModeratorTest()
//        {
//            DataManager dataManager = new DataManager();
//            PolicyManager target = new PolicyManager(dataManager);
//            string subName = "subforum";
//            dataManager.AddSubforum(new Subforum(subName));
//            User user = new User("some", "some");
//            dataManager.AddUser(user);
//            for (int i = 0; i < 5; i++)
//            {
//                dataManager.AddPost(new Post(new Postkey("some", DateTime.Now), "", "" , null, subName), subName);
//                Thread.Sleep(1);
//            }
//            bool actual = target.AddModerator(user.Username, subName);
//            Assert.AreEqual(true, actual);
//        }

//        /// <summary>
//        ///A test for ChangeModerator
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void ChangeModeratorTest()
//        {
//            DataManager dataManager = null; // TODO: Initialize to an appropriate value
//            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
//            string oldUsername = "some1"; // TODO: Initialize to an appropriate value
//            string newUsername = "some2"; // TODO: Initialize to an appropriate value
//            string subforum = "some"; // TODO: Initialize to an appropriate value
//            bool expected = true; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.ChangeModerator(oldUsername, newUsername, subforum);
//            Assert.AreEqual(expected, actual);
//        }

//        /// <summary>
//        ///A test for IsAuthorizedToEdit
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void IsAuthorizedToEditTest()
//        {
//            DataManager dataManager = null; // TODO: Initialize to an appropriate value
//            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
//            Postkey originalPostKey = null; // TODO: Initialize to an appropriate value
//            string username = string.Empty; // TODO: Initialize to an appropriate value
//            bool expected = true; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.IsAuthorizedToEdit(originalPostKey, username);
//            Assert.AreEqual(expected, actual);
//      }

//        /// <summary>
//        ///A test for RemoveModerator
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void RemoveModeratorTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            string subforum = "sub"; // TODO: Initialize to an appropriate value
//            User user = new User(username, username);
//            dataManager.AddUser(user);
//            Subforum sub = new Subforum(subforum);
//            sub.ModeratorsList.Add(user.Username);
//            dataManager.AddSubforum(sub);
//            bool expected = true; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.RemoveModerator(username, subforum);
//            Assert.AreEqual(expected, actual);
//         }

//        /// <summary>
//        ///A test for RemoveModerator
//        ///</summary>
//        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
//        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
//        // whether you are testing a page, web service, or a WCF service.
//        [TestMethod()]
//        public void RemoveModeratorFalseTest()
//        {
//            DataManager dataManager = new DataManager(); // TODO: Initialize to an appropriate value
//            PolicyManager target = new PolicyManager(dataManager); // TODO: Initialize to an appropriate value
//            string username = "some"; // TODO: Initialize to an appropriate value
//            string subforum = "sub"; // TODO: Initialize to an appropriate value
//            User user = new User(username, username);
//            dataManager.AddUser(user);
//            Subforum sub = new Subforum(subforum);
//            sub.ModeratorsList.Add(user.Username);
//            dataManager.AddSubforum(sub);
//            dataManager.AddPost(new Post(new Postkey(username, DateTime.Now), "sar","sar",null, subforum ), subforum);
//            bool expected = false; // TODO: Initialize to an appropriate value
//            bool actual;
//            actual = target.RemoveModerator(username, subforum);
//            Assert.AreEqual(expected, actual);
//        }
//    }
//}
