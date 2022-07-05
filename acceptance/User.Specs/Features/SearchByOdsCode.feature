@Search

Feature: SearchByOdsCode

Scenario: SearchByOdsCodeWithResult
  Given I have opened the ODS search page
  When I enter a valid ODS code into the search box
  And I click the find button
  Then I am taken to the detail page

Scenario: SearchByOdsCodeNoResult
  Given I have opened the ODS search page
  When I enter a non-existent ODS code into the search box
  And I click the find button
  Then I am taken to the no results page

# Scenario: SearchByOdsCodeBadCode
#   Given I have opened the ODS search page
#   When I enter an invalid ODS code into the search box
#   And I click the find button
#   Then I remain on the ODS search page
#   And a validation error is shown

Scenario: SearchByOdsCodeEmptyInput
  Given I have opened the ODS search page
  When I click the find button
  Then I remain on the ODS search page
  And a validation error is shown

