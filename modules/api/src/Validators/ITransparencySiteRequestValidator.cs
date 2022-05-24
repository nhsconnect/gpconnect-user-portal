using System;
using GpConnect.NationalDataSharingPortal.Api.DTO.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ITransparencySiteRequestValidator
{
    public Boolean IsValid(TransparencySiteRequest request);
}
