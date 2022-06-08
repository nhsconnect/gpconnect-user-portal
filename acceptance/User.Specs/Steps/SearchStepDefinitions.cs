using Xunit;
using TechTalk.SpecFlow;

using User.Specs.Drivers;
using User.Specs.PageObjects;

namespace User.Specs.Steps
{

    [Binding]
    public sealed class SearchStepDefinitions
    {
        private readonly SearchPageObject _searchPageObject;

        public SearchStepDefinitions(BrowserDriver browserDriver)
        {
            _searchPageObject = new SearchPageObject(browserDriver.Current);
        }

        [Given("I have opened the search page")]
        public void WhenIHaveOpenedTheSearchPage() {
            _searchPageObject.Open();
        }

        [When("I click the search button")]
        public void WhenIClickTheSearchButton()
        {
            _searchPageObject.ClickSearchButton();
        }

        [Then("A validation error is shown")]
        public void ThenAValidationErrorIsShown()
        {
            Assert.True(_searchPageObject.IsErrorBannerVisible());
        }

        [Given(@"I enter ""(.*)"" into the ODS Code input")]
        public void GivenIEnterIntoTheOdsCodeInput(string input)
        {
            _searchPageObject.EnterOdsCode(input);
        }

        [Given(@"I enter ""(.*)"" into the Provider Name input")]
        public void GivenIEnterIntoTheProviderNameInput(string input)
        {
            _searchPageObject.EnterProviderName(input);
        }

        [Then("no results are returned")]
        public void ThenNoResultsAreReturned()
        {
            Assert.True(_searchPageObject.IsNoResultsVisible());
        }

    }
}

