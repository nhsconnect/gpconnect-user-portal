using TechTalk.SpecFlow;

using OpenQA.Selenium;

using GpConnect.DataSharing.User.Specs.Drivers;

namespace GpConnect.DataSharing.User.Specs.Hooks
{

    [Binding]
    public class ScreenshotHook
    {

        ScenarioContext _context;
        IWebDriver _browser;

        public ScreenshotHook(ScenarioContext context, BrowserDriver browserDriver)
        {
            _context = context;
            _browser = browserDriver.Current;
        }

        [AfterScenario]
        public void CleanUp()
        {
            try
            {
                if (_context.TestError != null)
                {
                    Screenshot ss = ((ITakesScreenshot)_browser).GetScreenshot();
                    string title = _context.ScenarioInfo.Title;
                    string Runname = title + DateTime.Now.ToString("yyyy-MM-dd-HH_mm_ss.jpg");

                    string reportFolder = "./reports";
                    var d = Directory.CreateDirectory(reportFolder);
                    ss.SaveAsFile("./reports/" + Runname, ScreenshotImageFormat.Jpeg);
                }
            }
            finally
            {
                if (_browser != null)
                {
                    _browser.Quit();
                    _browser = null;
                }
            }
        }
    }
}

