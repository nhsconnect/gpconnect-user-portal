using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GpConnect.NationalDataSharingPortal.EndUserPortal.Pages;

public partial class SearchModel : BaseModel
{
  public SearchResult SearchResult { get; set; } = null;

  [Display(Name = "ProviderOdsCode", ResourceType = typeof(DataFieldNameResources))]
  [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessageResourceType = typeof(ValidationMessageResources), ErrorMessageResourceName = "ProviderOdsCode")]
  [BindProperty(SupportsGet = true)]
  public string? ProviderOdsCode { get; set; } = "";

  [Display(Name = "ProviderName", ResourceType = typeof(DataFieldNameResources))]
  [BindProperty(SupportsGet = true)]
  public string? ProviderName { get; set; } = "";

  public bool IsValidSearch => CheckForValidSearch();
  public bool HasMultipleSearchParameters => CheckForMultipleSearchParameters();

  private bool CheckForMultipleSearchParameters()
  {
      return
        !(
            String.IsNullOrEmpty(ProviderOdsCode) ||
            String.IsNullOrEmpty(ProviderName)
        );
  }

  private bool CheckForValidSearch()
  {
    return !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName);
  }

  public bool DisplaySearchInvalid { get; set; } = false;
}
