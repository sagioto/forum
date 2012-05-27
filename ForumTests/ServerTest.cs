using ForumServer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;

namespace ForumTests
{


    /// <summary>
    ///This is a test class for ServerControllerTest and is intended
    ///to contain all ServerControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ServerTest
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
        public void LoginRegisterTests(){

            ServerController sc = new ServerController();
            
            Assert.AreEqual(Result.OK,sc.Login("user1", "123456"));//login before register

            Assert.AreEqual(Result.OK,sc.Register("alice", "123456"));

            //login tests
            for (int i = 0; i < 100; i++)
            {
                // try to register twice with same userName

                Assert.AreEqual(Result.OK,sc.Register("alice" + i, "123456"));
                Assert.AreEqual(Result.ENTRY_EXISTS,sc.Register("alice" + i, "123456"));

                //failed: try to login twice with same userName return true , should return false.
                Assert.AreEqual(Result.OK,sc.Login("alice" + i, "123456"));
                //Assert.IsFalse(sc.Login("alice" + i, "123456"));

                Assert.AreEqual(Result.USER_NOT_FOUND,sc.Login("bob" + i, "123456"));//try to login with bad unknown user
                Assert.AreEqual(Result.SECURITY_ERROR,sc.Login("alice" + i, "123456" + i));//try to login with bad password
            }
        
         }

        [TestMethod]
        public void LogoutTests()
        {
            // logout tests
            ServerController sc2 = new ServerController();
            Assert.AreEqual(Result.USER_NOT_FOUND,sc2.Logout("testUser"));//try to logout without any register and login

            sc2.Register("alice", "123456");
            Assert.AreEqual(Result.USER_NOT_FOUND,sc2.Logout("alice"));//try to logout without login

            sc2.Login("alice", "123456");
            Assert.AreEqual(Result.OK, sc2.Logout("alice"));//try to logout after login
        }

        [TestMethod]
        public void postTest()
        {
            ServerController sc = new ServerController();
            sc.Register("test1", "123456");
            sc.Login("test1", "123456");

            //   Assert.IsInstanceOfType(cc.GetSubforumsList(), typeof(String[]));

            String[] SubForumArray = sc.GetSubforumsList();

            for (int i = 0; i < SubForumArray.Length; i++)
            {
                Post NewPost = new Post(new Postkey("test1", DateTime.Now), "title" + i, "body" + i,null, SubForumArray[i]);
                Assert.AreEqual(Result.OK, sc.Post(SubForumArray[i], NewPost)); //post message in all sub forums
            }
            Console.WriteLine("end");


            Post TmpPost = new Post(new Postkey("test1", DateTime.Now), "title", "body", null, "Woman");
            Assert.AreEqual(Result.ILLEGAL_POST,sc.Post("XXXYYYZZZ", TmpPost));//post message in sub forum that isn"t exists

        }

    }
}
