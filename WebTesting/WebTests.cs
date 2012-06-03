using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using ForumClientCore;
using ForumShared.SharedDataTypes;
using ForumShared.ForumAPI;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebTesting
{
    [TestFixture]
    public class WebTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private ClientController c;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "localhost:52644/NetworkLayer/forum.html";
            verificationErrors = new StringBuilder();
            c = new ClientController();
            driver.Navigate().GoToUrl(baseURL);
            Thread.Sleep(2000);
        }

        [TearDown]
        public void TeardownTest()
        {
            Thread.Sleep(2000);
            try
            {
                driver.Quit();

            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());

        }


        /*       [Test]
               public void TheSimplewebTest()
               {
                   driver.Navigate().GoToUrl(baseURL);
                   Thread.Sleep(3000);
                   string cars = driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[1]/td")).Text;
                   Assert.True(cars.Equals("Cars"));
                   driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[1]/td")).Click();
               }
               private bool IsElementPresent(By by)
               {
                   try
                   {
                       driver.FindElement(by);
                       return true;
                   }
                   catch (NoSuchElementException)
                   {
                       return false;
                   }
               }
               */




        public void addNewPostToBrowser(String subForumName, String title, String body)
        {
            driver.FindElement(By.XPath("//*[@id='subforumpostbutton']")).Click(); //post button
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//*[@id='titleToPost" + subForumName + "']")).SendKeys(title); //post title
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//*[@id='bodyToPost" + subForumName + "']")).SendKeys(body); //post body
            Thread.Sleep(300);
            driver.FindElement(By.XPath("//*[@id='subforumpostbutton']")).Click(); //submit button
            Thread.Sleep(300);
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception)
            {
            }
        }

        public void removePostFromBrowser(int index)
        {
            driver.FindElement(By.XPath("//*[@id='removeB" + index + "']")).Click(); //remove button
            Thread.Sleep(300);
        }

        public void editPostFromBrowser(int index, String newTitle, String newBody)
        {
            driver.FindElement(By.XPath("//*[@id='editB" + index + "']")).Click(); //edit button
            driver.FindElement(By.XPath("//*[@id='titleToPost" + index + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='titleToPost" + index + "']")).SendKeys(newTitle);
            driver.FindElement(By.XPath("//*[@id='bodyToPost" + index + "']")).Clear();
            driver.FindElement(By.XPath("//*[@id='bodyToPost" + index + "']")).SendKeys(newBody);
            driver.FindElement(By.XPath("//*[@id='editB" + index + "']")).Click(); //edit button
            Thread.Sleep(300);
        }

        public int findPostIndex(String subForumName, String title)
        {
            int num = driver.FindElements(By.ClassName("postButton")).Count;
            int ans = num / 3;
            return ans;
        }

        public Post findPost(String subForum, String Title, String Body)
        {
            Boolean subForumFound = false;
            Boolean titleFound = false;
            Boolean bodyFound = false;
            Post ans = null;
            String[] SubForums = c.GetSubforumsList();
            for (int i = 0; i < SubForums.Length && !subForumFound; i++)
            {
                if (SubForums[i].Equals(subForum))
                {
                    subForumFound = true;
                    Post[] p = c.GetSubforum(subForum);
                    //                  Console.WriteLine(p.Length);
                    for (int j = 0; j < p.Length && !titleFound && !bodyFound; j++)
                    {
                        //                     Console.WriteLine(p[j].Title);
                        if (p[j].Title.Equals(Title))
                        {
                            titleFound = true;
                            if (p[j].Body.Equals(Body))
                            {
                                bodyFound = true;
                                ans = p[j];
                            }
                        }
                    }
                }
            }
            return ans;
        }

        public bool findPostInController(String subForum, String Title, String Body)
        {
            Boolean subForumFound = false;
            Boolean titleFound = false;
            Boolean bodyFound = false;
            String[] SubForums = c.GetSubforumsList();
            for (int i = 0; i < SubForums.Length && !subForumFound; i++)
            {
                if (SubForums[i].Equals(subForum))
                {
                    subForumFound = true;
                    Post[] p = c.GetSubforum(subForum);
                    //                  Console.WriteLine(p.Length);
                    for (int j = 0; j < p.Length && !titleFound && !bodyFound; j++)
                    {
                        //                     Console.WriteLine(p[j].Title);
                        if (p[j].Title.Equals(Title))
                        {
                            titleFound = true;
                            if (p[j].Body.Equals(Body))
                            {
                                bodyFound = true;
                            }
                        }
                    }
                }
            }
            return (subForumFound && titleFound && bodyFound);
        }

        public void clickBack()
        {
            driver.FindElement(By.XPath("//*[@id='controlButtons']/tbody/tr/td[2]/img")).Click();
            Thread.Sleep(100);
        }

        public void clickHome()
        {
            driver.FindElement(By.XPath("//*[@id='controlButtons']/tbody/tr/td[1]/img")).Click();
            Thread.Sleep(100);
        }

        public void login(String userName, String password)
        {
            Thread.Sleep(200);
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/input[1]")).SendKeys(userName); //user name text box
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/input[2]")).SendKeys(password); //password text box
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[1]")).Click(); //login button
            Thread.Sleep(100);
        }
        public void register(String userName, String password)
        {
            Thread.Sleep(200);
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/input[1]")).SendKeys(userName); //user name text box
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/input[2]")).SendKeys(password); //password text box
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[2]")).Click(); //register button
            Thread.Sleep(100);
        }

        public void logout()
        {
            Thread.Sleep(100);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //register button
            }
            catch (Exception)
            {
                Console.WriteLine("no logout button");
            }
            Thread.Sleep(100);
        }


        [Test]
        public void registerTest1()
        {
            Boolean testCase = true;
            register("registerTest1", "registerTest1");
            Thread.Sleep(500);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[1]")); //register button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.True(testCase);
            Thread.Sleep(200);
            logout();
        }

        [Test]
        public void registerTest2()
        {
            Boolean testCase = true;
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[2]")); //register button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.True(testCase);
        }

        [Test]
        public void registerTest3()
        {
            Boolean testCase = true;
            register("registerTest2", "registerTest2");
            Thread.Sleep(200);
            logout();
            register("registerTest2", "registerTest2");
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                alert.Accept();
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.True(testCase);
        }

        [Test]
        public void registerTest4()
        {
            Boolean testCase = true;
            driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[2]")).Click();
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
        }

        [Test]
        public void registerIntegrationTest1()
        {
            register("registerIntegrationTest1", "registerIntegrationTest1");
            Result r = c.Register("registerIntegrationTest1", "registerIntegrationTest1");
            Assert.True(r != Result.OK);
            Thread.Sleep(200);
            logout();
        }

        [Test]
        public void registerIntegrationTest2()
        {
            Boolean testCase = true;
            c.Register("registerIntegrationTest2", "registerIntegrationTest2");
            register("registerIntegrationTest2", "registerIntegrationTest2");
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
            Thread.Sleep(200);
            c.Logout();
        }

        [Test]
        public void registerIntegrationTest3()
        {
            c.Register("registerIntegrationTest31", "registerIntegrationTest31");
            register("registerIntegrationTest32", "registerIntegrationTest32");
            Thread.Sleep(200);
            c.Logout();
            logout();
        }

        [Test]
        public void registerIntegrationTest4()
        {
            register("registerIntegrationTest42", "registerIntegrationTest42");
            c.Register("registerIntegrationTest41", "registerIntegrationTest41");
            Thread.Sleep(200);
            c.Logout();
            logout();
        }

        [Test]
        public void registerIntegrationTest5()
        {
            register("registerIntegrationTest5", "registerIntegrationTest5");
            Thread.Sleep(200);
            logout();
            Thread.Sleep(200);
            bool res = c.Login("registerIntegrationTest5", "registerIntegrationTest5");
            Thread.Sleep(200);
            Assert.True(res);
            c.Logout();
        }

        [Test]
        public void registerIntegrationTest6()
        {
            Boolean testCase = true;
            c.Register("registerIntegrationTest6", "registerIntegrationTest6");
            c.Logout();
            try
            {
                login("registerIntegrationTest6", "registerIntegrationTest6");
            }
            catch (Exception)
            {
                testCase = false;
            }
            Thread.Sleep(200);
            Assert.True(testCase);
            logout();

        }

        [Test]
        public void loginTest1()
        {
            logout();
            Boolean testCase = true;
            try
            {
                register("loginTest1", "loginTest1");
            }
            catch (Exception)
            {
                testCase = false;
            }
            Thread.Sleep(200);
            if (testCase)
            {
                logout();
            }
            Thread.Sleep(200);
            login("loginTest1", "loginTest1");
            Thread.Sleep(200);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[2]")).Click(); //register button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
            logout();

        }

        [Test]
        public void loginTest2()
        {
            Boolean testCase = true;
            login("loginTest2", "loginTest2");
            Thread.Sleep(200);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[1]/button[2]")).Click(); //register button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
            logout();
        }

        [Test]
        public void loginIntegrationTest1()
        {
            Boolean testCase = true;
            c.Register("loginIntegrationTest1", "loginIntegrationTest1");
            c.Logout();
            c.Login("loginIntegrationTest1", "loginIntegrationTest1");
            login("loginIntegrationTest1", "loginIntegrationTest1");
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Thread.Sleep(200);
            Assert.False(testCase);
            logout();
        }

        [Test]
        public void loginIntegrationTest2()
        {
            Boolean testCase = true;
            c.Register("loginIntegrationTest11", "loginIntegrationTest11");
            register("loginIntegrationTest12", "loginIntegrationTest12");
            c.Logout();
            logout();
            Thread.Sleep(500);

            login("loginIntegrationTest12", "loginIntegrationTest12");
            c.Login("loginIntegrationTest11", "loginIntegrationTest11");
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Thread.Sleep(200);
            Assert.True(testCase);
            c.Logout();
            logout();
        }


        [Test]
        public void logoutTest1()
        {
            Boolean testCase = true;
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);

        }

        [Test]
        public void logoutTest2()
        {
            Boolean testCase = true;
            register("logoutTest2", "logoutTest2");
            Thread.Sleep(200);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.True(testCase);
            logout();
            Thread.Sleep(200);
            testCase = true;
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
        }


        [Test]
        public void logoutTest3()
        {
            Boolean testCase = true;
            register("logoutTest3", "logoutTest3");
            Thread.Sleep(200);
            logout();
            Thread.Sleep(200);
            login("logoutTest3", "logoutTest3");
            Thread.Sleep(200);
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.True(testCase);
            logout();
            Thread.Sleep(200);
            testCase = true;
            try
            {
                driver.FindElement(By.XPath("//*[@id='forms']/form[2]/p/button")).Click(); //logout button
            }
            catch (Exception)
            {
                testCase = false;
            }
            Assert.False(testCase);
        }

        [Test]
        public void PostTest1()
        {
            register("PostTest1", "PostTest1");
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[1]/td")).Click(); //cars button
            Thread.Sleep(100);

            for (int i = 1; i < 3; i++)
            {
                addNewPostToBrowser("Cars", "Title for postTest1 " + i, "postTest1 will pass " + i + " !!!!!");
                Thread.Sleep(500);
            }
            Thread.Sleep(500);
            logout();
        }

        [Test]
        public void PostIntegrationTest1()
        {
            register("PostTest2", "PostTest2");
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[1]/td")).Click(); //cars button
            Thread.Sleep(100);

            for (int i = 1; i < 3; i++)
            {
                addNewPostToBrowser("Cars", "Title for PostTest2 " + i, "postTest2 will pass " + i + " !!!!!");
                Thread.Sleep(500);
            }
            Thread.Sleep(500);
            for (int i = 1; i < 3; i++)
            {
                Assert.True(findPostInController("Cars", "Title for PostTest2 " + i, "postTest2 will pass " + i + " !!!!!"));           //existing post
                Assert.False(findPostInController("Cars", "Title for PostTest2 " + i, "postTest2 will pass " + i + " !!!!"));       //same title, substring of an existing and matching body
                Assert.False(findPostInController("Cars", "Title for PostTest2 " + (i - 1), "postTest2 will pass " + i + " !!!!!"));//existing but non matching title and body 
                Assert.False(findPostInController("Cars", "Title for PostTest2 ", "postTest2 will pass " + i + " !!!!!"));          //substring of an existing title, existing body
            }
            Assert.True(findPostInController("Cars", "Post0 in Subforum: Cars", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Mauris ligula libero, rhoncus ac sollicitudin ut, pulvinar nec risus. Nunc laoreet hendrerit mollis. Sed at quam sit amet lacus vehicula hendrerit sit amet sed tellus. In accumsan turpis id justo scelerisque auctor. Sed ultricies, felis vitae hendrerit viverra, nunc arcu rutrum orci, eu tincidunt urna quam vel nulla. Praesent ut suscipit massa. Proin cursus egestas interdum. Maecenas at enim nibh. Nunc dictum mi non neque eleifend quis vulputate velit tincidunt. Integer fringilla sapien quis ipsum lobortis vel mollis ante rhoncus."));
            //old post, before adding
            logout();
        }

        [Test]
        public void editIntegrationTest1()
        {
            register("editIntegrationTest1", "editIntegrationTest1");
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[3]/td")).Click(); //sports button
            Thread.Sleep(100);
            addNewPostToBrowser("Sports", "editIntegrationTest1 1", "will pass 1 !!!!!");
            Thread.Sleep(1000);
            clickHome();
            logout();
            Thread.Sleep(500);
            c.Login("editIntegrationTest1", "editIntegrationTest1");
            Thread.Sleep(500);
            Post toEdit = findPost("Sports", "editIntegrationTest1 1", "will pass 1 !!!!!");
            c.EditPost(toEdit.Key, "Title After edit1", "will pass 1 !!!!!");
            c.Logout();
            Thread.Sleep(100);
            register("1", "1");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[3]/td")).Click(); //sports button
            Thread.Sleep(1000);
            logout();
        }

        [Test]
        public void editIntegrationTest2()
        {
            register("editIntegrationTest2", "editIntegrationTest2");
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[3]/td")).Click(); //sports button
            Thread.Sleep(100);
            addNewPostToBrowser("Sports", "editIntegrationTest1 2", "will pass 2 !!!!!");
            Thread.Sleep(1000);
            clickHome();
            logout();
            Thread.Sleep(500);
            c.Register("editIntegrationTest21", "editIntegrationTest21");
            Thread.Sleep(500);
            Post toEdit = findPost("Sports", "editIntegrationTest1 2", "will pass 2 !!!!!");
            c.EditPost(toEdit.Key, "Title After edit2", "will pass 1 !!!!!");
            c.Logout();
            Thread.Sleep(100);
            register("2", "2");
            Thread.Sleep(500);
            driver.FindElement(By.XPath("//*[@id='subforumsTable']/tbody/tr[3]/td")).Click(); //sports button
            Thread.Sleep(1000);
            logout();
        }










    }
}


