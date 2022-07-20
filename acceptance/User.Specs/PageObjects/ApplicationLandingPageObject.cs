using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class ApplicationLandingPageObject : BasePageObject
    {
        private const string PATH = "/Apply";
        private readonly IWebDriver _webDriver;

        public ApplicationLandingPageObject(IWebDriver webDriver) : base(webDriver)
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

        public bool IsStartButtonVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => IsButtonVisible("Apply Now"));
        }

        public IWebElement SupportEmail =>
            _webDriver.FindElement(By.PartialLinkText("@nhs.net"));

        public IWebElement SupportPhone =>
            _webDriver.FindElement(By.PartialLinkText("020 "));
    }
}
