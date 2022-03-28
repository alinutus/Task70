using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace Task70
{
    public class YandexScreenshot
    {
        protected IWebDriver _driver;

        public YandexScreenshot(IWebDriver driver)
        {
            _driver = driver;
        }
        public void TakeScreenshot()
        {
            try
            {
                var screenshotDriver = _driver as ITakesScreenshot;

                Screenshot screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
                screenshot.SaveAsFile(@"D:\Screenshots\SeleniumTestingScreenshot.jpg", ScreenshotImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                throw;
            }
        }
    }
}
