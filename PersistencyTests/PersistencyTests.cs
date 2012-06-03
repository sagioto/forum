using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ForumShared.ForumAPI;
using ForumShared.SharedDataTypes;
using ForumServer;
using System.Threading;

namespace PersistencyTests
{
    [TestFixture]
    public class PersistencyTests
    {

        [SetUp]
        public void SetupTest()
        {
            //Console.WriteLine("Initializing server...");
            //sc = new ServerNetworkAdaptor();
            //Console.WriteLine("Done.");
        }

        [TearDown]
        public void TeardownTest()
        {
            Thread.Sleep(1000);
        }

        /// <summary>
        /// Create a new user. Reload the server, see that the user still exists.
        /// </summary>
        [Test]
        public void UserPersistencyTest()
        {
            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                sc.Register("persTest", "persTest");
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }
              //  ReloadServer();
            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                Result r = sc.Login("persTest", "persTest");
                Assert.AreEqual(Result.OK, r, "Tried to login with the user after DB reload, but got " + r.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }
        }

        /// <summary>
        /// Posting and replying and reloading the server, check that the post remains.
        /// </summary>
        [Test]
        public void PostingPersistencyTest()
        {
            string[] subforums;
            int totalNumOfPosts = 0;
            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                sc.Login("admin", "admin");
                subforums = sc.GetSubforumsList();
                Assert.True(subforums.Length > 0, "No subforums in DB, can't run the test!");

                foreach (string s in subforums)
                {
                    totalNumOfPosts += sc.ReportSubForumTotalPosts("admin", "admin", s);
                }

                Postkey pk = new Postkey("admin", DateTime.Now);
                Post p = new Post(pk, "Persistency Test Post" + DateTime.Now, "Just a random post", null, subforums[0]);
                Assert.AreEqual(Result.OK, sc.Post(subforums[0], p), "Can't post!!! Test failed..");

                Postkey replyKey = new Postkey("admin", DateTime.Now);
                Post reply = new Post(replyKey, "Persistency Test Reply" + DateTime.Now, "Just a random reply", pk, subforums[0]);
                Assert.AreEqual(Result.OK, sc.Reply(pk, reply), "Can't reply!!! Test failed..");
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }

            //ReloadServer();

            try
            {
                int newNumOfPosts = 0;
                IForumService sc = new ServerNetworkAdaptor();
                sc = new ServerNetworkAdaptor();
                subforums = sc.GetSubforumsList();

                foreach (string s in subforums)
                {
                    newNumOfPosts += sc.ReportSubForumTotalPosts("admin", "admin", s);
                }

                Assert.True(newNumOfPosts - totalNumOfPosts == 2, "Wrong number of posts in DB. expected: " + (totalNumOfPosts + 2) + ", but got: " + newNumOfPosts);

            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }

            //TODO fetch the posts and see they exist.
        }

        /// <summary>
        /// Adding a subforum with some posts and seeing it remains.
        /// </summary>
        [Test]
        public void AddSubforumPersistencyTest()
        {
            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                sc.Login("admin", "admin");

                Assert.AreEqual(Result.OK, sc.AddSubforum("admin", "admin", "Persistency Test Forum" + DateTime.Now));
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }

            //ReloadServer();

            string[] subforums;

            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                subforums = sc.GetSubforumsList();
                Assert.True(subforums.Contains<string>("Persistency Test Forum"), "The subforum was not found after reloading DB!");

                Assert.AreEqual(Result.OK, sc.RemoveSubforum("admin", "admin", "Persistency Test Forum"), "Could not remove the subforum");

                subforums = sc.GetSubforumsList();
                Assert.False(subforums.Contains<string>("Persistency Test Forum"), "The sub forum was not removed from the DB");
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }

            //ReloadServer();
            
            try
            {
                IForumService sc = new ServerNetworkAdaptor();
                subforums = sc.GetSubforumsList();
                Assert.False(subforums.Contains<string>("Persistency Test Forum"), "The subforum exists in DB after reload!!");
            }
            catch (Exception e)
            {
                Console.WriteLine("Got exception while running test: " + e.Message);
                Console.WriteLine(e.InnerException.Message);
                Assert.Fail();
            }
        }

    }
}
