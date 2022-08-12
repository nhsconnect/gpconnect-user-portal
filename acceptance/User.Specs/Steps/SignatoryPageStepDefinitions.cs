using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class SignatoryPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly SignatoryPageObject _signatoryPage;

        public SignatoryPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _signatoryPage = new SignatoryPageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the signatory page")]
        public void ThenIAmTakenToTheSignatoryPage()
        {
            Assert.True(_signatoryPage.IsPageVisible());
        }

        [When(@"I enter ""(.*)"" into the signatory name field")]
        public void WhenIEnterIntoTheSignatoryNameField(string name)
        {
            _signatoryPage.SignatoryName.SendKeys(name);
        }

        [When(@"I enter ""(.*)"" into the signatory role field")]
        public void WhenIEnterIntoTheSignatoryRoleField(string role)
        {
            _signatoryPage.SignatoryRole.SendKeys(role);
        }

        [When(@"I enter ""(.*)"" into the signatory email field")]
        public void WhenIEnterIntoTheSignatoryEmailField(string email)
        {
            _signatoryPage.SignatoryEmail.SendKeys(email);
        }
    }
}
