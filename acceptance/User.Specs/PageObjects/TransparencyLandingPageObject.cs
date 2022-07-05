using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class TransparencyLandingPageObject : LandingPageObject
    {
        private const string PATH = "/Search";
        private readonly IWebDriver _webDriver;

        public TransparencyLandingPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url == URL(PATH));
        }

        public void Open()
        {
            _webDriver.Url = URL(PATH);
        }

        private IWebElement StartButton =>
            _webDriver.FindElement(By.PartialLinkText("Start"));

        public bool IsStartButtonVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => StartButton.Displayed);
        }

        public void ClickStart(){
            StartButton.Click();
        }

    }
}
