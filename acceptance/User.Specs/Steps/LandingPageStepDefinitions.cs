using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class LandingPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public LandingPageStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have opened the landing page")]
        public void GivenIHaveOpenedTheLandingPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"A link to the transparency route is present")]
        public void ThenALinkToTheTransparencyRouteIsPresent()
        {
            _scenarioContext.Pending();
        }
    }
}
