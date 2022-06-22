
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{
    public class UsersPageObject : BasePageObject
    {

        private const string PATH = "/Users";

        private readonly IWebDriver _webDriver;

        public UsersPageObject(IWebDriver webDriver)
        {
            _webDriver = webDriver;
        }

        private IWebElement UsersHeaderElement => _webDriver.FindElement(By.XPath("//h3[contains(text(), 'Users (')]"));


        public void Open()
        {
            if (_webDriver.Url != URL(PATH))
            {
                _webDriver.Url = URL(PATH);
            }
        }


        public bool IsUsersHeaderVisible()
        {
            var wait = new WebDriverWait(_webDriver, DefaultWait);
            return wait.Until(driver => UsersHeaderElement.Displayed);
        }
    }
}
