using Xunit;

using TechTalk.SpecFlow;

using GpConnect.DataSharing.User.Specs.Drivers;
using GpConnect.DataSharing.User.Specs.PageObjects;

namespace GpConnect.DataSharing.User.Specs.Steps
{
    [Binding]
    public class UseCasePageStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly UseCasePageObject _useCasePage;

        public UseCasePageStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _useCasePage = new UseCasePageObject(browserDriver.Current);
        }

        [Then(@"I am taken to the use case page")]
        public void ThenIAmTakenToTheUseCasePage()
        {
            Assert.True(_useCasePage.IsPageVisible());
        }

        [When(@"I enter ""(.*)"" into the use case description field")]
        public void WhenIEnterIntoTheSignatoryEmailField(string useCase)
        {
            _useCasePage.UseCaseDescription.SendKeys(useCase);
        }

    }
}

