@Results

Feature: Results

Scenario: NoResults
  Given I have performed a search for records that don't exist
  Then I am on the results page
  And the original search parameter is shown
  And there are 0 results
  And there is a link to search again by name
  And there is a link to search again by ODS code

Scenario: SomeResults
  Given I have performed a search for records that exist
  Then I am on the results page
  And the original search parameter is shown
  And the results contain the name
  And the results contain the address
  And the results contain the postcode
  When I click the result
  Then I am taken to the sharing report
