using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class LandingPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LandingPageObject _landingPage;
        private readonly TransparencyLandingPageObject _transparencyLandingPageObject;

        public LandingPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _landingPage = new LandingPageObject(browserDriver.Current);
            _transparencyLandingPageObject = new TransparencyLandingPageObject(browserDriver.Current);
        }

        [Given(@"I have opened the landing page")]
        public void GivenIHaveOpenedTheLandingPage()
        {
            _landingPage.Open();
        }

        [Then(@"A link to the transparency route is present")]
        public void ThenALinkToTheTransparencyRouteIsPresent()
        {
            Assert.True(_landingPage.IsSearchLinkVisible());
        }

        [Then(@"A link to the sharing agreement route is present")]
        public void ThenALinkToTheSharingAgreementRouteIsPresent()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the link to the transparency route")]
        public void WhenIClickTheLinkToTheTransparencyRoute()
        {
            _landingPage.ClickSearchLink();
        }

        [Then(@"I am taken to the transparency landing page")]
        public void ThenIAmTakenToTheTransparencyLandingPage()
        {

        }

        [Then(@"the support telephone number is present")]
        public void ThenTheSupportTelephoneNumberIsPresent()
        {
            Assert.True(_landingPage.SupportPhone.Displayed);
        }

        [Then(@"the support email address is present")]
        public void ThenTheAdministratorsEmailAddressIsPresent()
        {
            Assert.True(_landingPage.SupportEmail.Displayed);
        }

    }
}
