@SharingApplication

Feature: SharingApplication

Scenario: ChooseSupplier
  Given I have opened the application landing page
  When I click the "Apply now" link
  Then I am taken to the software supplier page
  When I select "Person Centred Software (PCS)" in the supplier list
  And I click the "Next" button
  Then I am taken to the software supplier page
  And the GP Connect product selection panel is shown
