using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Threading.Tasks;

using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class SupplierRequestValidator : ISupplierRequestValidator
{
    private readonly ISupplierService _supplierService;

    public SupplierRequestValidator(ISupplierService supplierService)
    {
        _supplierService = supplierService;
    }

    public async Task<BaseRequestValidator> IsValidUpdate(SupplierUpdateRequest request) 
    {
        var validParameters = request.SupplierId > 0 && (!string.IsNullOrWhiteSpace(request.SupplierValue));
        var supplier = await _supplierService.GetSupplier(request.SupplierId);
        return new BaseRequestValidator() { EntityFound = supplier != null, RequestValid = validParameters };
    }

    public async Task<BaseRequestValidator> IsValidDisable(SupplierDisableRequest request)
    {
        var validParameters = request.SupplierId > 0;
        var supplier = await _supplierService.GetSupplier(request.SupplierId);
        return new BaseRequestValidator() { EntityFound = supplier != null, RequestValid = validParameters };
    }

    public bool IsValidAdd(SupplierAddRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.SupplierValue);
    }
}
