using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class SoftwareSupplierPageObject : BasePageObject
    {
        private const string PATH = "/Apply/SystemSupplier";
        private readonly IWebDriver _webDriver;

        public SoftwareSupplierPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public void Open()
        {
            _webDriver.Url = URL(PATH);
        }

        private IWebElement ProviderNameInput =>
            _webDriver.FindElement(By.XPath("//input[@name='ProviderName']"));

        public void EnterSearchText(string input)
        {
            ProviderNameInput.SendKeys(input);
        }

        private IWebElement FindButton =>
            _webDriver.FindElement(By.XPath("//button[contains(text(), 'Find')]"));

        public void ClickFind()
        {
            FindButton.Click();
        }

        private IWebElement SupplierList =>
            _webDriver.FindElement(By.Id("SelectedSoftwareSupplierNameId"));

        public void SelectSupplier(string supplierName)
        {
            new SelectElement(SupplierList).SelectByText(supplierName);
        }

        private IWebElement ProductInputHint =>
            _webDriver
                .FindElement(By.Id("input-hint-softwaresupplierproduct"));


        public Boolean IsGpConnectProductPanelVisible()
        {
            return ProductInputHint.Displayed;
        }

    }
}
