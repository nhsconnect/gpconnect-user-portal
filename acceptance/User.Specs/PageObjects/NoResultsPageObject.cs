using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class NoResultsPageObject : BasePageObject
    {
        private const string PATH = "/Search/NoResults";
        private readonly IWebDriver _webDriver;

        public NoResultsPageObject(IWebDriver webDriver)
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

        public IWebElement ByNameSearchLink =>
            _webDriver.FindElement(By.LinkText("Organisation Name"));

        public IWebElement OdsCodeSearchLink =>
            _webDriver.FindElement(By.LinkText("Organisation ODS Code"));

        public bool HasNoResultsBanner(string content)
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(
                driver => driver.FindElement(
                    By.XPath(
                        $"//h3[contains(text(), '{content}')]"
                    )
                ).Displayed
            );
        }
    }
}
