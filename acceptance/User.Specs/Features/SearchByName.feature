@SearchByName

Feature: SearchByName

Scenario: SearchByNameEmptyInput
  Given I have opened the search by name page
  When I click the Find button
  Then I remain on the search by name page
  And a validation error is shown

Scenario: SearchByNameBadInput
  Given I have opened the search by name page
  When I enter bad input into the search box
  And I click the Find button
  Then I remain on the search by name page
  And a validation error is shown

Scenario: SearchByNameWithResults
  Given I have opened the search by name page
  When I enter "Legg" in the search box
  And I click the Find button
  Then I am taken to the results page
  And there is 1 result

Scenario: SearchByNameNoResults
  Given I have opened the search by name page
  When I enter "Myxptlk" in the search box
  And I click the Find button
  Then I am taken to the results page
  And there are 0 results

Scenario: SearchByNameHasLinkToOdsSearch
  Given I have opened the search by name page
  Then there is a link to the ODS search page
  When I click the link to the ODS search page
  Then I am taken to the ODS search page
