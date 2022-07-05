using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class OdsSearchPageObject : BasePageObject
    {
        private const string PATH = "/Search/Code";
        private readonly IWebDriver _webDriver;

        public OdsSearchPageObject(IWebDriver webDriver)
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

        private IWebElement SearchBox =>
            _webDriver.FindElement(By.Name("ProviderOdsCode"));

        public void EnterSearchText(string input)
        {
            SearchBox.SendKeys(input);
        }

        private IWebElement FindButton =>
            _webDriver.FindElement(By.XPath("//button[contains(text(), 'Find')]"));

        public void ClickFind()
        {
            FindButton.Click();
        }

    }
}

