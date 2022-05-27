@Admin
Feature: Admin

Scenario: Sign In
  Given I have opened the admin site
  And I sign in
  Then the endpoints header should be displayed

Scenario: Not Signed In
  Given I have opened the admin site
  Then the Sign In element is shown
