using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class OrganisationPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OrganisationPageObject _organisationPage;

        public OrganisationPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _organisationPage = new OrganisationPageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the organisation page")]
        public void ThenIAmTakenToTheOrganisationPage()
        {
            Assert.True(_organisationPage.IsPageVisible());
        }

        [When(@"I enter ""(.*)"" into the site ODS code field")]
        public void WhenIEnterOdsCode(string code)
        {
            _organisationPage.EnterOdsCode(code);
        }

        [Then(@"the organisation name is ""(.*)""")]
        public void ThenTheOrganisationNameIs(string name)
        {
            Assert.Equal(name, _organisationPage.OrganisationName.Text);
        }

        [Then(@"the organisation address is ""(.*)""")]
        public void ThenTheOrganisationAddressIs(string address)
        {
            Assert.Equal(address, _organisationPage.OrganisationAddress.Text);
        }
    }
}
