using gpconnect_user_portal.DTO.Response.Reference;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IReferenceService
    {
        Task<List<Organisation>> GetOrganisations();
        Task<Organisation> GetOrganisation(string odsCode);
        Task<Task> GetCCGs();
        Task<List<LookupType>> GetLookupTypes();
        Task<List<Lookup>> GetLookups();
        Task<List<Lookup>> GetLookup(Enumerations.LookupType lookupTypeId);
        Task<EnabledSupplierProduct> GetSupplierProducts(int supplierId);
        Task<List<SupplierProduct>> GetSuppliersProducts();
        Task AddLookup(Enumerations.LookupType lookupTypeId, string lookupValue);
        Task DisableLookup(int lookupTypeId, DateTime? disableDate);
    }
}
