using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public partial class SearchModel : SearchBaseModel
    {
        public IEnumerable<SelectListItem> SearchResultSortOptions => GetSearchResultSortOptions();

        [BindProperty]
        public string SelectedSortOption { get; set; }

        public SearchResult SearchResults { get; set; }

        [Display(Name = DisplayConstants.PROVIDERODSCODE)]
        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessage = MessageConstants.ODSCODEVALIDVALUEERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string ProviderOdsCode { get; set; }

        [Display(Name = DisplayConstants.PROVIDERNAME)]
        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        public bool IsValidSearch => !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName) || !string.IsNullOrEmpty(SelectedCCGName) || !string.IsNullOrEmpty(SelectedCCGOdsCode);

        public bool DisplaySearchInvalid { get; set; } = false;
    }
}