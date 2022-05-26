using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface ISupplierService
{
    Task<IEnumerable<Supplier>> GetSuppliers();
    Task<Supplier> GetSupplier(int id);
    Task UpdateSupplier(SupplierUpdateRequest supplierUpdateRequest);
    Task DisableSupplier(SupplierDisableRequest supplierDisableRequest);
    Task<Supplier> AddSupplier(SupplierAddRequest supplierAddRequest);
}