Feature: JourneyPlanner
Test Journey Planner Widget


@journeyfromLeicesterSquareUndergroundStationtoCoventGardenUndergroundStation
Scenario: Plan a valid journey from Leicester Square Underground Station to Covent Garden Underground Station
	Given I navigate to the TfL Plan a Journey page
	When I select "Leicester Square Underground " value from auto complete suggestion as the start location
	And I select "Covent Garden " value from auto complete suggestion as the to location
	And I click Plan my Journey button
	Then I validate the walking and cycling time results


@LeastWalkingDistance
Scenario: Edit Preferences - Least Walking
	Given I have Planned my journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"
	When I edit preferences
	And I select Routes with least walking
	And I click Update Journey button
	Then I should see an updated journey time reflecting the least walking route


@VerifyCompleteAccessInformationAtCoventGardenUndergroundStation 
Scenario: Verify complete access information at Covent Garden Underground Station 
	Given I have Planned my journey from "Leicester Square Underground Station" to "Covent Garden Underground Station"
	And I edit preferences
	And I select Routes with least walking
	And I click Update Journey button
	When I click View Details
	Then I Verify complete access information at Covent Garden Underground Station 
	
@JouneySearchWithoutAnyLocation
Scenario: Plan a journey without any location
	Given I navigate to the TfL Plan a Journey page
	When I click Plan my Journey button
	Then I validate the error message

@JouneySearchWithBothInvalidLocations
Scenario: Plan a journey with both invalid locations
	Given I navigate to the TfL Plan a Journey page
	When I enter "abcd12548" as the start location
	And I enter "testinglok254" as the to location
	And I click Plan my Journey button
	Then I validate if results are displayed or not


@JouneySearchWithFromInvalidLocation
Scenario: Plan a journey from invalid location
	Given I navigate to the TfL Plan a Journey page
	When I enter "abcd12548" as the start location
	And I enter "Covent Garden Underground Station" as the to location
	And I click Plan my Journey button
	Then I validate if results are displayed or not


@JouneySearchWithToInvalidLocation
Scenario: Plan a journey To invalid location
	Given I navigate to the TfL Plan a Journey page
	When I enter "Leicester Square Underground Station" as the start location
	And I enter "abcd12548" as the to location
	And I click Plan my Journey button
	Then I validate if results are displayed or not
