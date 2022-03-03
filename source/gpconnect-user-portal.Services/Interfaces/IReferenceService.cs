using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response.Reference;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IReferenceService
    {
        Task<Task> GetCCGs();
        Task<List<SiteDefinition>> GetSiteDefinitions();
        Task<List<LookupType>> GetLookupTypes();
        Task<List<Lookup>> GetLookups();
        Task<List<Lookup>> GetLookup(int lookupTypeId);
        Task<List<Lookup>> GetProductListWithSupplier();
        Task<EnabledSupplierProductCapability> GetSupplierProductCapabilities(int supplierProductId);
        Task AddLookup(int lookupTypeId, string lookupValue);
        Task UpdateLookup(int lookupId, string lookupValue);
        Task EnableDisableLookup(int lookupTypeId, bool isDisabled = false);
        Task<List<LookupDataCountByType>> GetLookupDataCountByType();
    }
}
