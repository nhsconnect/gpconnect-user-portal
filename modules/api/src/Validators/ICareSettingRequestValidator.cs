using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System.Threading.Tasks;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ICareSettingRequestValidator
{
    public Task<BaseRequestValidator> IsValidUpdate(CareSettingUpdateRequest request);
    public Task<BaseRequestValidator> IsValidDisable(CareSettingDisableRequest request);
    public bool IsValidAdd(CareSettingAddRequest request);
}
