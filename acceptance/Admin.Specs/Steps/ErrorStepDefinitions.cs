using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

using Xunit;
using TechTalk.SpecFlow;

namespace Admin.Specs.Steps
{

    [Binding]
    public sealed class ErrorStepDefinitions
    {

        private readonly ErrorPageObject _errorPageObject;

        public ErrorStepDefinitions(BrowserDriver browserDriver)
        {
            _errorPageObject = new ErrorPageObject(browserDriver.Current);
        }

        [Then("the Error page is open")]
        public void ThenErrorPageIsOpen()
        {
            Assert.True(_errorPageObject.IsOpen());
        }

    }

}
