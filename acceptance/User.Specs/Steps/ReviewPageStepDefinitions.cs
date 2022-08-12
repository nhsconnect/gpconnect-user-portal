using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class ReviewPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ReviewPageObject _reviewPage;

        public ReviewPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _reviewPage = new ReviewPageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the review page")]
        public void ThenIAmTakenToTheReviewPage()
        {
            Assert.True(_reviewPage.IsPageVisible());
        }

        [Then(@"the supplier name is ""(.*)""")]
        public void ThenTheSupplierNameIs(string name)
        {
            Assert.Equal(name, _reviewPage.SupplierName.Text);
        }

        [Then(@"the GP connect product is ""(.*)""")]
        public void ThenTheGPConnectProductIs(string product)
        {
            Assert.Equal(product, _reviewPage.ConnectProduct.Text);
        }

        [Then(@"the site ODS code is ""(.*)""")]
        public void ThenTheSiteODSCodeIs(string odsCode)
        {
            Assert.Equal(odsCode, _reviewPage.OdsCode.Text);
        }

        [Then(@"the organisation name is  ""(.*)""")]
        public void ThenTheOrganisationNameIs(string orgName)
        {
            Assert.Equal(orgName, _reviewPage.OrganisationName.Text);
        }

        [Then(@"the organisation address is  ""(.*)""")]
        public void ThenTheOrganisationAddressIs(string orgAddress)
        {
            Assert.Equal(orgAddress, _reviewPage.OrganisationAddress.Text);
        }

        [Then(@"the signatory name is ""(.*)""")]
        public void ThenTheSignatoryNameIs(string signatoryName)
        {
            Assert.Equal(signatoryName, _reviewPage.SignatoryName.Text);
        }

        [Then(@"the signatory role is ""(.*)""")]
        public void ThenTheSignatoryRoleIs(string signatoryRole)
        {
            Assert.Equal(signatoryRole, _reviewPage.SignatoryRole.Text);
        }

        [Then(@"the signatory email is ""(.*)""")]
        public void ThenTheSignatoryEmailIs(string email)
        {
            Assert.Equal(email, _reviewPage.SignatoryEmail.Text);
        }

        [Then(@"the use case is ""(.*)""")]
        public void ThenTheUseCaseIs(string useCase)
        {
            Assert.Equal(useCase, _reviewPage.UseCase.Text);
        }
    }
}

