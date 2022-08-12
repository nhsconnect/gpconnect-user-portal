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
  When I click the "Next" button
  Then I am taken to the signatory page
  When I enter "Angus McTestBloke" into the signatory name field
  And I enter "Tester" into the signatory role field
  And I enter "angus@testbloke.com" into the signatory email field
  And I click the "Next" button
  Then I am taken to the use case page
  When I enter "A lot of text" into the use case description field
  And I click the "Next" button
  Then I am taken to the agreement page
  When I click the "Next" link
  Then I am taken to the review page
  And the supplier name is "Person Centred Software (PCS)"
  And the GP connect product is "Send Document"
  And the site ODS code is "A6JR"
  And the organisation name is  "BERRYSTEAD NURSING & RESIDENTIAL HOME LTD"
  And the organisation address is  "1001 MELTON ROAD, SYSTON, LEICESTER, LEICESTERSHIRE, LE7 2BE, ENGLAND"
  And the signatory name is "Angus McTestBloke"
  And the signatory role is "Tester"
  And the signatory email is "angus@testbloke.com"
  And the use case is "A lot of text"
  When I click the "Submit" button
  Then I am taken to the confirmation page
