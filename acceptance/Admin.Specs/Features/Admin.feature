@Admin
Feature: Admin

Scenario: NotSignedIn
  Given I have opened the admin site
  Then the Sign In element is shown

Scenario: SignInWithoutPrivs
  Given I have opened the admin site
  And I sign in
  Then an access restriction message should be shown

Scenario: SignInWithPrivs
  Given I have opened the admin site
  And I sign in
  And I sign out
  And my user is granted admin rights
  And I sign in
  Then the endpoints header should be displayed
