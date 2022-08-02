using System.Threading.Tasks;
using GpConnect.NationalDataSharingPortal.Api.Dto.Request;

namespace GpConnect.NationalDataSharingPortal.Api.Service.Interface;

public interface IFeedbackService
{
    Task CreateFeedbackAsync(FeedbackAddRequest feedbackAddRequest);
}
