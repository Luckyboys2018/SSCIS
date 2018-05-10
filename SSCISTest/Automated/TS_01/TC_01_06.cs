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
    public class TC_01_06
    {
        private OpenQA.Selenium.IWebDriver driver;
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
        public void TheTC0106Test()
        {
            driver.FindElement(By.LinkText("Přihlásit se")).Click();
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("USER_TST_02");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.LinkText("Vypsat lekci")).Click();
            driver.FindElement(By.Id("Date")).Click();
            driver.FindElement(By.Id("Date")).Clear();
            driver.FindElement(By.Id("Date")).SendKeys("0009-01-01");
            driver.FindElement(By.Id("Date")).Clear();
            driver.FindElement(By.Id("Date")).SendKeys("0099-01-01");
            driver.FindElement(By.Id("Date")).Clear();
            driver.FindElement(By.Id("Date")).SendKeys("0999-01-01");
            driver.FindElement(By.Id("Date")).Clear();
            driver.FindElement(By.Id("Date")).SendKeys("9999-01-01");
            driver.FindElement(By.Id("TimeFrom")).Clear();
            driver.FindElement(By.Id("TimeFrom")).SendKeys("10:00");
            driver.FindElement(By.Id("TimeTo")).Clear();
            driver.FindElement(By.Id("TimeTo")).SendKeys("11:00");
            driver.FindElement(By.Id("SubjectID")).Click();
            driver.FindElement(By.Id("SubjectID")).Click();
            driver.FindElement(By.XPath("//input[@value='Vytvořit událost']")).Click();
            Assert.AreEqual("Moje lekce", driver.FindElement(By.XPath("//h2")).Text);
            Assert.AreEqual("01.01.9999 10:00", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td")).Text);
            Assert.AreEqual("01.01.9999 11:00", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td[2]")).Text);
            Assert.AreEqual("MAT", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td[4]")).Text);
            Assert.AreEqual("USER_TST_02", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td[5]")).Text);
            Assert.AreEqual("Ne", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td[6]")).Text);
            Assert.AreEqual("Ne", driver.FindElement(By.XPath("//table[@id='tutor-timetable']/tbody/tr[2]/td[7]")).Text);
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
