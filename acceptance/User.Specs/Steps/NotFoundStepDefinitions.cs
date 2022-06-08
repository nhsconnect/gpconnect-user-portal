using Xunit;
using TechTalk.SpecFlow;

using User.Specs.Drivers;
using User.Specs.PageObjects;

namespace User.Specs.Steps
{

    [Binding]
    public sealed class NotFoundStepDefinitions
    {
        private readonly NotFoundPageObject _notFoundPageObject;

        public NotFoundStepDefinitions(BrowserDriver browserDriver)
        {
            _notFoundPageObject = new NotFoundPageObject(browserDriver.Current);
        }

        [Given("I navigate to a page that is not there")]
        public void GivenINavigateToAPageThatIsNotThere()
        {
            _notFoundPageObject.NavigateToNonExistentPage();
        }

        [Then("The standard 404 page is shown")]
        public void ThenTheStandard404PageIsShown()
        {
            Assert.True(_notFoundPageObject.IsNotFoundPageViewed());
        }

    }
}
