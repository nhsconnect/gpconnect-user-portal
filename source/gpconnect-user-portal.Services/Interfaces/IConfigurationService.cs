using gpconnect_user_portal.DAL.Enumerations;
using gpconnect_user_portal.DTO.Response.Configuration;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IConfigurationService
    {
        Task<ReferenceApiQuery> GetReferenceApiQuery(ReferenceApiQueryTypes referenceApiQueryType);
        Task<Spine> GetSpineConfiguration();
        Task<FhirApiQuery> GetFhirApiQuery(FhirApiQueryTypes fhirApiQueryType);
    }
}
