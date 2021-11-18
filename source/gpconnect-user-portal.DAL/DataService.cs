using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace gpconnect_user_portal.DAL
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly IConfiguration _configuration;

        public DataService(IConfiguration configuration, ILogger<DataService> logger)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public List<T> ExecuteQuery<T>(string query, DynamicParameters parameters) where T : class
        {
            return null;
        }

        public int ExecuteQuery(string query, DynamicParameters parameters)
        {
            return 0;
        }
    }
}
