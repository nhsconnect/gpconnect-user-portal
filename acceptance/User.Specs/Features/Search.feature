@Search

Feature: Search

Scenario: SearchWithoutInputs
  Given I have opened the search page
  When I click the search button
  Then A validation error is shown

Scenario: SearchTooManyInputs
  Given I have opened the search page
  And I enter "Nonsense" into the ODS Code input
  And I enter "Nonsense" into the Provider Name input
  When I click the search button
  Then A validation error is shown

Scenario: NoProvidersFoundOnName
  Given I have opened the search page
  And I enter "Nonsense" into the Provider Name input
  When I click the search button
  Then no results are returned

Scenario: NoProvidersFoundOnCode
  Given I have opened the search page
  And I enter "Nonsense" into the ODS Code input
  When I click the search button
  Then no results are returned
