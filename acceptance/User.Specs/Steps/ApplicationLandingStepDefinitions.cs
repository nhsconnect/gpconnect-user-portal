using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{

    [Binding]
    public sealed class ApplicationLandingStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;
        private readonly ApplicationLandingPageObject _applicationLandingPage;
        private readonly SoftwareSupplierPageObject _softwareSupplierPage;

        public ApplicationLandingStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _applicationLandingPage = new ApplicationLandingPageObject(browserDriver.Current);
            _softwareSupplierPage = new SoftwareSupplierPageObject(browserDriver.Current);
        }

        [Given(@"I have opened the application landing page")]
        public void GivenIHaveOpenedTheApplicationLandingPage()
        {
            _applicationLandingPage.Open();
        }

        [Then(@"A button to start the application is present")]
        public void ThenAButtonToStartTheApplicationSearchIsPresent()
        {
            Assert.True(_applicationLandingPage.IsStartButtonVisible());
        }

        // [Then(@"the support email address is present")]
        // public void ThenTheAdministratorsEmailAddressIsPresent()
        // {
        //     Assert.True(_applicationLandingPage.SupportEmail.Displayed);
        // }

        // [Then(@"the support telephone number is present")]
        // public void ThenTheSupportTelephoneNumberIsPresent()
        // {
        //     Assert.True(_applicationLandingPage.SupportPhone.Displayed);
        // }

        // [When(@"I click the start now button")]
        // public void WhenIClickTheStartNowButton()
        // {
        //     _applicationLandingPage.
        // }

        [Then(@"I am taken to the software supplier page")]
        public void ThenIAmTakenToTheSoftwareSupplierPage()
        {
            Assert.True(_softwareSupplierPage.IsPageVisible());
        }
    }
}
