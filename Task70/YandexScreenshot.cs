using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;

namespace Task70
{
    public class YandexScreenshot
    {
        protected IWebDriver _driver;

        public YandexScreenshot(IWebDriver driver)
        {
            _driver = driver;
        }

        public object Helper { get; private set; }

        public void TakeScreenshot()
        {
            try
            {
                var screenshotDriver = _driver as ITakesScreenshot;

                 string startupPath = Environment.CurrentDirectory;
                Screenshot screenshot = ((ITakesScreenshot) _driver).GetScreenshot();
                string cas = DateTime.Now.ToString("dd_MM_yy_HH_mm_ss");
                screenshot.SaveAsFile(cas + ".jpg", ScreenshotImageFormat.Jpeg);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
    }
}
