﻿using Dapper;
using GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Dal
{
    public class DataService : IDataService
    {
        private readonly ILogger<DataService> _logger;
        private readonly string _connectionString;

        public DataService(IOptions<ConnectionStrings> optionsAccessor, ILogger<DataService> logger)
        {
            _logger = logger;
            _connectionString = optionsAccessor.Value.DefaultConnection;
        }

        public async Task<List<T>> ExecuteQuery<T>(string query, DynamicParameters? parameters = null) where T : class
        {
            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
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
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
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
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                var rowsProcessed = await connection.ExecuteAsync(query, parameters, commandType: CommandType.StoredProcedure);
                return rowsProcessed;
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
                using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
                var results = (await connection.QueryAsync<T>(query, commandType: CommandType.Text)).AsList();
                return results;
            }
            catch (Exception exc)
            {
                _logger?.LogError(exc, $"An error has occurred while attempting to execute the query {query}");
                throw;
            }
        }
    }
}