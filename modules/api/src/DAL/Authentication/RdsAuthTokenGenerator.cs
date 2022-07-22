using System;
using Amazon;
using GpConnect.NationalDataSharingPortal.Api.Dal.Authentication.Interface;
using Microsoft.Extensions.Logging;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Authentication
{
    public class RdsAuthTokenGenerator : IAuthTokenGenerator
    {
        private readonly static TimeSpan EXPIRY = TimeSpan.FromSeconds(900);
        
        private ILogger<RdsAuthTokenGenerator> _logger;


        private long _currentExpiryTime = 0;
        private string _currentToken = "";
       
        public RdsAuthTokenGenerator(ILogger<RdsAuthTokenGenerator> logger) 
        {
            _logger = logger;
        }

        public string GenerateAuthToken(string host, int port, string user)
        {
            if ( DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() >= _currentExpiryTime - 1000) {
                try 
                {
                    _logger.LogDebug("Generating RDS token from Amazon");
                    _currentToken = Amazon.RDS.Util.RDSAuthTokenGenerator.GenerateAuthToken(RegionEndpoint.EUWest2, host, 5432, user);
                    // Only update time if we get a token
                    _currentExpiryTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (long)EXPIRY.TotalMilliseconds;
                }
                catch (Exception e) 
                {
                    _logger.LogError( e, "Failed to obtain RDS token from AWS");
                    throw;
                }
            }
            return _currentToken;
        }
    }
}