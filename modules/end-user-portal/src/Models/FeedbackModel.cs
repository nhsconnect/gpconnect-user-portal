using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages.Feedback;

public partial class FeedbackModel : BaseModel
{
    [Display(Name = "OverallRating", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(ErrorMessageResourceName = "OverallRating", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string OverallRating { get; set; } = "";

    public string[] OverallRatings = new[] { "Very satisfied", "Satisfied", "Neither satisfied or dissatisfied", "Dissatisfied", "Very dissatisfied" };

    [Display(Name = "ImproveService", ResourceType = typeof(DataFieldNameResources))]
    [BindProperty(SupportsGet = true)]
    [Required(AllowEmptyStrings = false, ErrorMessageResourceName = "ImproveService", ErrorMessageResourceType = typeof(ErrorMessageResources))]
    public string ImproveService { get; set; } = "";
}
