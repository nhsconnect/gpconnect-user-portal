using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;
using gpconnect_user_portal.api.dto.request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ITransparencySiteRequestValidator
{
    public Boolean IsValid(TransparencySiteRequest request);
}
