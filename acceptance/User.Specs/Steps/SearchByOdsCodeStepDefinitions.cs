using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;


namespace GpConnect.DataSharing.User.Specs.Steps;
{
    [Binding]
    public class SearchByOdsCodeStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly OdsSearchPageObject _odsSearchPage;

        public SearchByOdsCodeStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _odsSearchPage = new OdsSearchPageObject(browserDriver.Current);
        }
        [When(@"I enter a valid ODS code into the search box")]
        public void WhenIEnterAValidODSCodeIntoTheSearchBox()
        {
            _odsSearchPage.EnterSearchText("X27");
        }

        [Given(@"I have opened the ODS search page")]
        public void GivenIHaveOpenedTheODSSearchPage()
        {
            _odsSearchPage.Open();
            Assert.True(_odsSearchPage.IsPageVisible());
        }

        [Then(@"I am taken to the ODS search page")]
        public void ThenIAmTakenToTheODSSearchPage()
        {
            Assert.True(_odsSearchPage.IsPageVisible());
        }

        [When(@"I enter an invalid ODS code into the search box")]
        public void WhenIEnterAnInvalidODSCodeIntoTheSearchBox()
        {
            _scenarioContext.Pending();
        }

        [When(@"I enter a non-existent ODS code into the search box")]
        public void WhenIEnterANonExistentODSCodeIntoTheSearchBox()
        {
            _odsSearchPage.EnterSearchText("XL5");
        }


        [When(@"I click the find button")]
        public void WhenIClickTheFindButton()
        {
            _odsSearchPage.ClickFind();
        }

        [Then(@"I remain on the ODS search page")]
        public void ThenIRemainOnTheODSSearchPage()
        {
            Assert.True(_odsSearchPage.IsPageVisible());
        }
    }
}
