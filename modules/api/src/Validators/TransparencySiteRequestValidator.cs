using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class TransparencySiteRequestValidator: ITransparencySiteRequestValidator
{
    public bool IsValid(TransparencySiteRequest request) 
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
