using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;
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

        if (request.StartPosition < 1)
        {
            return isValid;
        }

        if (request.Count < 1)
        {
            return isValid;
        }

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
