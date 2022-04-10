using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Edge;

namespace Task70
{
    [TestFixture("Edge", "latest", "Windows 10", "Edge Windows 10")]
    [TestFixture("Chrome", "latest", "Linux", "Chrome Linux")]
    [TestFixture("Firefox", "latest", "Windows 8.1", "Firefox Windows 8.1")]
    public class YandexLoginTests
    {
        private static readonly By USERNAME_FIRST_LETTER = By.ClassName("username__first-letter");

        private IWebDriver Driver;

        private string _browser;
        private string _version;
        private string _platform;
        private string _name;
        private string _sauceUserName;
        private string _sauceAccessKey;

        public YandexLoginTests(string browser, string version, string platform, string name)
        {
            _browser = browser;
            _version = version;
            _platform = platform;
            _name = name;
            _sauceUserName = Environment.GetEnvironmentVariable("SAUCE_USERNAME", EnvironmentVariableTarget.User);
            _sauceAccessKey = Environment.GetEnvironmentVariable("SAUCE_ACCESS_KEY", EnvironmentVariableTarget.User);
        }

        [SetUp]
        public void setUp()
        {
            var browserOptions = GetBrowserOptions();
            browserOptions.PlatformName = _platform;
            browserOptions.BrowserVersion = _version;

            var sauceOptions = new Dictionary<string, object>();
            sauceOptions.Add("name", _name);
            sauceOptions.Add("username", _sauceUserName);
            sauceOptions.Add("accessKey", _sauceAccessKey);
            sauceOptions.Add("extendedDebugging", true);
            sauceOptions.Add("capturePerformance", true);

            browserOptions.AddAdditionalCapability("sauce:options", sauceOptions, true);
            Driver = new RemoteWebDriver(new Uri("https://ondemand.eu-central-1.saucelabs.com/wd/hub"),
                                    browserOptions.ToCapabilities(), TimeSpan.FromSeconds(600));
            Thread.Sleep(1000);
        }
        public dynamic GetBrowserOptions()
        {
            if (_browser == "Chrome")
                return new ChromeOptions();
            if (_browser == "Firefox")
                return new FirefoxOptions();
            if (_browser == "Edge")
                return new EdgeOptions();

            return new ChromeOptions();
        }

        [Test]
        [TestCase("alinutus@yandex.ru", "coherent1")]

        public void LogOut(string name, string password)
        {
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

            loginPage.Logout();
        }

        [Test]
        [TestCase("autolilia", "coherent1")]
        public void LogIn(string name, string password)
        {
            var loginPage = new LoginPage(Driver);

            loginPage.LoginName(name);

            loginPage.LoginPassword(password);

            Driver.Manage().Timeouts().ImplicitWait = System.TimeSpan.FromSeconds(15);

            var accountName = "A";
            var loginElement = Driver.FindElement(USERNAME_FIRST_LETTER).Text;
            var message = "Account name has wrong value";
            Assert.AreEqual(accountName, loginElement, message);
        }   
    }
}