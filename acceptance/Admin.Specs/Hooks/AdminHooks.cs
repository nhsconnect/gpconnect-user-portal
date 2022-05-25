using TechTalk.SpecFlow;

using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

namespace Admin.Specs.Hooks
{

    [Binding]
    public class AdminHooks
    {
        [BeforeScenario("Admin")]
        public static void BeforeScenario(BrowserDriver browserDriver)
        {
            var rootPageObject = new RootPageObject(browserDriver.Current);
            // TODO clear login cookies
        }
    }
}
