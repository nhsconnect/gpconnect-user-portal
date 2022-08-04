using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Validators.Interface;

public interface IFeedbackRequestValidator
{
    public bool IsValidAdd(FeedbackAddRequest request);
}
