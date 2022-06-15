using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class NoResultsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public NoResultsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"I have performed a search for records that don't exist")]
        public void GivenIHavePerformedASearchForRecordsThatDontExist()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am on the no results page")]
        public void ThenIAmOnTheNoResultsPage()
        {
            _scenarioContext.Pending();
        }

        // [Then(@"the original search parameter is shown")]
        // public void ThenTheOriginalSearchParameterIsShown()
        // {
        //     _scenarioContext.Pending();
        // }

        [Then(@"there are no results")]
        public void ThenThereAreNoResults(string ignore, int resultCount)
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

