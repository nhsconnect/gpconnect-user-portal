using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ISupplierRequestValidator
{
    public Task<BaseRequestValidator> IsValidUpdate(SupplierUpdateRequest request);
    public Task<BaseRequestValidator> IsValidDisable(SupplierDisableRequest request);
    public bool IsValidAdd(SupplierAddRequest request);
}
