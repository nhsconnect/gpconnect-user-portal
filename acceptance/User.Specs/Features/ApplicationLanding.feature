@ApplicationLanding

Feature: ApplicationLanding

Scenario: ApplicationLandingPage
  Given I have opened the application landing page
  Then A button to start the application is present
  And the support email address is present
  And the support telephone number is present
  When I click the start now button
  Then I am taken to the software supplier page

# Scenario: ApplicationLandingPage-NoCompletedPreviousPages
#   Given I have opened the 'path' in the application journey page
#   Then I am taken to the application landing page