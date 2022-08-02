using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class DetailPageObject : BasePageObject
    {
        private const string PATH = "/Search/Detail";
        private readonly IWebDriver _webDriver;

        public DetailPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public void Open(string queryParams = "")
        {
            _webDriver.Url = URL($"{PATH}?{queryParams}");
        }

        public IWebElement SiteName =>
            _webDriver
                .FindElement(By.ClassName("nhsuk-card__content"))
                .FindElement(By.CssSelector("label.nhsuk-label"));        
    }
}


