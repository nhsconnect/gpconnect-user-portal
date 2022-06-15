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
        private readonly LandingPageObject _landingPageObject;
        private readonly TransparencyLandingPageObject _transparencyLandingPageObject;

        public LandingPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _landingPageObject = new LandingPageObject(browserDriver.Current);
            _transparencyLandingPageObject = new TransparencyLandingPageObject(browserDriver.Current);
        }

        private readonly string URL = "https://localhost:5003";

        [Given(@"I have opened the landing page")]
        public void GivenIHaveOpenedTheLandingPage()
        {
            _landingPageObject.Open();
        }

        [Then(@"A link to the transparency route is present")]
        public void ThenALinkToTheTransparencyRouteIsPresent()
        {
            Assert.True(_landingPageObject.IsSearchLinkVisible());
        }

        [Then(@"A link to the sharing agreement route is present")]
        public void ThenALinkToTheSharingAgreementRouteIsPresent()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the link to the transparency route")]
        public void WhenIClickTheLinkToTheTransparencyRoute()
        {
            _landingPageObject.ClickSearchLink();
        }

        [Then(@"I am taken to the transparency landing page")]
        public void ThenIAmTakenToTheTransparencyLandingPage()
        {

        }
    }
}
