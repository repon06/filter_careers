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

        [Test]
        public void SearchJobsTest()
        {
            var country = "Romania";
            var language = "English";
            var expectedRomaniaEnglishJobs = 27;

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
            _driver = DriverFactory.GetWebDriver(WebBrowser.chrome);
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