using GpConnect.NationalDataSharingPortal.Api.Dto.Request;
using System;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ITransparencySiteRequestValidator
{
    public Boolean IsValid(TransparencySiteRequest request);
}
