using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class BasePageStepDefinitions
    {
        private readonly BasePageObject _basePage;

        public BasePageStepDefinitions(BrowserDriver browserDriver)
        {
            _basePage = new BasePageObject(browserDriver.Current);
        }

        [When(@"I click the ""(.*)"" button")]
        public void WhenIClickTheNamedButton(string name)
        {
            _basePage.ClickButton(name);
        }

        [When(@"I click the ""(.*)"" link")]
        public void WhenIClickTheNamedLink(string name)
        {
            _basePage.ClickLink(name);
        }

        [Then("there are cookies")]
        public void ThenThereAreCookies()
        {
            _basePage.IsThereCookies();
        }

        [When(@"I check the ""(.*)"" box")]
        public void WhenICheckTheLabelledBox(string label) {
            _basePage.CheckBox(label);
        }

    }
}
