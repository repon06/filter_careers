using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using veeam_ui_test.Utils;


namespace veeam_ui_test.Pages
{
    public class VeeamCareersPage
    {
        private readonly IWebDriver _driver;

        public VeeamCareersPage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.CssSelector, Using = "#country-element div.selecter")]
        private IWebElement selectCountryField { get; set; }

        [FindsBy(How = How.CssSelector, Using = "div#language")]
        private IWebElement selectLanguageField { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".vacancies-blocks h3")]
        private IWebElement foundJobs { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".vacancies-blocks-container>div")]
        private IList<IWebElement> vacancies { get; set; }

        /// <summary>
        /// ждем изменения кол-ва найденных вакансий 
        /// </summary>
        /// <param name="oldCount"></param>
        /// <returns></returns>
        public VeeamCareersPage WaitUntilChangesVacanciesCount(int oldCount)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            wait.Until(d => GetFoundJobs() != oldCount);
            return this;
        }

        /// <summary>
        /// ждем изменения текста найденных вакансий 
        /// </summary>
        /// <param name="oldCount"></param>
        /// <returns></returns>
        public VeeamCareersPage WaitUntilChangesTextVacanciesCount(string oldVacanciesCount)
        {
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(60));
            wait.Until(d => GetTextFoundJobs() != oldVacanciesCount);
            return this;
        }

        public int GetVacanciesElementsOnPage()
        {
            return vacancies.Count;
        }

        public int GetFoundJobs()
        {
            var text = foundJobs.Text.Trim();
            Console.WriteLine(text);
            return StringHelper.getFirstNumber(text);
        }

        public string GetTextFoundJobs()
        {
            return foundJobs.Text.Trim();
        }


        public VeeamCareersPage SelectCountry(string country)
        {
            PressSelectCountry().SelectCountryByText(country);
            return this;
        }

        public VeeamCareersPage CheckLanguage(string language)
        {
            PressSelectLanguage().CheckLanguageByText(language).SelectLanguageApply();
            return this;
        }


        private VeeamCareersPage PressSelectLanguage()
        {
            selectLanguageField.Click();
            return this;
        }

        private VeeamCareersPage PressSelectCountry()
        {
            selectCountryField.Click();
            return this;
        }

        private VeeamCareersPage SelectCountryByText(string country)
        {
            _driver.FindElement(By.XPath(
                    $".//dd[@id='country-element']/div/div/div[@class='scroller-content']/span[contains(text(),'{country}')]"))
                .Click();
            return this;
        }

        private VeeamCareersPage CheckLanguageByText(string language)
        {
            _driver.FindElement(By.XPath($"//label[contains(., '{language}')]")).Click();
            return this;
        }

        private VeeamCareersPage SelectLanguageApply()
        {
            selectLanguageField.FindElement(By.CssSelector(".selecter-apply a")).Click();
            return this;
        }
    }
}