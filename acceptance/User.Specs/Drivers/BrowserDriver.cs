
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace GpConnect.DataSharing.User.Specs.Drivers
{

    public class BrowserDriver : IDisposable
    {
        private readonly Lazy<IWebDriver> _currentWebDriverLazy;
        private bool _isDisposed;

        public BrowserDriver()
        {
            _currentWebDriverLazy = new Lazy<IWebDriver>(CreateWebDriver);
        }

        public IWebDriver Current => _currentWebDriverLazy.Value;

        private IWebDriver CreateWebDriver()
        {

            var chromeOptions = new ChromeOptions();
            chromeOptions.AddArgument("ignore-certificate-errors");

            var remoteUrl = Environment.GetEnvironmentVariable("REMOTE_BROWSER_URL");
            if (remoteUrl == null) {
                var chromeDriverService = ChromeDriverService.CreateDefaultService();
                var chromeDriver = new ChromeDriver(
                    chromeDriverService,
                    chromeOptions
                );
                return chromeDriver;
            } else {
                var remoteDriver = new RemoteWebDriver(new Uri(remoteUrl), chromeOptions);
                return remoteDriver;
            }
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            if (_currentWebDriverLazy.IsValueCreated)
            {
                Current.Quit();
            }

            _isDisposed = true;
        }

    }
}
