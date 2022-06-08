@FourOhFour

Feature: NotFound

Scenario: PageNotFound
  Given I navigate to a page that is not there
  Then The standard 404 page is shown
