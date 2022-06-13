using TechTalk.SpecFlow;

namespace MyNamespace
{
    [Binding]
    public class SearchByOdsCodeStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public SearchByOdsCodeStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }
        [When(@"I enter a valid ODS code into the search box")]
        public void WhenIEnterAValidODSCodeIntoTheSearchBox()
        {
            _scenarioContext.Pending();
        }
    }
}
