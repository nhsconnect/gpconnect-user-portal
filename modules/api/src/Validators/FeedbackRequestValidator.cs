using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

using GpConnect.NationalDataSharingPortal.Api.Validators.Interface;

namespace GpConnect.NationalDataSharingPortal.Api.Validators;

public class FeedbackRequestValidator : IFeedbackRequestValidator
{
    public bool IsValidAdd(FeedbackAddRequest request)
    {
        return !string.IsNullOrWhiteSpace(request.ImproveService) && !string.IsNullOrWhiteSpace(request.OverallRating);
    }
}
