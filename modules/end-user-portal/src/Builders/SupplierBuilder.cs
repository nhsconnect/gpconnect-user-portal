using GpConnect.NationalDataSharingPortal.EndUserPortal.Builders.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models.Request;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Builders;

public class SupplierBuilder : ISupplierBuilder
{
    private readonly ISupplierService _supplierService;

    public SupplierBuilder(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task<SupplierInformation> Build(int supplierId)
    {
        var supplier = await _supplierService.GetSoftwareSupplierAsync(supplierId);

        return new SupplierInformation
        {
            Name = supplier.SoftwareSupplierName
        };
    }
}
