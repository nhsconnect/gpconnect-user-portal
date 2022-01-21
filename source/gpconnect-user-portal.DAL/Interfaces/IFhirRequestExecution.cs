using System.Threading;
using System.Threading.Tasks;

namespace gpconnect_user_portal.DAL.Interfaces
{
    public interface IFhirRequestExecution
    {
        Task<T> ExecuteFhirQuery<T>(string query, CancellationToken cancellationToken, string hostName = null, string apiKey = null) where T : class;
    }
}
