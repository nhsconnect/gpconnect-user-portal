using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class ResultsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public ResultsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have performed a search for records that exist")]
        public void GivenIHavePerformedASearchForRecordsThatExist()
        {
            _scenarioContext.Pending();
        }

        [Given(@"I have performed a search for records that don't exist")]
        public void GivenIHavePerformedASearchForRecordsThatDontExist()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the result")]
        public void WhenIClickTheResult()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am on the results page")]
        public void ThenIAmOnTheResultsPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the original search parameter is shown")]
        public void ThenTheOriginalSearchParameterIsShown()
        {
            _scenarioContext.Pending();
        }

        [Then(@"there (are|is) (.*) results?")]
        public void ThenThereAreResults(string ignore, int resultCount)
        {
            _scenarioContext.Pending();
        }

        [Then(@"the results contain the name")]
        public void ThenTheResultsContainTheName()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the results contain the address")]
        public void ThenTheResultsContainTheAddress()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the results contain the postcode")]
        public void ThenTheResultsContainThePostcode()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am taken to the sharing report")]
        public void ThenIAmTakenToTheSharingReport()
        {
            _scenarioContext.Pending();
        }

        [Then(@"there is a link to search again by name")]
        public void ThenThereIsALinkToSearchAgainByName()
        {
            _scenarioContext.Pending();
        }

        [Then(@"there is a link to search again by ODS code")]
        public void ThenThereIsALinkToSearchAgainByODSCode()
        {
            _scenarioContext.Pending();
        }
    }
}
