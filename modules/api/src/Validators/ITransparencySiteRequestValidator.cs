using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public interface ITransparencySiteRequestValidator
{
    public bool IsValid(TransparencySiteRequest request);
}
