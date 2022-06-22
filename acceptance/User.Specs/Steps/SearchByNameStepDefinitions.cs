using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class SearchByNameStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly SearchByNamePageObject _searchByNamePage;

        public SearchByNameStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _searchByNamePage = new SearchByNamePageObject(browserDriver.Current);
        }

        [Given(@"I have performed a search for records that don't exist")]
        public void GivenIHavePerformedASearchForRecordsThatDontExist()
        {
            _searchByNamePage.Open();
            Assert.True(_searchByNamePage.IsPageVisible());
            _searchByNamePage.EnterSearchText("Myxptlk");
            _searchByNamePage.ClickFind();
        }

        [Given(@"I have opened the search by name page")]
        public void GivenIHaveOpenedTheSearchByNamePage()
        {
            _searchByNamePage.Open();
        }

        [Then(@"I remain on the search by name page")]
        public void ThenIRemainOnTheSearchByNamePage()
        {
            Assert.True(_searchByNamePage.IsPageVisible());
        }

        [When(@"I enter bad input into the search box")]
        public void WhenIEnterBadInputIntoTheSearchBox()
        {
            _scenarioContext.Pending();
        }

        [When(@"I enter ""(.*)"" in the search box")]
        public void WhenIEnterInTheSearchBox(string input)
        {
            _searchByNamePage.EnterSearchText(input);
        }

        [When(@"I click the Find button")]
        public void WhenIClickTheFindButton()
        {
            _searchByNamePage.ClickFind();
        }

        [Then(@"I am taken to the results page")]
        public void ThenIAmTakenToTheResultsPage()
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the link to the ODS search page")]
        public void WhenIClickTheLinkToTheODSSearchPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"there is a link to the ODS search page")]
        public void ThenThereIsALinkToTheODSSearchPage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I am taken to the ODS search page")]
        public void ThenIAmTakenToTheODSSearchPage()
        {
            _scenarioContext.Pending();
        }
    }
}
