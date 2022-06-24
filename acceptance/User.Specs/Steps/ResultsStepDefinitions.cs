using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class ResultsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ResultsPageObject _resultsPageObject;

        public ResultsStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _resultsPageObject = new ResultsPageObject(browserDriver.Current);
        }

        [Given(@"I have performed a search for records that exist")]
        public void GivenIHavePerformedASearchForRecordsThatExist()
        {
            _resultsPageObject.Open("NHS");
        }

        [When(@"I click the result")]
        public void WhenIClickTheResult()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am taken to the results page")]
        public void ThenIAmTakenToTheResultsPage()
        {
            Assert.True(_resultsPageObject.IsPageVisible());
        }

        [Then(@"I am on the results page")]
        public void ThenIAmOnTheResultsPage()
        {
            Assert.True(_resultsPageObject.IsPageVisible());
        }

        [Then(@"the original search parameter is shown")]
        public void ThenTheOriginalSearchParameterIsShown()
        {
            Assert.Contains("'NHS'", _resultsPageObject.ResultsHeaderText);
        }

        [Then(@"there (are|is) (.*) results?")]
        public void ThenThereAreResults(string ignore, int resultCount)
        {
            Assert.Equal(resultCount, _resultsPageObject.ResultCount());
        }

        [Then(@"the results contain the \w+ ""(.*)""")]
        public void ThenTheResultsContain(string someText)
        {
            _resultsPageObject.ResultsContain(someText);
        }

        [When(@"I click result (\d+)")]
        public void WhenIClickResult(int index)
        {
            _resultsPageObject.ClickResult(index);
        }


    }
}
