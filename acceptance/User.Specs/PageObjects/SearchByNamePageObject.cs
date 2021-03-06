using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class SearchByNamePageObject : BasePageObject
    {
        private const string PATH = "/Search/Name";
        private readonly IWebDriver _webDriver;

        public SearchByNamePageObject(IWebDriver webDriver)
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

        private IWebElement ValidationError => _webDriver.FindElement(By.ClassName("field-validation-error"));

        public bool IsValidationErrorVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => ValidationError.Displayed);
        }

        public IWebElement OdsSearchLink => _webDriver.FindElement(By.PartialLinkText("ODS code"));
    }
}
