namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Core.HttpClientServices.Interfaces;

public interface IFeedbackService
{
    Task SubmitFeedbackAsync(string overallRating, string improveService);
}
