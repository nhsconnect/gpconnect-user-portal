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

  public List<CcgModel> CcgList { get; set; } = null;

  [Display(Name = "ProviderOdsCode", ResourceType = typeof(DataFieldNameResources))]
  [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessageResourceType = typeof(ValidationMessageResources), ErrorMessageResourceName = "ProviderOdsCode")]
  [BindProperty(SupportsGet = true)]
  public string? ProviderOdsCode { get; set; } = "";

  [Display(Name = "ProviderName", ResourceType = typeof(DataFieldNameResources))]
  [BindProperty(SupportsGet = true)]
  public string? ProviderName { get; set; } = "";

  [BindProperty(SupportsGet = true)]
  [Display(Name = "CcgIcbName", ResourceType = typeof(DataFieldNameResources))]
  public IEnumerable<SelectListItem> CcgIcbNames => GetCcgIcbNames();

  [BindProperty(SupportsGet = true)]
  [Display(Name = "CcgIcbOdsCode", ResourceType = typeof(DataFieldNameResources))]
  public IEnumerable<SelectListItem> CcgIcbOdsCodes => GetCcgIcbOdsCodes();

  [BindProperty(SupportsGet = true)]
  public string? SelectedCcgIcbName { get; set; } = "";

  [BindProperty(SupportsGet = true)]
  public string? SelectedCcgIcbOdsCode { get; set; } = "";

  public bool IsValidSearch => CheckForValidSearch();
  public bool HasMultipleSearchParamaters => HasMultipleSearchParameters();

  private bool HasMultipleSearchParameters()
  {
    var multipleSearchParametersEntered = new string[] { ProviderOdsCode, ProviderName };
    return multipleSearchParametersEntered.Count(s => !string.IsNullOrEmpty(s)) > 1;
  }

  private bool CheckForValidSearch()
  {
    return !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName);
  }

  public bool DisplaySearchInvalid { get; set; } = false;

  public IEnumerable<SelectListItem> GetCcgIcbNames(string selectedCcgIcbOdsCode = "")
  {
    var options = CcgList.Select(option => new SelectListItem()
    {
      Text = option.CcgName,
      Value = option.CcgOdsCode,
      Selected = selectedCcgIcbOdsCode == option.CcgOdsCode
    }
    ).OrderBy(c => c.Text).ToList();
    options.Insert(0, new SelectListItem());
    return options;
  }

  public IEnumerable<SelectListItem> GetCcgIcbOdsCodes(string selectedCcgIcbOdsCode = "")
  {
    var options = CcgList.Select(option => new SelectListItem()
    {
      Text = option.CcgOdsCode,
      Value = option.CcgOdsCode,
      Selected = selectedCcgIcbOdsCode == option.CcgOdsCode
    }
    ).OrderBy(c => c.Text).ToList();
    options.Insert(0, new SelectListItem());
    return options;
  }
}
