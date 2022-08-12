using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class AgreementPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly AgreementPageObject _agreementPage;

        public AgreementPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _agreementPage = new AgreementPageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the agreement page")]
        public void ThenIAmTakenToTheAgreementPage()
        {
            Assert.True(_agreementPage.IsPageVisible());
        }

    }
}
