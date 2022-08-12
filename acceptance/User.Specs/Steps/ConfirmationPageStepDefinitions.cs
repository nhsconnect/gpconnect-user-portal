using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class ConfirmationPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly ConfirmationPageObject _confirmationPage;

        public ConfirmationPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _confirmationPage = new ConfirmationPageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the confirmation page")]
        public void ThenIAmTakenToTheConfirmationPage()
        {
            Assert.True(_confirmationPage.IsPageVisible());
        }

    }
}

