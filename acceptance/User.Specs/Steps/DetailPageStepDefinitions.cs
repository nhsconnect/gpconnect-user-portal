using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class DetailPageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly DetailPageObject _detailPage;

        public DetailPageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _detailPage = new DetailPageObject(browserDriver.Current);
        }

        [Given(@"I have opened a detail page")]
        public void GivenIHaveOpenedADetailPage()
        {
            _detailPage.Open("id=011c9fb1-827b-4f0c-8fb3-72575a0108d8");
        }

        [Then(@"I am taken to the detail page")]
        public void ThenIAmTakenToTheDetailPage()
        {
            Assert.True(_detailPage.IsPageVisible());
        }

        [Then(@"""(.*)"" is the site name")]
        public void ThenIsTheSiteName(string siteName)
        {
            Assert.Equal(siteName, _detailPage.SiteName.Text);
        }

        [Then(@"a list of services used is shown")]
        public void ThenAListOfServicesUsedIsShown()
        {
            Assert.True(_detailPage.ServiceList.Displayed);
        }
    }
}
