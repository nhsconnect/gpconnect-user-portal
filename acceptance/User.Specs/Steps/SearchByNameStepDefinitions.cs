using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class SearchByNameStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public SearchByNameStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have opened the search by name page")]
        public void GivenIHaveOpenedTheSearchByNamePage()
        {
            _scenarioContext.Pending();
        }

        [Then(@"I remain on the search by name page")]
        public void ThenIRemainOnTheSearchByNamePage()
        {
            _scenarioContext.Pending();
        }

        [When(@"I enter bad input into the search box")]
        public void WhenIEnterBadInputIntoTheSearchBox()
        {
            _scenarioContext.Pending();
        }

        [When(@"I enter ""(.*)"" in the search box")]
        public void WhenIEnterInTheSearchBox(string legg0)
        {
            _scenarioContext.Pending();
        }

        [When(@"I click the Find button")]
        public void WhenIClickTheFindButton()
        {
            _scenarioContext.Pending();
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
