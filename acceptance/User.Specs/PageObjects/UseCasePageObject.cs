using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class UseCasePageObject : BasePageObject
    {
        private const string PATH = "/Apply/UseCase";
        private readonly IWebDriver _webDriver;

        public UseCasePageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        public bool IsPageVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => driver.Url.StartsWith(URL(PATH)));
        }

        public IWebElement UseCaseDescription =>
            _webDriver.FindElement(By.Id("input-usecasedescription"));
    }
}

