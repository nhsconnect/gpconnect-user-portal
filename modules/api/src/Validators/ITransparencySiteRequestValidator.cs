using System;
using gpconnect_user_portal.api.dto.request;

namespace gpconnect_user_portal.api.validators;

public interface ITransparencySiteRequestValidator
{
    public Boolean IsValid(TransparencySiteRequest request);
}
