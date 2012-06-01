using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "localhost:52644/NetworkLayer/forum.html";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
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


        [Test]
        public void TheSimplewebTest()
        {
            driver.Navigate().GoToUrl(baseURL);
            Thread.Sleep(1000);
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
    }
}


