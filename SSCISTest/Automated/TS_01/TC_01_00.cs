using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSCISTest.Automated._1_TS_01
{
    [TestClass]
    public class TC_01_00
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
        public void TC0100()
        {
            driver.Navigate().GoToUrl("http://ourea98.zcu.cz/");
            driver.FindElement(By.LinkText("Přihlásit se")).Click();
            driver.FindElement(By.Id("Login")).Click();
            driver.FindElement(By.Id("Login")).Clear();
            driver.FindElement(By.Id("Login")).SendKeys("USER_TST_00");
            driver.FindElement(By.XPath("//input[@value='Login']")).Click();
            driver.FindElement(By.LinkText("USER_TST_00")).Click();
            Assert.AreEqual("Login", driver.FindElement(By.XPath("//dt")).Text);
            Assert.AreEqual("Jméno", driver.FindElement(By.XPath("//dt[2]")).Text);
            Assert.AreEqual("Příjmení", driver.FindElement(By.XPath("//dt[3]")).Text);
            Assert.AreEqual("Vytvořeno", driver.FindElement(By.XPath("//dt[4]")).Text);
            Assert.AreEqual("Studentské číslo", driver.FindElement(By.XPath("//dt[5]")).Text);
            Assert.AreEqual("Role", driver.FindElement(By.XPath("//dt[6]")).Text);
            Assert.AreEqual("USER_TST_00", driver.FindElement(By.XPath("//dd")).Text);
            Assert.AreEqual("Jan", driver.FindElement(By.XPath("//dd[2]")).Text);
            Assert.AreEqual("Novák", driver.FindElement(By.XPath("//dd[3]")).Text);
            Assert.AreEqual("01.01.2019 0:00:00", driver.FindElement(By.XPath("//dd[4]")).Text);
            Assert.AreEqual("A16B0001P", driver.FindElement(By.XPath("//dd[5]")).Text);
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
