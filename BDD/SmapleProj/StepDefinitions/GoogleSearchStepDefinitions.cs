﻿using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleProj.StepDefinitions
{
    [Binding]
    internal class GoogleSearchStepDefinitions
    {
        public IWebDriver driver;

        [BeforeScenario]
        public void InitializeBrowse()
        {
            driver = new ChromeDriver();
            driver.Url = "https://www.google.com";
            driver.Manage().Window.Maximize();
        }

        [AfterScenario]
        public void CleanupBrowse()
        {
            driver.Quit();
        }
        [Given(@"Google Homepage should be loaded")]
        public void GivenGoogleHomepageShouldBeLoaded()
        {
            driver.Url = "https://www.google.com";
            driver.Manage().Window.Maximize();

        }
        [When(@"Type ""([^""]*)"" in the search text input")]
        public void WhenTypeInTheSearchTextInput(string searchtext)
        {
            IWebElement searchInput = driver.FindElement(By.Id("APjFqb"));
            searchInput.SendKeys(searchtext);

        }
        [When(@"Click on the Google Search button")]
        public void WhenClickOnTheGoogleSearchButton()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element Not Found";

            IWebElement? gsb = fluentWait.Until(d =>
            {
                IWebElement searchButton = driver.FindElement(By.Name("btnK"));
                return searchButton.Displayed ? searchButton : null;
            });

            if (gsb != null)
            {
                gsb.Click();
            }
        }


        [Then(@"the results should be displayed on the next page with title ""([^""]*)""")]
        public void ThenTheResultsShouldBeDisplayedOnTheNextPageWithTitle(string title)
        {
            Assert.That(driver.Title, Is.EqualTo(title));
        }

        [When(@"Click on the IMFL button")]
        public void WhenClickOnTheIMFLButton()
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element Not Found";

            IWebElement? iflb = fluentWait.Until(d =>
            {
                IWebElement searchButton = driver.FindElement(By.Name("btnI"));
                return searchButton.Displayed ? searchButton : null;
            });

            if (iflb != null)
            {
                iflb.Click();
            }
        }

        [Then(@"the results should be redirected to a new page with title ""([^""]*)""")]
        public void ThenTheResultsShouldBeRedirectedToANewPageWithTitle(string title)
        {
            Assert.That(driver.Title.Contains(title));
        }

    }


}

