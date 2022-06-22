using Xunit;
using TechTalk.SpecFlow;


using GpConnect.DataSharing.Admin.Specs.Drivers;
using GpConnect.DataSharing.Admin.Specs.PageObjects;

namespace GpConnect.DataSharing.Admin.Specs.Steps
{

    [Binding]
    public sealed class RootStepDefinitions
    {

        private BrowserDriver _browserDriver;
        private DataDriver _dataDriver;
        private readonly RootPageObject _rootPageObject;

        public RootStepDefinitions(BrowserDriver browserDriver, DataDriver dataDriver)
        {
            _browserDriver = browserDriver;
            _dataDriver = dataDriver;
            _rootPageObject = new RootPageObject(browserDriver.Current);
        }

        [Given("I sign out")]
        public void GivenISignOut()
        {
            _rootPageObject.ClickSignOut();
        }

        [Given("I have opened the admin site")]
        public void GivenIHaveOpenedSite()
        {
            _rootPageObject.Open();
        }

        [Given("my user is granted admin rights")]
        public void GivenMyUserHasAdminRights()
        {
            _dataDriver.GrantUserAdmin("testy.mctestface@nhs.net");
        }

        [Then("an access restriction message should be shown")]
        public void ThenAnAccessRestrictionMessageIsShown()
        {
            Assert.True(_rootPageObject.IsAccessRestrictionMessageVisible());
        }

        [Given("the Sign In element is shown")]
        public void GivenSignInIsDisplayed()
        {
            Assert.True(_rootPageObject.IsSignInElementVisible());
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
