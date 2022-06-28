@SearchByName

Feature: SearchByName

Scenario: SearchByNameEmptyInput
  Given I have opened the search by name page
  When I click the Find button
  Then I remain on the search by name page
  And a validation error is shown

# Scenario: SearchByNameBadInput
#   Given I have opened the search by name page
#   When I enter bad input into the search box
#   And I click the Find button
#   Then I remain on the search by name page
#   And a validation error is shown
#   TODO See [JIRA](https://nhsd-jira.digital.nhs.uk/browse/GCNDSP-325)

Scenario: SearchByNameWithResults
  Given I have opened the search by name page
  When I enter "NHS" in the search box
  And I click the Find button
  Then I am taken to the results page
  And there are 2 results

Scenario: SearchByNameWithSingleResult
  Given I have opened the search by name page
  When I enter "VEGETAL" in the search box
  And I click the Find button
  Then I am taken to the detail page
  And "NHS VEGETAL" is the site name

Scenario: SearchByNameNoResults
  Given I have opened the search by name page
  When I enter "Myxptlk" in the search box
  And I click the Find button
  Then I am taken to the no results page
  And there are 0 results

Scenario: SearchByNameHasLinkToOdsSearch
  Given I have opened the search by name page
  Then there is a link to the ODS search page
  When I click the link to the ODS search page
  Then I am taken to the ODS search page
