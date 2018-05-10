using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SSCIS.Class;
using SSCIS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Automated._1_TS_01
{
    [TestClass]
    public class TC_01_02
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
        public void TC0102()
        {
            driver.Navigate().GoToUrl("http://ourea98.zcu.cz/");
            driver.FindElement(By.LinkText("Přihlásit se")).Click();
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("USER_TST_01");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.LinkText("Chci pomáhat")).Click();
            driver.FindElement(By.Id("SubjectID")).Click();
            new SelectElement(driver.FindElement(By.Id("SubjectID"))).SelectByText("KMA/MA1");
            driver.FindElement(By.Id("SubjectID")).Click();
            driver.FindElement(By.Id("Degree")).Click();
            new SelectElement(driver.FindElement(By.Id("Degree"))).SelectByText("1");
            driver.FindElement(By.Id("Degree")).Click();
            driver.FindElement(By.LinkText("Přidat předmět")).Click();
            driver.FindElement(By.XPath("(//select[@id='SubjectID'])[2]")).Click();
            new SelectElement(driver.FindElement(By.XPath("(//select[@id='SubjectID'])[2]"))).SelectByText("KMA/MA2");
            driver.FindElement(By.XPath("(//select[@id='SubjectID'])[2]")).Click();
            driver.FindElement(By.XPath("(//select[@id='Degree'])[2]")).Click();
            new SelectElement(driver.FindElement(By.XPath("(//select[@id='Degree'])[2]"))).SelectByText("1");
            driver.FindElement(By.XPath("(//select[@id='Degree'])[2]")).Click();
            driver.FindElement(By.Id("submit-btn")).Click();
            Assert.AreEqual("Přihláška odeslána", driver.FindElement(By.XPath("//h2")).Text);
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
