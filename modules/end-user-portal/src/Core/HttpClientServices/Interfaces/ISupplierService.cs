using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface ISupplierService
{
    Task<List<SoftwareSupplierResult>> GetSoftwareSuppliersAsync();
    Task<SoftwareSupplierResult> GetSoftwareSupplierAsync(int supplierId);
}
