using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class OdsSearchStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public OdsSearchStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have opened the ODS search page")]
        public void GivenIHaveOpenedTheODSSearchPage()
        {
            _scenarioContext.Pending();
        }

        [When(@"I enter an invalid ODS code into the search box")]
        public void WhenIEnterAnInvalidODSCodeIntoTheSearchBox()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the find button")]
        public void WhenIClickTheFindButton()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I remain on the ODS search page")]
        public void ThenIRemainOnTheODSSearchPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"a validation error is shown")]
        public void ThenAValidationErrorIsShown()
        {
            _scenarioContext.Pending();
        }
    }
}
