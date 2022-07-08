using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Service.Interface;
using System.Threading.Tasks;

using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class CareSettingRequestValidator : ICareSettingRequestValidator
{
    private readonly ICareSettingService _careSettingService;

    public CareSettingRequestValidator(ICareSettingService careSettingService)
    {
        _careSettingService = careSettingService ?? throw new ArgumentNullException(nameof(careSettingService));
    }

    public async Task<BaseRequestValidator> IsValidUpdate(CareSettingUpdateRequest request) 
    {
        var validParameters = request.CareSettingId > 0 && (!string.IsNullOrWhiteSpace(request.CareSettingValue));
        var careSetting = await _careSettingService.GetCareSetting(request.CareSettingId);
        return new BaseRequestValidator() { EntityFound = careSetting != null, RequestValid = validParameters };
    }

    public async Task<BaseRequestValidator> IsValidDisable(CareSettingDisableRequest request)
    {
        var validParameters = request.CareSettingId > 0;
        var careSetting = await _careSettingService.GetCareSetting(request.CareSettingId);
        return new BaseRequestValidator() { EntityFound = careSetting != null, RequestValid = validParameters };
    }

    public bool IsValidAdd(CareSettingAddRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.CareSettingValue);
    }
}
