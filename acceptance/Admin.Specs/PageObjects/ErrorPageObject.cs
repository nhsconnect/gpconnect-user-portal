using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{
    public class ErrorPageObject : BasePageObject
    {

        private const string PATH = "/Error";

        private readonly IWebDriver _webDriver;

        public ErrorPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }


        public bool IsOpen()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url == URL(PATH));
        }

    }
}
