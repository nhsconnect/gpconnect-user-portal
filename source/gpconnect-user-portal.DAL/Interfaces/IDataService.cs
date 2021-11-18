using Dapper;
using System.Collections.Generic;

namespace gpconnect_user_portal.DAL.Interfaces
{
    public interface IDataService
    {
        List<T> ExecuteQuery<T>(string query, DynamicParameters parameters) where T : class;
        int ExecuteQuery(string query, DynamicParameters parameters);
    }
}
