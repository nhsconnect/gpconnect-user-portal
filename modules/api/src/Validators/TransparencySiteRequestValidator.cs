using System;
using gpconnect_user_portal.api.dto.request;

namespace gpconnect_user_portal.api.validators;

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
