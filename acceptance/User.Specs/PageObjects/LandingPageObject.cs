
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class LandingPageObject
    {
        private const string URL = "https://localhost:5003/";
        private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);

        private readonly IWebDriver _webDriver;

        public LandingPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Open()
        {
            if (_webDriver.Url != URL)
            {
                _webDriver.Url = URL;
            }
        }

        private IWebElement SearchLink =>
            _webDriver.FindElement(By.PartialLinkText("Find"));

        public bool IsSearchLinkVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => SearchLink.Displayed);
        }

        public void ClickSearchLink()
        {
            SearchLink.Click();
        }


    }
}
