using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

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

        public async Task<List<T>> ExecuteQuery<T>(string query, DynamicParameters parameters = null) where T : class
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
                var results = (await connection.QueryAsync<T>(query, parameters, commandType: CommandType.StoredProcedure)).AsList();
                return results;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public async Task<T> ExecuteQueryFirstOrDefault<T>(string query, DynamicParameters parameters = null) where T : class
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
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
                using NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
                var rowsProcessed = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return rowsProcessed;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }

        public DataTable ExecuteQueryAndGetDataTable(string query, Dictionary<string, Guid> parameters)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
            connection.Open();
            using (var cmd = new NpgsqlCommand(query, connection))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, NpgsqlDbType.Uuid, parameter.Value);
                }

                cmd.Prepare();

                var da = new NpgsqlDataAdapter(cmd);
                var _ds = new DataSet();
                var _dt = new DataTable();

                da.Fill(_ds);

                try
                {
                    _dt = _ds.Tables[0];
                }
                catch (Exception exc)
                {
                    _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                    throw;
                }
                finally
                {
                    connection.Close();
                }
                return _dt;
            }
        }

        public async Task<List<T>> ExecuteSQLQuery<T>(string query) where T : class
        {
            try
            {
                using (var connection = new SqlConnection(_configuration.GetConnectionString(ConnectionStrings.GpConnect)))
                {
                    var command = new SqlCommand(query, connection);
                    connection.Open();
                    var results = (await connection.QueryAsync<T>(query, commandType: CommandType.Text)).AsList();
                    return results;
                }
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }
    }
}
