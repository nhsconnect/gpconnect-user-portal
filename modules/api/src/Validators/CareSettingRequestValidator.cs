using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class CareSettingRequestValidator : ICareSettingRequestValidator
{
    public bool IsValidUpdate(CareSettingUpdateRequest request) 
    {
        return request.CareSettingId > 0 && (!string.IsNullOrWhiteSpace(request.CareSettingValue));
    }

    public bool IsValidEnableDisable(CareSettingEnableDisableRequest request)
    {
        return request.CareSettingId > 0;
    }

    public bool IsValidAdd(CareSettingAddRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.CareSettingValue);
    }
}
