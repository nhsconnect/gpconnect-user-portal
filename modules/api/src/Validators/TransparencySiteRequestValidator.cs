using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;
using gpconnect_user_portal.api.dto.request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class TransparencySiteRequestValidator: ITransparencySiteRequestValidator
{
    public Boolean IsValid(TransparencySiteRequest request) 
    {
        if (!string.IsNullOrWhiteSpace(request.CcgCode))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(request.CcgName))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(request.ProviderCode))
        {
            return true;
        }

        if (!string.IsNullOrWhiteSpace(request.ProviderName))
        {
            return true;
        }

        return false;
    }
}
