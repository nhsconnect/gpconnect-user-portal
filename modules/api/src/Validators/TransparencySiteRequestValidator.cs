using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class TransparencySiteRequestValidator: ITransparencySiteRequestValidator
{
    public bool IsValidId(string id)
    {
        return Guid.TryParse(id, out _);
    }

    public bool IsValidRequest(TransparencySiteRequest request) 
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
