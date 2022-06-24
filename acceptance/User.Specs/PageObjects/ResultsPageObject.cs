using System.Collections.ObjectModel;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class ResultsPageObject : BasePageObject
    {
        private const string PATH = "/Search/Results";
        private readonly IWebDriver _webDriver;

        public ResultsPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public void Open(string searchText)
        {
            _webDriver.Url = URL(PATH) + $"?mode=Name&query={searchText}";
        }

        private ReadOnlyCollection<IWebElement> Results =>
            _webDriver.FindElements(By.ClassName("nhsuk-summary-list__row"));

        public int ResultCount()
        {
            return Results.Count;
        }

        public bool ResultsContain(string text)
        {
            return Results.Any(
                element =>
                    element.FindElement(
                        By.XPath($"//*[contains(text(), '{text}')]")
                    ) != null
                );
        }

        public void ClickResult(int index)
        {
            Results[index - 1].Click();
        }

        private IWebElement ResultsHeader =>
            _webDriver.FindElement(By.CssSelector("h3.nhsuk-card__heading"));

        public string ResultsHeaderText
        {
            get
            {
                return ResultsHeader.Text;
            }
        }


    }
}
