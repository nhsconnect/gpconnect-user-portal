@Results

Feature: Results

Scenario: NoResults
  Given I have performed a search for records that don't exist
  Then I am taken to the no results page
  And a no results message with the original search parameter is shown
  And there is a link to search again by name
  And there is a link to search again by ODS code

Scenario: SomeResults
  Given I have performed a search for records that exist
  Then I am on the results page
  And the original search parameter is shown
  And there are 2 results
  And the results contain the name "NHS VEGETAL"
  And the results contain the address "BEEF WELLINGTON PLACE"
  And the results contain the postcode "LS2 5AP"
  When I click result 2
  Then I am taken to the detail page
