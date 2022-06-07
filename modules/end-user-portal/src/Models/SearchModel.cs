using GpConnect.NationalDataSharingPortal.EndUserPortal.Helpers.Constants;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Models;
using GpConnect.NationalDataSharingPortal.EndUserPortal.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
    var multipleSearchParametersEntered = new string[] { ProviderOdsCode, ProviderName };
    return multipleSearchParametersEntered.Count(s => !string.IsNullOrEmpty(s)) > 1;
  }

  private bool CheckForValidSearch()
  {
    return !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName);
  }

  public bool DisplaySearchInvalid { get; set; } = false;  
}
