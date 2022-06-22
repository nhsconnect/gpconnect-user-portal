
using Npgsql;

namespace GpConnect.DataSharing.Admin.Specs.Drivers
{
    public class DataDriver : IDisposable
    {

        private string CONNECTION_STRING;

        public DataDriver()
        {
            var DB_HOST = Environment.GetEnvironmentVariable("DB_HOST") ?? "localhost";
            CONNECTION_STRING = $"Host={DB_HOST};Database=postgres;Username=postgres;Include Error Detail=true";
        }

        public void TruncateUsers()
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            using
                var cmd = new NpgsqlCommand(
                    "TRUNCATE TABLE application.user;",
                    connection
                );
                cmd.ExecuteNonQuery();
        }

        public void GrantUserAdmin(string userEmail)
        {
            using var connection = new NpgsqlConnection(CONNECTION_STRING);
            connection.Open();

            using
                var cmd = new NpgsqlCommand(
                    @"UPDATE application.user
                        SET is_admin = TRUE, authorised_date = NOW()
                        WHERE email_address = @userEmail;",
                    connection
                ){
                    Parameters = {
                        new("userEmail", userEmail)
                    }
                };
                cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {

        }
    }
}
