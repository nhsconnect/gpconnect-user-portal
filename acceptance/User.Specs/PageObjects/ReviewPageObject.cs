using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class ReviewPageObject : BasePageObject
    {
        private const string PATH = "/Apply/Review";
        private readonly IWebDriver _webDriver;

        public ReviewPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        private IWebElement nextDD(string dtText)
        {
            return _webDriver.FindElement(
                By.XPath($"//dt[contains(text(), '{dtText}')]/following::dd[1]")
            );
        }


        public IWebElement SupplierName => nextDD("System name:");

        public IWebElement ConnectProduct => nextDD("GP Connect product(s):");

        public IWebElement OdsCode => nextDD("Site ODS code:");

        public IWebElement OrganisationName => nextDD("Organisation name:");
        public IWebElement OrganisationAddress => nextDD("Organisation address:");
        public IWebElement SignatoryName => nextDD("Signatory name:");
        public IWebElement SignatoryRole => nextDD("Signatory title:");
        public IWebElement SignatoryEmail => nextDD("Signatory email address:");

        public IWebElement UseCase =>
            _webDriver.FindElement(By.XPath("//h2[contains(text(), 'Reason for applying')]/following::dl[1]/div/dd"));

    }
}

