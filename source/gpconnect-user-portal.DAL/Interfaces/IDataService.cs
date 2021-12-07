using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gpconnect_user_portal.DAL.Interfaces
{
    public interface IDataService
    {
        Task<List<T>> ExecuteQuery<T>(string query, DynamicParameters parameters = null) where T : class;
        Task<T> ExecuteQueryFirstOrDefault<T>(string query, DynamicParameters parameters = null) where T : class;
        Task<int> ExecuteQuery(string query, DynamicParameters parameters);
    }
}
