using ForumServer.DataTypes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;

namespace ServerTests
{
    
    
    /// <summary>
    ///This is a test class for PostkeyServerTests and is intended
    ///to contain all PostkeyServerTests Unit Tests
    ///</summary>
    [TestClass()]
    public class PostkeyServerTests
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
        ///A test for Postkey Constructor
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void PostkeyConstructorServerTests()
        {
            string username = "admin";
            DateTime time = DateTime.Now;
            Postkey target = new Postkey(username, time);
            Assert.IsNotNull(target);
        }

        /// <summary>
        ///A test for CompareTo
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        [TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        public void CompareToServerTests()
        {
            string username = "dori";
            DateTime time = DateTime.Now;
            Postkey target = new Postkey(username, time); // TODO: Initialize to an appropriate value
            Postkey pk = new Postkey("dor", time);
            int expected = 1; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.CompareTo(pk);
            Assert.AreEqual(expected, actual);
        }

        // The following are not necessary to test, ignore:


        ///// <summary>
        /////A test for Time
        /////</summary>
        //// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        //// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        //// whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        ////[HostType("ASP.NET")]
        ////[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        ////[UrlToTest("http://localhost:52644/")]
        //public void TimeServerTests()
        //{
        //    string username = string.Empty; // TODO: Initialize to an appropriate value
        //    DateTime time = new DateTime(); // TODO: Initialize to an appropriate value
        //    Postkey target = new Postkey(username, time); // TODO: Initialize to an appropriate value
        //    DateTime expected = new DateTime(); // TODO: Initialize to an appropriate value
        //    DateTime actual;
        //    target.Time = expected;
        //    actual = target.Time;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}

        ///// <summary>
        /////A test for Username
        /////</summary>
        //// TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        //// http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        //// whether you are testing a page, web service, or a WCF service.
        //[TestMethod()]
        //[HostType("ASP.NET")]
        //[AspNetDevelopmentServerHost("C:\\Users\\dleitman\\Documents\\Git\\forum\\ForumServer", "/")]
        //[UrlToTest("http://localhost:52644/")]
        //public void UsernameServerTests()
        //{
        //    string username = string.Empty; // TODO: Initialize to an appropriate value
        //    DateTime time = new DateTime(); // TODO: Initialize to an appropriate value
        //    Postkey target = new Postkey(username, time); // TODO: Initialize to an appropriate value
        //    string expected = string.Empty; // TODO: Initialize to an appropriate value
        //    string actual;
        //    target.Username = expected;
        //    actual = target.Username;
        //    Assert.AreEqual(expected, actual);
        //    Assert.Inconclusive("Verify the correctness of this test method.");
        //}
    }
}
