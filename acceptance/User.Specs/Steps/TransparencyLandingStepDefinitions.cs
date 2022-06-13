
using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{

    [Binding]
    public sealed class TransparencyLandingStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        public TransparencyLandingStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have opened the transparency landing page")]
        public void GivenIHaveOpenedTheTransparencyLandingPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"A button to start the transparency search is present")]
        public void ThenAButtonToStartTheTransparencySearchIsPresent()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the administrator's email address is present")]
        public void ThenTheAdministratorsEmailAddressIsPresent()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the support telephone number is present")]
        public void ThenTheSupportTelephoneNumberIsPresent()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the start now button")]
        public void WhenIClickTheStartNowButton()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am taken to the search by name page")]
        public void ThenIAmTakenToTheSearchByNamePage()
        {
            _scenarioContext.Pending();
        }
    }
}
