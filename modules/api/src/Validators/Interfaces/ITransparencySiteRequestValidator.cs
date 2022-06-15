using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators.Interface;

public interface ITransparencySiteRequestValidator
{
    public bool IsValidRequest(TransparencySiteRequest request);

    public bool IsValidId(string id);
}
