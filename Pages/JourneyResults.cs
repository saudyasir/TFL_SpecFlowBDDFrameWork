using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransportForLondonTests.Pages
{
    public class JourneyResults
    {
        IWebDriver driver;
        IJavaScriptExecutor js;
        WebDriverWait wait;

        public JourneyResults(IWebDriver driver)
        {
            this.driver = driver;
            js = (IJavaScriptExecutor)driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        By hdrWalkingAndCycling = By.XPath("//h2[text()='Walking and cycling']");
        By txtCyclingTime = By.XPath("(//div[@class='col2 journey-info']/strong)[1]");
        By txtWalkingTime = By.XPath("(//div[@class='col2 journey-info']/strong)[2]");


        By btnEditPreferences = By.XPath("//button[@class='toggle-options more-options']");
        By radRoutesWithLeastWalking = By.XPath("//label[@for='JourneyPreference_2']");
        By btnUpdateJourney = By.XPath("(//input[@class='primary-button plan-journey-button'])[2]");

        By txtJourneyTime = By.XPath("//div[@class='journey-time no-map']");

        By btnViewDetails = By.XPath("(//div/button[@class='secondary-button show-detailed-results view-hide-details'])[1]");
        
        public String getCyclingTime() {
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(txtCyclingTime));
            String cyclingTime = driver.FindElement(txtCyclingTime).Text;
            Console.WriteLine("Cycling time = "+ cyclingTime);
            return cyclingTime;
        }

        public String getWalkingTime()
        {
            String walkingTime = driver.FindElement(txtWalkingTime).Text;
            Console.WriteLine("Walking time = " + walkingTime);
            return walkingTime;
        }

        public void clickEditPreferences() {
            js.ExecuteScript("window.scrollBy(0, -1000);");
            driver.FindElement(btnEditPreferences).Click();
        }

        public void selectRoutesWithLeastWalking(){
            js.ExecuteScript("window.scrollBy(0, 1000);");
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(radRoutesWithLeastWalking));
            driver.FindElement(radRoutesWithLeastWalking).Click();
        }

        public void clickUpdateJourney(){
            driver.FindElement(btnUpdateJourney).Click();
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(txtJourneyTime));
        }

        public String getJourneyTime()
        {
            String journeyTimeFullText = driver.FindElement(txtJourneyTime).Text;
            String journeyTime = journeyTimeFullText.Replace("Total time:\r\n", " ").Replace("mins", "").Trim();
            Console.WriteLine("Journey time = " + journeyTime);
            return journeyTime;
        }


        public void clickViewDetails()
        {
            driver.FindElement(btnViewDetails).Click();
        }

        public ArrayList getAccessInfo() {
            ArrayList accessInfoText = new ArrayList();
            IList<IWebElement> accessInfoElements = driver.FindElements(By.XPath("(//div[@class='access-information'])[2]/a"));
            foreach (IWebElement element in accessInfoElements)
            {
                String value = element.GetAttribute("data-title");
                Console.WriteLine("Access Info Displayed on UI" + value); 
                accessInfoText.Add(value);
            }
           return accessInfoText;
        
        }


        public Boolean ishdrWalkingAndCyclingDisplayed() {
            Boolean flag = false;
             try
            {
                IWebElement myElement = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(hdrWalkingAndCycling));
                if (myElement.Displayed)
                {
                    flag = true;
                }
            }
            catch (NoSuchElementException)
            {
                Console.WriteLine("Element not found.");
            }
            catch (WebDriverTimeoutException)
            {
                Console.WriteLine("Element not visible within the given time.");
            }
            return flag;

        }

    }
}
