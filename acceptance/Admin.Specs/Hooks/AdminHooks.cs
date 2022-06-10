using TechTalk.SpecFlow;

using Npgsql;

using GpConnect.DataSharing.Admin.Specs.Drivers;

namespace GpConnect.DataSharing.Admin.Specs.Hooks
{

    [Binding]
    public class AdminHooks
    {
        [BeforeScenario]
        public static async void BeforeScenario(BrowserDriver browserDriver)
        {
            await using var connection = new NpgsqlConnection(
                "Host=localhost;Database=postgres;Username=postgres;Include Error Detail=true"
            );
            await connection.OpenAsync();

            await using
            (
                var cmd = new NpgsqlCommand(
                    @"UPDATE application.user
                        SET is_admin = FALSE, authorised_date = NULL
                        WHERE email_address = 'testy.mctestface@nhs.net';",
                    connection
                )
            ) {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
