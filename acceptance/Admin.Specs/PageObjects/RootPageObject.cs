using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace GpConnect.DataSharing.Admin.Specs.PageObjects
{

  public class RootPageObject
  {
    private const string AdminUrl = "https://localhost:5001/";

    private readonly IWebDriver _webDriver;

    private TimeSpan DefaultWait = TimeSpan.FromSeconds(5);

    public RootPageObject(IWebDriver webDriver)
    {
      _webDriver = webDriver;
    }

    private IWebElement SignInElement => _webDriver.FindElement(By.LinkText("Sign in"));
    private IWebElement UserNameElement => _webDriver.FindElement(
        By.XPath("//div[contains(text(), 'testy.mctestface@nhs.net')]")
    );
    private IWebElement EndpointChangesHeaderElement => _webDriver.FindElement(
        By.XPath("//h2[contains(text(), 'Endpoint')]")
    );
    private IWebElement AccessRestrictionMessageElement => _webDriver.FindElement(
        By.XPath("//p[contains(text(), 'application is restricted to registered users')]")
    );

    public void Open()
    {
        if (_webDriver.Url != AdminUrl)
        {
            _webDriver.Url = AdminUrl;
        }
    }

    public bool IsSignInElementVisible()
    {
        var wait = new WebDriverWait(_webDriver, DefaultWait);
        return wait.Until(driver => SignInElement.Displayed);
    }

    public void ClickSignIn()
    {
      SignInElement.Click();
    }

    public bool IsEndpointHeaderVisible()
    {
      var wait = new WebDriverWait(_webDriver, DefaultWait);
      return wait.Until(driver => EndpointChangesHeaderElement.Displayed);
    }

    public bool IsAccessRestrictionMessageVisible()
    {
        var wait = new WebDriverWait(_webDriver, DefaultWait);
        return wait.Until(driver => AccessRestrictionMessageElement.Displayed);
    }

  }
}
