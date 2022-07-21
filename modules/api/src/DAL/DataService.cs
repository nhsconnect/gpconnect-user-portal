using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Amazon;

namespace GpConnect.NationalDataSharingPortal.Api.Dal
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly string _connectionString;
        private readonly IAuthTokenGenerator _authTokenGenerator;

        public DataService(IOptions<ConnectionStrings> optionsAccessor, ILogger<DataService> logger, IAuthTokenGenerator authTokenGenerator = null)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _connectionString = optionsAccessor == null ? throw new ArgumentNullException() : optionsAccessor.Value.DefaultConnection;
            _authTokenGenerator = authTokenGenerator ?? new RDSAuthTokenGenerator();
        }

        public async Task<List<T>> ExecuteQuery<T>(string query, DynamicParameters? parameters = null) where T : class
        {
            try
            {
                CheckQuery(query);
                await using var connection = GetConnection();
                var results = (await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure)).AsList();
                return results;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public async Task<T> ExecuteQueryFirstOrDefault<T>(string query, DynamicParameters? parameters = null) where T : class
        {
            try
            {
                CheckQuery(query);
                await using var connection = GetConnection();
                var result = await connection.QueryFirstOrDefaultAsync<T>(query, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public async Task<int> ExecuteQuery(string query, DynamicParameters parameters)
        {
            try
            {
                CheckQuery(query);
                await using var connection = GetConnection();
                var rowsProcessed = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return rowsProcessed;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public async Task<int> ExecuteScalar(string query, DynamicParameters parameters)
        {
            try
            {
                CheckQuery(query);
                await using var connection = GetConnection();
                var singleValue = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                return singleValue;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public async Task<List<T>> ExecuteTextQuery<T>(string query) where T : class
        {
            try
            {
                CheckQuery(query);
                await using var connection = GetConnection();
                var results = (await connection.QueryAsync<T>(query, commandType: CommandType.Text)).AsList();
                return results;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        private NpgsqlConnection GetConnection() {
            return new NpgsqlConnection(ConnectionString);
        }

        public string ConnectionString
        {
            get
            {
                var connectionString = _connectionString;
                if (connectionString.Contains("${rdsToken}")) {
                    var host = Array.Find(connectionString.Split(';'), item => item.StartsWith("Host=")).Split('=')[1];
                    var user = Array.Find(connectionString.Split(';'), item => item.StartsWith("User=")).Split('=')[1];
                    var pwd = _authTokenGenerator.GenerateAuthToken(RegionEndpoint.EUWest2, host, 5432, user);
                    connectionString = connectionString.Replace("${rdsToken}", pwd);
                }
                return connectionString;
            }
        }

        private string CheckQuery(string query)
        {
            if (string.IsNullOrEmpty(query?.Trim()))
                throw new ArgumentNullException(nameof(query));
            return query;
        }
    }

    public interface IAuthTokenGenerator
    {
        string GenerateAuthToken(RegionEndpoint endpoint, string host, int port, string user);
    }

    public class RDSAuthTokenGenerator : IAuthTokenGenerator
    {
        public string GenerateAuthToken(RegionEndpoint endpoint, string host, int port, string user)
        {
            return Amazon.RDS.Util.RDSAuthTokenGenerator.GenerateAuthToken(RegionEndpoint.EUWest2, host, 5432, user);
        }
    }
}
