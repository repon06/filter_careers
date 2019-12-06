using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using veeam_ui_test.Drivers;
using veeam_ui_test.Pages;

namespace veeam_ui_test
{
    [TestFixture]
    public class Tests
    {
        private IWebDriver _driver;

        [TestCase( "Romania","English", 27)]
        public void SearchJobsTest(string country, string language, int expectedRomaniaEnglishJobs)
        {
            var careersPage = new VeeamCareersPage(_driver);

            //var actualVacanciesFields = careersPage.GetVacanciesElementsOnPage();
            var oldTextJobs = careersPage.GetTextFoundJobs();
            //var oldFoundJobs = careersPage.GetFoundJobs();

            careersPage.SelectCountry(country);
            careersPage.CheckLanguage(language);
            careersPage.WaitUntilChangesTextVacanciesCount(oldTextJobs);

            var foundJobs = careersPage.GetFoundJobs();


            Assert.True(foundJobs.Equals(expectedRomaniaEnglishJobs),
                $"actual vacancies found: {foundJobs}; expected: {expectedRomaniaEnglishJobs}");
        }

        [SetUp]
        public void BeforeTest()
        {
            _driver = DriverFactory.GetWebDriver(WebBrowser.Chrome);
            _driver.Url = "https://careers.veeam.com/";
        }

        [TearDown]
        public void AfterTest()
        {
            if (_driver != null && _driver.WindowHandles.Count > 0)
                _driver.Quit();
        }
    }
}