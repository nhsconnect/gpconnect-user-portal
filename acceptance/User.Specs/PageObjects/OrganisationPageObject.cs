using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class OrganisationPageObject : BasePageObject
    {
        private const string PATH = "/Apply/Organisation";
        private readonly IWebDriver _webDriver;

        public OrganisationPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public void EnterOdsCode(string code)
        {
            _webDriver.FindElement(By.Id("input-siteodscode")).SendKeys(code);
        }

        public IWebElement OrganisationName =>
            _webDriver.FindElement(
                By.XPath("//dt[contains(text(), 'Organisation name')]/following::dd[1]")
            );

        public IWebElement OrganisationAddress =>
            _webDriver.FindElement(
                By.XPath("//dt[contains(text(), 'Organisation address')]/following::dd[1]")
            );
    }
}
