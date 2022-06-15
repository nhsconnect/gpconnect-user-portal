@TransparencySearchLanding

Feature: TransparencySearchLanding

Scenario: TransparencyLandingPage
  Given I have opened the transparency landing page
  Then A button to start the transparency search is present
  And the support email address is present
  And the support telephone number is present
  When I click the start now button
  Then I am taken to the search by name page
