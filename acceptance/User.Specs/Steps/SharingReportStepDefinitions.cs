using TechTalk.SpecFlow;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class SharingReportStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public SharingReportStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [Given(@"I have opened a sharing report")]
        public void GivenIHaveOpenedASharingReport()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the name of the organization is shown")]
        public void ThenTheNameOfTheOrganizationIsShown()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the address of the organization is shown")]
        public void ThenTheAddressOfTheOrganizationIsShown()
        {
            _scenarioContext.Pending();
        }

        [Then(@"the postcode of the organization is shown")]
        public void ThenThePostcodeOfTheOrganizationIsShown()
        {
            _scenarioContext.Pending();
        }

        [Then(@"a list of services used is shown")]
        public void ThenAListOfServicesUsedIsShown()
        {
            _scenarioContext.Pending();
        }
    }
}
