@SharingApplication

Feature: SharingApplication

Scenario: ChooseSupplier
  Given I have opened the application landing page
  When I click the "Apply Now" button
  Then I am taken to the software supplier page
  Then there are cookies
  When I select "Person Centred Software (PCS)" in the supplier list
  And I click the "Next" button
  Then I am taken to the software supplier page
  And the "Access Record: HTML" label is shown
  And the "Access Record: Structured" label is shown
  And the "Appointment Management" label is shown
  And the "Send Document" label is shown
  When I check the "Send Document" box
  And I click the "Next" button
  Then I am taken to the organisation page
  When I enter "A6JR" into the site ODS code field
  And I click the "Find Organisation" button
  Then I am taken to the organisation page
  And the organisation name is "BERRYSTEAD NURSING & RESIDENTIAL HOME LTD"
  And the organisation address is "1001 MELTON ROAD, SYSTON, LEICESTER, LEICESTERSHIRE, LE7 2BE, ENGLAND"
