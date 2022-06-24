@Report

Feature: Detail

Scenario: OrganizationSharingReport
  Given I have opened a detail page
  Then "NHS VEGETAL" is the site name
  And a list of services used is shown

# ?? Scenario for each of the four possible services and their content
