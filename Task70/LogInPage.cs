using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Task70
{
    public class LoginPage
    {
        private static readonly string URL = "https://yandex.ru";
        private static readonly By LOGIN_BUTTON_FIRST = By.XPath("//a[@data-statlog='notifications.mail.logout.enter']");
        private static readonly By LOGIN_NAME_TEXT_FIELD = By.XPath("//span/input[@type='text']");
        private static readonly By LOGIN_BUTTON_SECOND = By.Id("passp:sign-in");
        private static readonly By PASSWORD_TEXT_FIELD = By.Name("passwd");
        private static readonly By AVATAR_ELEMENT = By.XPath("//span[@class='avatar__image  avatar__image-server0']");
        private static readonly By LOGOUT = By.XPath("//a[@data-statlog='mail.login.usermenu.exit']");


        protected IWebDriver _driver;

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            driver.Url = URL;
        }

        public void LoginName(string name)
        {
            IWebElement loginButtonFirst = _driver.FindElement(LOGIN_BUTTON_FIRST);
            loginButtonFirst.Click();

            IWebElement loginNameTextField = _driver.FindElement(LOGIN_NAME_TEXT_FIELD);
            loginNameTextField.SendKeys(name);

            IWebElement loginButtonSecond = _driver.FindElement(LOGIN_BUTTON_SECOND);
            loginButtonSecond.Click();

            Thread.Sleep(1000); /* This is fluent waiter */
        }

        public LoginPage LoginPassword(string password)
        {
            IWebElement passwordTextField = _driver.FindElement(PASSWORD_TEXT_FIELD);
            passwordTextField.SendKeys(password);

            IWebElement loginButtonSecond = _driver.FindElement(LOGIN_BUTTON_SECOND);
            loginButtonSecond.Click();

            Thread.Sleep(1000);

            return new LoginPage(_driver);

            Thread.Sleep(1000);
        }

        public void Logout()
        {
            IWebElement avatarElement = _driver.FindElement(AVATAR_ELEMENT);
            avatarElement.Click();

            IWebElement logoutElement = _driver.FindElement(LOGOUT);
            logoutElement.Click();

            Thread.Sleep(1000); 
        }
    }
}
