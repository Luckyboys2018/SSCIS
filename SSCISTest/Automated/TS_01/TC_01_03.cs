using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Automated.TS_01
{
    [TestClass]
    public class TC_01_03
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [TestInitialize]
        public void SetupTest()
        {
            driver = new ChromeDriver();
            baseURL = "https://www.katalon.com/";
            verificationErrors = new StringBuilder();
        }

        [TestCleanup]
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

        [TestMethod]
        public void TC0103()
        {
            driver.Navigate().GoToUrl("http://ourea98.zcu.cz/");
            driver.FindElement(By.LinkText("Přihlásit se")).Click();
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("ADMIN_TST_00");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.LinkText("Žádosti tutorů")).Click();
            driver.FindElement(By.LinkText("Detail")).Click();
            driver.FindElement(By.LinkText("Zamítnout")).Click();
            Assert.AreEqual("Přihlášky tutorů", driver.FindElement(By.XPath("//h1")).Text);
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

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
