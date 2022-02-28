using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DAL.Resources;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Resources;
using System.Threading.Tasks;

namespace gpconnect_user_portal.DAL
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly IConfiguration _configuration;
        private readonly ResourceManager _resourceManager;

        public DataService(IConfiguration configuration, ILogger<DataService> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _resourceManager = new ResourceManager("gpconnect_user_portal.DAL.Resources.DataFieldNameResources", typeof(DataFieldNameResources).Assembly);
        }

        public async Task<List<T>> ExecuteSQLQuery<T>(string query) where T : class
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_configuration.GetConnectionString(ConnectionStrings.DefaultConnection));
                var results = (await connection.QueryAsync<T>(query, commandType: CommandType.Text)).AsList();
                return results;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
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

        public DataTable ExecuteQueryAndGetDataTable(string query, Dictionary<string, Guid> parameters, bool transposeDataFieldNames = true)
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
                return transposeDataFieldNames ? TransposeFieldsWithDataFieldNames(_dt) : _dt;
            }
        }

        private DataTable TransposeFieldsWithDataFieldNames(DataTable sourceDataTable)
        {
            if (sourceDataTable.Columns.Contains("FieldName"))
            {
                var dataTable = sourceDataTable.AsEnumerable().Where(x => GetFieldColumnName(x.Field<string>("FieldName")) != null).CopyToDataTable();
                foreach (var row in dataTable.AsEnumerable())
                {
                    row.SetField("FieldName", GetFieldColumnName(row.Field<string>("FieldName")));
                }
                return TransposeColumnNames(dataTable);
            }
            return TransposeColumnNames(sourceDataTable);
        }

        private DataTable TransposeColumnNames(DataTable dataTable)
        {
            for (var i = 0; i < dataTable.Columns.Count; i++)
            {
                dataTable.Columns[i].ColumnName = GetFieldColumnName(dataTable.Columns[i].ColumnName) ?? dataTable.Columns[i].ColumnName;
            }
            return dataTable;
        }

        private string GetFieldColumnName(string fieldColumnName)
        {
            _resourceManager.IgnoreCase = true;
            return _resourceManager.GetString(fieldColumnName);
        }
    }
}
