using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class BasePageObject
    {

        private string baseUrl;
        protected TimeSpan DefaultWait = TimeSpan.FromSeconds(1);

        private readonly IWebDriver _webDriver;

        public BasePageObject()
        {
            baseUrl = Environment.GetEnvironmentVariable("BASE_USER_URL") ?? "https://localhost:5003";
        }

        public BasePageObject(IWebDriver webDriver) : this()
        {
            _webDriver = webDriver;
        }

        protected string URL(string path)
        {
            return baseUrl + path;
        }

        public void ClickLink(string name)
        {
            _webDriver.FindElement(By.LinkText(name)).Click();
        }
        public void ClickButton(string name)
        {
            _webDriver.FindElement(
                By.XPath($"//button[contains(text(), '{name}')]")
            ).Click();
        }
        public bool IsButtonVisible(string labelText)
        {
            return _webDriver.FindElement(
                By.XPath($"//button[contains(text(), '{labelText}')]")
            ).Displayed;
        }

        public bool IsLabelVisible(string labelText)
        {
            return _webDriver.FindElement(
                By.XPath($"//label[contains(text(), '{labelText}')]")
            ).Displayed;
        }

        public bool IsThereCookies()
        {
            return _webDriver.Manage().Cookies.AllCookies.Count > 0;
        }
    }
}
