using Xunit;
using TechTalk.SpecFlow;

using Npgsql;

using Admin.Specs.Drivers;
using Admin.Specs.PageObjects;

namespace Admin.Specs.Steps
{

    [Binding]
    public sealed class RootStepDefinitions
    {

        private BrowserDriver _browserDriver;
        private readonly RootPageObject _rootPageObject;

        public RootStepDefinitions(BrowserDriver browserDriver)
        {
            _browserDriver = browserDriver;
            _rootPageObject = new RootPageObject(browserDriver.Current);
        }

        [Given("I have opened the admin site")]
        public void GivenIHaveOpenedSite()
        {
            _rootPageObject.Open();
        }

        [Given("my user has admin rights")]
        public async void GivenMyUserHasAdminRights()
        {
            await using var connection = new NpgsqlConnection(
                "Host=localhost;Database=postgres;Username=postgres;Include Error Detail=true"
            );
            await connection.OpenAsync();

            await using
            (
                var cmd = new NpgsqlCommand(
                    @"UPDATE application.user
                        SET is_admin = TRUE, authorised_date = NOW()
                        WHERE email_address = 'testy.mctestface@nhs.net';",
                    connection
                )
            ) {
                await cmd.ExecuteNonQueryAsync();
            }
        }

        [Then("an access restriction message should be shown")]
        public void ThenAnAccessRestrictionMessageIsShown()
        {
            Assert.True(_rootPageObject.IsAccessRestrictionMessageVisible());
        }

        [Then("the Sign In element is shown")]
        public void ThenSignInIsDisplayed()
        {
            Assert.True(_rootPageObject.IsSignInElementVisible());
        }

        [Given("I sign in")]
        public void GivenISignIn()
        {
            _rootPageObject.ClickSignIn();
        }

        [Then("the endpoints header should be displayed")]
        public void ThenEndpointHeaderIsDisplayed()
        {
            Assert.True(_rootPageObject.IsEndpointHeaderVisible());
        }
    }
}
