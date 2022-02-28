using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response.Reference;
using System;
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
        Task<List<Lookup>> GetLookup(Enumerations.LookupType lookupTypeId);
        Task<List<Lookup>> GetProductListWithSupplier();
        Task<EnabledSupplierProductCapability> GetSupplierProductCapabilities(int supplierProductId);
        Task AddLookup(Enumerations.LookupType lookupTypeId, string lookupValue);
        Task DisableLookup(int lookupTypeId, DateTime? disableDate);
        Task<List<LookupDataCountByType>> GetLookupDataCountByType();
    }
}
