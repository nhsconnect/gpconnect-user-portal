using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

using Xunit;
using TechTalk.SpecFlow;

namespace Admin.Specs.Steps
{

    [Binding]
    public sealed class RootStepDefinitions
    {

        private readonly RootPageObject _rootPageObject;

        public RootStepDefinitions(BrowserDriver browserDriver)
        {
            _rootPageObject = new RootPageObject(browserDriver.Current);
        }

        [Given("I have opened the admin site")]
        public void GivenIHaveOpenedSite()
        {
            _rootPageObject.Open();
        }

        [Given("I sign in")]
        public void GivenISignIn()
        {
            _rootPageObject.ClickSignIn();
        }

        [Then("the endpoints header should be displayed")]
        public void ThenEndpointHeaderIsDisplayed()
        {
            Assert.True(_rootPageObject.IsEndpointHeaderVisible());
        }
    }
}
