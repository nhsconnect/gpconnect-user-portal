using TechTalk.SpecFlow;

using Npgsql;

using GpConnect.DataSharing.Admin.Specs.Drivers;

namespace GpConnect.DataSharing.Admin.Specs.Hooks
{

    [Binding]
    public class AdminHooks
    {
        [AfterScenario]
        public static async void DeleteUsers(BrowserDriver browserDriver)
        {
            await using var connection = new NpgsqlConnection(
                "Host=localhost;Database=postgres;Username=postgres;Include Error Detail=true"
            );
            await connection.OpenAsync();

            await using
            (
                var cmd = new NpgsqlCommand(
                    "TRUNCATE TABLE application.user;",
                    connection
                )
            ) {
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
