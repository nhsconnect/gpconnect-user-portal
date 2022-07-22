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
        private readonly IOptionsSnapshot<ConnectionStrings> _optionsAccessor;

        public DataService(IOptionsSnapshot<ConnectionStrings> optionsAccessor, ILogger<DataService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException();
            _optionsAccessor = optionsAccessor;
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
            return new NpgsqlConnection(_optionsAccessor.Value.DefaultConnection);
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

        private readonly static TimeSpan EXPIRY = TimeSpan.FromSeconds(900);
        private long _currentExpiryTime = 0;

        private string _currentToken = "";


        public string GenerateAuthToken(RegionEndpoint endpoint, string host, int port, string user)
        {
            if ( DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > _currentExpiryTime) {
                _currentExpiryTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() + (long)EXPIRY.TotalMilliseconds;
                _currentToken = Amazon.RDS.Util.RDSAuthTokenGenerator.GenerateAuthToken(RegionEndpoint.EUWest2, host, 5432, user);
            }
            return _currentToken;
        }
    }
}
