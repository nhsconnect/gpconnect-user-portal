
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class SearchPageObject : BasePageObject
    {
        private const string PATH = "/Search";
        private readonly IWebDriver _webDriver;

        public SearchPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public void Open()
        {
            if (_webDriver.Url != URL(PATH))
            {
                _webDriver.Url = URL(PATH);
            }
        }

        private IWebElement OdsCodeInput =>
            _webDriver.FindElement(By.XPath("//input[@name='ProviderOdsCode']"));
        private IWebElement ProviderNameInput =>
            _webDriver.FindElement(By.XPath("//input[@name='ProviderName']"));

        private IWebElement SearchButton =>
            _webDriver.FindElement(By.XPath("//button[contains(text(), 'Search')]"));
        private IWebElement ClearButton =>
            _webDriver.FindElement(By.XPath("//button[contains(text(), 'Clear')]"));

        private IWebElement NoResultsHeader =>
            _webDriver.FindElement(By.XPath("//h3[contains(text(), 'No Results')]"));

        private IWebElement ErrorMessageSpan =>
            _webDriver.FindElement(By.CssSelector("span.nhsuk-error-message"));

        private IWebElement BackLink =>
            _webDriver.FindElement(By.LinkText("Back"));

        public IWebElement SearchInput =>
            _webDriver.FindElement(By.XPath("//input"));

        public void ClickSearchButton()
        {
            SearchButton.Click();
        }

        public void ClickBack()
        {
            BackLink.Click();
        }

        public bool IsErrorBannerVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => ErrorMessageSpan.Displayed);
        }

        public bool IsNoResultsVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => NoResultsHeader.Displayed);
        }

        public void EnterOdsCode(string input)
        {
            OdsCodeInput.SendKeys(input);
        }

        public void EnterProviderName(string input)
        {
            ProviderNameInput.SendKeys(input);
        }

        public IEnumerable<IWebElement> GetResults() {
            var results = _webDriver.FindElements(By.ClassName("nhsuk-card"));
            return results;
        }

    }
}
