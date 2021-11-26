using System;
using System.Threading;
using System.Threading.Tasks;

namespace gpconnect_user_portal.DAL.Interfaces
{
    public interface IFhirRequestExecution
    {
        Task<T> ExecuteFhirQuery<T>(Uri endPoint, CancellationToken cancellationToken) where T : class;
    }
}
