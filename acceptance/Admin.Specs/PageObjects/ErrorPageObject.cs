using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Admin.Specs.PageObjects
{
    public class ErrorPageObject
    {

        private const string ErrorUrl = "https://localhost:5001/Error";

        private readonly IWebDriver _webDriver;

        private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);

        public ErrorPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }


        public bool IsOpen()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url == ErrorUrl);
        }

    }
}
