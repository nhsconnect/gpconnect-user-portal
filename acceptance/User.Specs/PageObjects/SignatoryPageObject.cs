using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class SignatoryPageObject : BasePageObject
    {
        private const string PATH = "/Apply/Signatory";
        private readonly IWebDriver _webDriver;

        public SignatoryPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public IWebElement SignatoryName =>
            _webDriver.FindElement(By.Id("input-signatoryname"));

        public IWebElement SignatoryRole =>
            _webDriver.FindElement(By.Id("input-signatoryrole"));

        public IWebElement SignatoryEmail =>
            _webDriver.FindElement(By.Id("input-signatoryemail"));
    }
}
