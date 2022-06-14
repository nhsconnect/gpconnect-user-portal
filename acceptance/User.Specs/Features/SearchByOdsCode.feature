@Search

Feature: SearchByOdsCode

Scenario: SearchByOdsCodeWithResult
  Given I have opened the ODS search page
  When I enter a valid ODS code into the search box
  And I click the find button
  Then I am taken to the results page
  And there is 1 result

Scenario: SearchByOdsCodeNoResult
  Given I have opened the ODS search page
  When I enter a valid ODS code into the search box
  And I click the find button
  Then I am taken to the results page
  And there are 0 results

Scenario: SearchByOdsCodeBadCode
  Given I have opened the ODS search page
  When I enter an invalid ODS code into the search box
  And I click the find button
  Then I remain on the ODS search page
  And a validation error is shown

Scenario: SearchByOdsCodeEmptyInput
  Given I have opened the ODS search page
  When I click the find button
  Then I remain on the ODS search page
  And a validation error is shown

