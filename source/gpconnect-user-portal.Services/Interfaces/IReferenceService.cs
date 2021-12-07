using gpconnect_user_portal.DTO.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IReferenceService
    {
        Task<List<Organisation>> GetOrganisations();
        Task<Organisation> GetOrganisation(string odsCode);
        Task<Task> GetCCGs();
    }
}
