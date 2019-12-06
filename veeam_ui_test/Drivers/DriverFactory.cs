using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace veeam_ui_test.Drivers
{
    public static class DriverFactory
    {
        public static IWebDriver GetWebDriver(WebBrowser type)
        {
            switch (type)
            {
                case WebBrowser.Chrome:
                    // options & capabilities
                    var driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                    return driver;
                default:
                    return new ChromeDriver();
            }
        }
    }
}

public enum WebBrowser
{
    Chrome
}