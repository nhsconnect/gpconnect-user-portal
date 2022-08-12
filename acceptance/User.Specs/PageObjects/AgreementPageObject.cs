using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class AgreementPageObject : BasePageObject
    {
        private const string PATH = "/Apply/Agreement";
        private readonly IWebDriver _webDriver;

        public AgreementPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

    }
}
