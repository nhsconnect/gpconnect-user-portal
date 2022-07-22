using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace GpConnect.DataSharing.User.Specs.PageObjects
{
    public class BasePageObject
    {
        private readonly IWebDriver _webDriver;

        private readonly string baseUrl;
        protected readonly TimeSpan DefaultWait;

        public BasePageObject()
        {
            DefaultWait = TimeSpan.FromSeconds(double.Parse(Environment.GetEnvironmentVariable("DEFAULT_TIMEOUT") ?? "1"));
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
