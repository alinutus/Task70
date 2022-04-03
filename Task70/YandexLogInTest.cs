using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Task70
{ 
    public class YandexLoginTests
    {
        private static readonly By USERNAME_FIRST_LETTER = By.ClassName("username__first-letter");

        private IWebDriver Driver;

        [Test]
        [TestCase("alinutus@yandex.ru", "coherent1")]
        public void LogOut(string name, string password)
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(15);

            var loginPage = new LoginPage(Driver);

            loginPage.LoginName(name);

            loginPage.LoginPassword(password);

            var wait = new WebDriverWait(Driver, System.TimeSpan.FromSeconds(15));
            var element = wait.Until(condition =>
            {
                try
                {
                    var elementToBeDisplayed = Driver.FindElement(USERNAME_FIRST_LETTER);
                    return elementToBeDisplayed.Text == "A" || elementToBeDisplayed.Text == "a";
                }
                catch (NoSuchElementException)
                {
                    return false;
                }
            }
            );

            YandexScreenshot screenshot = new YandexScreenshot(Driver);
            screenshot.TakeScreenshot();

            loginPage.Logout();
        }

        [Test]
        [TestCase("autolilia", "coherent1")]
        public void LogIn(string name, string password)
        {
            Driver = new ChromeDriver();
            Driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(15);

            var loginPage = new LoginPage(Driver);

            loginPage.LoginName(name);

            loginPage.LoginPassword(password);

            var accountName = "A";
            var loginElement = Driver.FindElement(USERNAME_FIRST_LETTER).Text;
            var message = "Account name has wrong value";
            Assert.AreEqual(accountName, loginElement, message);
        }
    }
}