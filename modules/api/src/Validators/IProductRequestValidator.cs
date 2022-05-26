using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface IProductRequestValidator
{
    public Task<BaseRequestValidator> IsValidUpdate(ProductUpdateRequest request);
    public Task<BaseRequestValidator> IsValidDisable(ProductDisableRequest request);
    public bool IsValidAdd(ProductAddRequest request);
}
