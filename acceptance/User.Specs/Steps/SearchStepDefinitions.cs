using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{

    [Binding]
    public sealed class SearchStepDefinitions
    {
        private readonly SearchPageObject _searchPageObject;

        public SearchStepDefinitions(BrowserDriver browserDriver)
        {
            _searchPageObject = new SearchPageObject(browserDriver.Current);
        }

        [When(@"I click ""Back""")]
        public void WhenIClick()
        {
            _searchPageObject.ClickBack();
        }

        [Then(@"the search box contains ""(.*)""")]
        public void ThenTheSearchBoxContains(string searchInput)
        {
            Assert.Equal(searchInput, _searchPageObject.SearchInput.GetAttribute("value"));
        }


    }
}

