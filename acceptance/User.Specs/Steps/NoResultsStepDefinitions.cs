using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class NoResultsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        private readonly NoResultsPageObject _noResultsPage;

        public NoResultsStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _noResultsPage = new NoResultsPageObject(browserDriver.Current);
        }

        [Then(@"I am on the no results page")]
        public void ThenIAmOnTheNoResultsPage()
        {
            _noResultsPage.IsPageVisible();
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

        [Then(@"a no results message with the original search parameter is shown")]
        public void ThenTheOriginalSearchParameterIsShown()
        {
            Assert.True(_noResultsPage.HasNoResultsBanner("Myxptlk"));
        }


        [Then(@"there is a link to search again by name")]
        public void ThenThereIsALinkToSearchAgainByName()
        {
            Assert.True(_noResultsPage.ByNameSearchLink.Displayed);
        }

        [Then(@"there is a link to search again by ODS code")]
        public void ThenThereIsALinkToSearchAgainByODSCode()
        {
            Assert.True(_noResultsPage.OdsCodeSearchLink.Displayed);
        }
    }
}

