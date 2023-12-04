using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using TechTalk.SpecFlow;

namespace Linkedintests.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        public IWebDriver? driver;
        private IWebElement? passwordInput;
        /*
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
        */
        [BeforeFeature]
        public void InitializeBrowser()
        {
            driver = new ChromeDriver();
        }
        [Given(@"Usernwill be on the login page")]
        public void GivenUsernwillBeOnTheLoginPage()
        {
            
            driver.Url = "https://www.linkedin.com";

        }
        [AfterFeature]
        public  void CleanUp()
        {
            driver?.Quit();
        }
        [When(@"User will enter '(.*)' ")]
        public void WhenUserWillEnterUsername(string un)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element Not Found";

            IWebElement emailInput = fluentWait.Until(driv => driv.FindElement(By.Id("session_key")));
           // IWebElement passwordInput = fluentWait.Until(driv => driv.FindElement(By.Id("session_password")));


            emailInput.SendKeys(un);
           // passwordInput.SendKeys("12345");

            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        }

        [When(@"User will enter '(.*)' ")]
        public void WhenUserWillEnterPassword(string pwd)
        {
            DefaultWait<IWebDriver> fluentWait = new DefaultWait<IWebDriver>(driver);
            fluentWait.Timeout = TimeSpan.FromSeconds(5);
            fluentWait.PollingInterval = TimeSpan.FromMilliseconds(100);
            fluentWait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            fluentWait.Message = "Element Not Found";

           // IWebElement emailInput = fluentWait.Until(driv => driv.FindElement(By.Id("session_key")));
            IWebElement passwordInput = fluentWait.Until(driv => driv.FindElement(By.Id("session_password")));


            //emailInput.SendKeys("abc@xyz.com");
            passwordInput.SendKeys(pwd);

            // driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);

        }

        [When(@"User will click on login button")]
        public void WhenUserWillClickOnLoginButton()
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);",
                driver.FindElement(By.XPath("//button[@type='submit']")));

            Thread.Sleep(TimeSpan.FromSeconds(5));
            js.ExecuteScript("arguments[0].click();",
                driver.FindElement(By.XPath("//button[@type='submit']")));


        }

        [Then(@"User will be redirected to Homepage")]
        public void ThenUserWillBeRedirectedToHomepage()
        {
            Assert.That(driver.Title.Contains("Log In"));
        }

        [Then(@"Error message for Password Length should be thrown")]
        public void ThenErrorMessageForPasswordLengthShouldBeThrown()
        {
            IWebElement alertPara = driver.FindElement(By.XPath("//p[@for='session_password']"));
            string alerttext = alertPara.Text;
            Assert.That(alerttext.Equals("The password you provided must have at least 6 characters"));
            
        }

    }
}
