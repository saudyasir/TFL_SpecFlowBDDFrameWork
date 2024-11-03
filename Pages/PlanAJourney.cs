using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;


namespace TransportForLondonTests.Pages
{
    public class PlanAJourney
    {

        IWebDriver driver;
        WebDriverWait wait;

        public PlanAJourney(IWebDriver driver) { 
        this.driver = driver;
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(20));
        }

        By btnAcceptAllCookies = By.Id("CybotCookiebotDialogBodyLevelButtonLevelOptinAllowAll");
        By txtFrom = By.XPath("//input[@id='InputFrom']");
        By txtTo = By.XPath("//input[@id='InputTo']");
        By autoSuggest = By.XPath("//span[contains(@id,'suggestion-0')]");
        
        By btnPlanMyJourney = By.XPath("//input[@id='plan-journey-button']");

        By errorMessageForFromField = By.XPath("//span[text()='The From field is required.']");
        By errorMessageForToField = By.XPath("//span[text()='The To field is required.']");

        public void acceptAllCookies()
        {
            wait.Until(ExpectedConditions.ElementIsVisible(btnAcceptAllCookies));
            driver.FindElement(btnAcceptAllCookies).Click();
            wait.Until(ExpectedConditions.ElementIsVisible(txtFrom));
        }

        public void enterFromLocation(String fromLocation) {
            driver.FindElement(txtFrom).SendKeys(fromLocation);
        }

        public void enterToLocation(String toLocation)
        {
            driver.FindElement(txtTo).SendKeys(toLocation);
        }

        public void selectFromLocationFromAutoSuggest(String fromLocation)
        {
            driver.FindElement(txtFrom).SendKeys(fromLocation);
            wait.Until(ExpectedConditions.ElementIsVisible(autoSuggest));
            driver.FindElement(autoSuggest).Click();
        }

        public void selectToLocationFromAutoSuggest(String toLocation)
        {
            driver.FindElement(txtTo).SendKeys(toLocation);
            wait.Until(ExpectedConditions.ElementIsVisible(autoSuggest));
            driver.FindElement(autoSuggest).Click();
        }

        public JourneyResults clickPlanMyJourney()
        {
            driver.FindElement(btnPlanMyJourney).Click();
            return new JourneyResults(driver);
        }

        public String getFromFieldErrorMessage()
        {
            String errorMessage = driver.FindElement(errorMessageForFromField).Text;
            Console.WriteLine("Error message for blank From Field = " + errorMessage);
            return errorMessage;
        }

        public String getToFieldErrorMessage()
        {
            String errorMessage = driver.FindElement(errorMessageForToField).Text;
            Console.WriteLine("Error message for blank To Field = " + errorMessage);
            return errorMessage;
        }

    }
}
