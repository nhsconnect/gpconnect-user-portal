using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class SearchByNamePageObject
    {
        private const string URL = "https://localhost:5003/Search/Start";
        private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);

        private readonly IWebDriver _webDriver;

        public SearchByNamePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url == URL);
        }

        public void Open()
        {
            _webDriver.Url = URL;
        }

    }
}
