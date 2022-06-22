
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class LandingPageObject : BasePageObject
    {
        private const string PATH = "/";
        private readonly IWebDriver _webDriver;

        public LandingPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Open()
        {
            if (_webDriver.Url != URL(PATH))
            {
                _webDriver.Url = URL(PATH);
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
