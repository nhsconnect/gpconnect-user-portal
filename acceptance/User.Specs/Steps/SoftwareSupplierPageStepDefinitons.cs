using Xunit;
using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class SoftwareSupplierPageStepDefinitions
    {
        private readonly SoftwareSupplierPageObject _softwareSupplierPage;

        public SoftwareSupplierPageStepDefinitions(BrowserDriver browserDriver)
        {
            _softwareSupplierPage = new SoftwareSupplierPageObject(browserDriver.Current);
        }

        [When(@"I select ""(.*)"" in the supplier list")]
        public void WhenISelectInTheSupplierList(string supplier)
        {
            _softwareSupplierPage.SelectSupplier(supplier);
        }

        [Then(@"the GP Connect product selection panel is shown")]
        public void ThenTheGpConnectProductPanelIsShown()
        {
            Assert.True(_softwareSupplierPage.IsGpConnectProductPanelVisible());
        }

        [Then(@"the ""(.*)"" label is shown")]
        public void ThenTheNamedLabelIsShown(string labelText)
        {
            Assert.True(_softwareSupplierPage.IsLabelVisible(labelText));
        }
    }
}
