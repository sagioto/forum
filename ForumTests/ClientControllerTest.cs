using ForumClientCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ForumTests
{
    
    
    /// <summary>
    ///This is a test class for ClientControllerTest and is intended
    ///to contain all ClientControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ClientControllerTest
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
        ///A test for AddMessage
        ///</summary>
        [TestMethod()]
        public void AddMessageTest()
        {
            ClientController target = new ClientController(); // TODO: Initialize to an appropriate value
            string s = "hello world";
            target.AddMessage(s);
           // Assert.Inconclusive("A method that does not return a value cannot be verified.");
        }

        /// <summary>
        ///A test for GetSubforumsList
        ///</summary>
        [TestMethod()]
        public void GetSubforumsListTest()
        {
            ClientController target = new ClientController(); // TODO: Initialize to an appropriate value
            string expected = string.Empty; // TODO: Initialize to an appropriate value
            string actual;
            actual = target.GetSubforumsList();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
