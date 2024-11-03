using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Collections;
using TechTalk.SpecFlow;
using TransportForLondonTests.Pages;


namespace TransportForLondonTests.StepDefinitions
{
    [Binding]
    public sealed class JourneyPlannerStepDefinitions
    {

        private IWebDriver driver;
        PlanAJourney planAJourney;
        JourneyResults journeyResults;

        public JourneyPlannerStepDefinitions(IWebDriver driver)
        {   this.driver = driver;
            planAJourney = new PlanAJourney(driver);
        }


        [Given(@"I navigate to the TfL Plan a Journey page")]
        public void GivenINavigateToTheTfLPlanAJourneyPage()
        {
            driver.Url = "https://tfl.gov.uk/plan-a-journey";
            planAJourney.acceptAllCookies();
            
        }

        [When(@"I select ""([^""]*)"" value from auto complete suggestion as the start location")]
        public void WhenISelectValueFromAutoCompleteSuggestionAsTheStartLocation(string fromLocation)
        {
          planAJourney.selectFromLocationFromAutoSuggest(fromLocation);
        
        }


        [When(@"I select ""([^""]*)"" value from auto complete suggestion as the to location")]
        public void WhenISelectValueFromAutoCompleteSuggestionAsTheToLocation(string toLocation)
        {
           planAJourney.selectToLocationFromAutoSuggest(toLocation);
        
        }

        [When(@"I enter ""([^""]*)"" as the start location")]
        public void WhenIEnterAsTheStartLocation(string fromLocation)
        {
            planAJourney.enterFromLocation(fromLocation);
        }

        [When(@"I enter ""([^""]*)"" as the to location")]
        public void WhenIEnterAsTheToLocation(string toLocation)
        {
            planAJourney.enterToLocation(toLocation);
        }

        [When(@"I click Plan my Journey button")]
        public void WhenIClickPlanMyJourneyButton()
        {
            journeyResults = planAJourney.clickPlanMyJourney();
            
        }

        [Then(@"I validate the walking and cycling time results")]
        public void ThenIValidateTheWalkingAndCyclingTimeResults()
        {
            String cyclingTime = journeyResults.getCyclingTime();
            Assert.AreEqual("1", cyclingTime, "Cycling Time is not as expected on UI");
            String walkingTime = journeyResults.getWalkingTime();
            Assert.AreEqual("6",walkingTime,"Walking Time is not as expected on UI");

        }

        [Given(@"I have Planned my journey from ""([^""]*)"" to ""([^""]*)""")]
        public void GivenIHavePlannedMyJourneyFromLeicesterSquareUndergroundStationToCoventGardenUndergroundStation(string fromLocation, string toLocation)
        {
            GivenINavigateToTheTfLPlanAJourneyPage();
            planAJourney.enterFromLocation(fromLocation);
            planAJourney.enterToLocation(toLocation);
            journeyResults = planAJourney.clickPlanMyJourney();
        }


        [When(@"I edit preferences")]
        public void WhenIEditPreferences()
        {
            journeyResults.clickEditPreferences();
        }

        [When(@"I select Routes with least walking")]
        public void WhenISelectRoutesWithLeastWalking()
        {
            journeyResults.selectRoutesWithLeastWalking();
        }

        [When(@"I click Update Journey button")]
        public void WhenIClickUpdateJourneyButton()
        {
            journeyResults.clickUpdateJourney();
        }

        [Then(@"I should see an updated journey time reflecting the least walking route")]
        public void ThenIShouldSeeAnUpdatedJourneyTimeReflectingTheLeastWalkingRoute()
        {
            String journeyTime = journeyResults.getJourneyTime();
            Assert.AreEqual("11", journeyTime, "Journey Time is not as expected on UI");

        }


        [Given(@"I edit preferences")]
        public void GivenIEditPreferences()
        {
            journeyResults.clickEditPreferences();
        }

        [Given(@"I select Routes with least walking")]
        public void GivenISelectRoutesWithLeastWalking()
        {
            journeyResults.selectRoutesWithLeastWalking();
        }

        [Given(@"I click Update Journey button")]
        public void GivenIClickUpdateJourneyButton()
        {
            journeyResults.clickUpdateJourney();
        }

        [When(@"I click View Details")]
        public void WhenIClickViewDetails()
        {
            journeyResults.clickViewDetails();
        }

        [Then(@"I Verify complete access information at Covent Garden Underground Station")]
        public void ThenIVerifyCompleteAccessInformationAtCoventGardenUndergroundStation()
        {
           ArrayList accessInfo =  journeyResults.getAccessInfo();
           ArrayList expectedaccessInfo = new ArrayList { "Up stairs", "Up lift", "Level walkway"};
           CollectionAssert.AreEqual(expectedaccessInfo, accessInfo, "Access Info is not correct on UI");
        }

        [Then(@"I validate the error message")]
        public void ThenIValidateTheErrorMessage()
        {
            Assert.AreEqual("The From field is required.", planAJourney.getFromFieldErrorMessage(), "Error Message for From Filed is not as expected on UI");
            Assert.AreEqual("The To field is required.", planAJourney.getToFieldErrorMessage(), "Error Message for From Filed is not as expected on UI");
        }

        [Then(@"I validate if results are displayed or not")]
        public void ThenIValidateIfResultsAreDisplayedOrNot()
        {
            Assert.False(journeyResults.ishdrWalkingAndCyclingDisplayed(), "Widget provides results when an invalid journey is planned");
        }


    }
}
