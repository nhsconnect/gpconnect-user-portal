@Landing

Feature: Landing

Scenario: LandingPageHasLinkToTransparency
  Given I have opened the landing page
  Then A link to the transparency route is present
  When I click the link to the transparency route
  Then I am taken to the transparency landing page

