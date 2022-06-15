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
        private readonly TransparencyLandingPageObject _transparencyLandingPage;
        private readonly SearchByNamePageObject _searchByNamePage;

        public TransparencyLandingStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _transparencyLandingPage = new TransparencyLandingPageObject(browserDriver.Current);
            _searchByNamePage = new SearchByNamePageObject(browserDriver.Current);
        }

        [Given(@"I have opened the transparency landing page")]
        public void GivenIHaveOpenedTheTransparencyLandingPage()
        {
            _transparencyLandingPage.Open();
        }

        [Then(@"A button to start the transparency search is present")]
        public void ThenAButtonToStartTheTransparencySearchIsPresent()
        {
            Assert.True(_transparencyLandingPage.IsStartButtonVisible());
        }

        [Then(@"the support email address is present")]
        public void ThenTheAdministratorsEmailAddressIsPresent()
        {
            Assert.True(_transparencyLandingPage.SupportEmail.Displayed);
        }

        [Then(@"the support telephone number is present")]
        public void ThenTheSupportTelephoneNumberIsPresent()
        {
            Assert.True(_transparencyLandingPage.SupportPhone.Displayed);
        }

        [When(@"I click the start now button")]
        public void WhenIClickTheStartNowButton()
        {
            _transparencyLandingPage.ClickStart();
        }

        [Then(@"I am taken to the search by name page")]
        public void ThenIAmTakenToTheSearchByNamePage()
        {
            Assert.True(_searchByNamePage.IsPageVisible());
        }
    }
}
