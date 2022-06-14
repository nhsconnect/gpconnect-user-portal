@Report

Feature: SharingReport

Scenario: OrganizationSharingReport
  Given I have opened a sharing report
  Then the name of the organization is shown
  And the address of the organization is shown
  And the postcode of the organization is shown
  And a list of services used is shown

# ?? Scenario for each of the four possible services and their content
