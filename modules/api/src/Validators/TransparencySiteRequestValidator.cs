using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class TransparencySiteRequestValidator: ITransparencySiteRequestValidator
{
    public bool IsValid(TransparencySiteRequest request) 
    {
        bool isValid = false;

        if (!string.IsNullOrWhiteSpace(request.ProviderCode))
        {
            isValid = true;
        }

        if (!string.IsNullOrWhiteSpace(request.ProviderName))
        {
            return !isValid;
        }

        return isValid;
    }
}
