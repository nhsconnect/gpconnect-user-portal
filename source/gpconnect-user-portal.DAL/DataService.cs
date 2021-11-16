using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using System;
using System.Collections.Generic;

namespace gpconnect_user_portal.DAL
{
    public class DataService : IDataService
    {
        public List<T> ExecuteQuery<T>(string query, DynamicParameters parameters) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
