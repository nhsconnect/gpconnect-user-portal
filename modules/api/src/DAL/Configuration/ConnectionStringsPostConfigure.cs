using System;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Amazon;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Configuration
{
    public class ConnectionStringsPostConfiguration : IPostConfigureOptions<ConnectionStrings>
    {
        private readonly IAuthTokenGenerator _tokenGenerator;
        private readonly ILogger<ConnectionStringsPostConfiguration> _logger;

        public ConnectionStringsPostConfiguration(IAuthTokenGenerator tokenGenerator, ILogger<ConnectionStringsPostConfiguration> logger)
        {
            _tokenGenerator = tokenGenerator;
            _logger = logger;
        }

        public void PostConfigure(string name, ConnectionStrings options)
        {
            _logger.LogInformation("Calling PostConfigure");
            var connectionString = options.DefaultConnection;
            if (connectionString.Contains("${rdsToken}")) {
                _logger.LogInformation("Replacing Token");
                var host = GetNamedParameterValue(connectionString, "Host");
                var user = GetNamedParameterValue(connectionString, "User");
                var pwd = _tokenGenerator.GenerateAuthToken(RegionEndpoint.EUWest2, host, 5432, user);


                options.DefaultConnection = connectionString.Replace("${rdsToken}", pwd);
            }
        }

        private string GetNamedParameterValue(string connectionString, string parameterName)
        {
            return Array.Find(connectionString.Split(';'), item => item.StartsWith($"{parameterName}=")).Split('=')[1];
        }
    }
}
