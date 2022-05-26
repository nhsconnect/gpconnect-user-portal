using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ICareSettingRequestValidator
{
    public bool IsValidUpdate(CareSettingUpdateRequest request);
    public bool IsValidDisable(CareSettingDisableRequest request);
    public bool IsValidAdd(CareSettingAddRequest request);
}
