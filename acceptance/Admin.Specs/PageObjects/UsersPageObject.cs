
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{
    public class UsersPageObject
    {

        private const string UsersUrl = "https://localhost:5001/Users";

        private readonly IWebDriver _webDriver;

        private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);

        public UsersPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement UsersHeaderElement => _webDriver.FindElement(By.XPath("//h3[contains(text(), 'Users (')]"));


        public void Open()
        {
            if (_webDriver.Url != UsersUrl)
            {
                _webDriver.Url = UsersUrl;
            }
        }


        public bool IsUsersHeaderVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => UsersHeaderElement.Displayed);
        }
    }
}
