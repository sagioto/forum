using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ForumServer;
using ForumShared.ForumAPI;
using ForumShared.SharedDataTypes;

namespace PersistencyTests
{
    [TestFixture]
    public class PersistencyTests
    {

        IForumService sc;

        [SetUp]
        public void SetupTest()
        {
            Console.WriteLine("Initializing server...");
            sc = new ServerNetworkAdaptor();
            Console.WriteLine("Done.");
        }

        [TearDown]
        public void TeardownTest()
        {

        }

        /// <summary>
        /// Create a new user. Reload the server, see that the user still exists.
        /// </summary>
        [Test]
        public void UserPersistencyTest()
        {
            sc.Register("persTest", "persTest");

            ReloadServer();

            Result r = sc.Login("persTest", "persTest");
            Assert.AreEqual(Result.OK, r, "Tried to login with the user after DB reload, but got " + r.ToString());
        }

        /// <summary>
        /// Posting and replying and reloading the server, check that the post remains.
        /// </summary>
        [Test]
        public void PostingPersistencyTest()
        {
            sc.Login("admin", "admin");
            string[] subforums = sc.GetSubforumsList();
            Assert.True(subforums.Length > 0, "No subforums in DB, can't run the test!");
            int totalNumOfPosts = 0;

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

            ReloadServer();

            int newNumOfPosts = 0;

            foreach (string s in subforums)
            {
                newNumOfPosts += sc.ReportSubForumTotalPosts("admin", "admin", s);
            }

            Assert.True(newNumOfPosts - totalNumOfPosts == 2, "Wrong number of posts in DB. expected: " + (totalNumOfPosts + 2) + ", but got: " + newNumOfPosts);

            //TODO fetch the posts and see they exist.
        }

        /// <summary>
        /// Adding a subforum with some posts and seeing it remains.
        /// </summary>
        [Test]
        public void AddSubforumPersistencyTest()
        {
            sc.Login("admin", "admin");

            Assert.True(sc.AddSubforum("Persistency Test Forum", "admin", "admin") == Result.OK);

            ReloadServer();

            string[] subforums = sc.GetSubforumsList();
            Assert.True(subforums.Contains<string>("Persistency Test Forum"), "The subforum was not found after reloading DB!");

            Assert.AreEqual(Result.OK, sc.RemoveSubforum("admin", "admin", "Persistency Test Forum"), "Could not remove the subforum");

            subforums = sc.GetSubforumsList();
            Assert.False(subforums.Contains<string>("Persistency Test Forum"), "The sub forum was not removed from the DB");

            ReloadServer();

            subforums = sc.GetSubforumsList();
            Assert.False(subforums.Contains<string>("Persistency Test Forum"), "The subforum exists in DB after reload!!");
        }


        private void ReloadServer()
        {
            //TODO
            throw new NotImplementedException();
        }
    }
}
