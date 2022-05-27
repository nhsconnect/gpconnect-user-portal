using Dapper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Interfaces
{
    public interface IDataService
    {
        Task<List<T>> ExecuteQuery<T>(string query, DynamicParameters? parameters = null) where T : class;
        Task<List<T>> ExecuteTextQuery<T>(string query) where T : class;
        Task<T> ExecuteQueryFirstOrDefault<T>(string query, DynamicParameters? parameters = null) where T : class;
        Task<int> ExecuteQuery(string query, DynamicParameters parameters);
    }
}
