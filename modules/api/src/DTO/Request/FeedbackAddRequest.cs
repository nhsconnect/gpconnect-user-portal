using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.Api.Dto.Request;

public class FeedbackAddRequest 
{
    [Required]
    public string OverallRating { get; set; } = "";
    [Required]
    public string ImproveService { get; set; } = "";    
}
