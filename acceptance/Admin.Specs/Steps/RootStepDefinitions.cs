using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

using Xunit;
using TechTalk.SpecFlow;

namespace Admin.Specs.Steps
{

    [Binding]
    public sealed class RootStepDefinitions
    {

        private BrowserDriver _browserDriver;
        private readonly RootPageObject _rootPageObject;

        public RootStepDefinitions(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
            _rootPageObject = new RootPageObject(browserDriver.Current);
        }

        [Given("I have opened the admin site")]
        public void GivenIHaveOpenedSite()
        {
            _rootPageObject.Open();
        }

        [Then("the Sign In element is shown")]
        public void ThenSignInIsDisplayed()
        {
            Assert.True(_rootPageObject.IsSignInElementVisible());
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
