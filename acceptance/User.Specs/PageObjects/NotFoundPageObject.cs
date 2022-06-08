
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace User.Specs.PageObjects
{
    public class NotFoundPageObject
    {

        private const string NotFoundPageUrl = "https://localhost:5003/NotFound";

        private readonly IWebDriver _webDriver;
        private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);


        public NotFoundPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void NavigateToNonExistentPage()
        {
            _webDriver.Url = "https://localhost:5003/ThisPageDoesNotExist";
        }

        public bool IsNotFoundPageViewed()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url == NotFoundPageUrl);
        }

    }
}
