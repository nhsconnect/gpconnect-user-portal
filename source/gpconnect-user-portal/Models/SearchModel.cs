using gpconnect_user_portal.Helpers.Validators;
using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    //[AtLeastOneProperty("ProviderOdsCode", "ProviderName", "SelectedCCGName", "SelectedCCGOdsCode", ErrorMessage = "You must supply at least one value")]
    public partial class SearchModel : SearchBaseModel
    {
        [Display(Name = DisplayConstants.CCGICBNAME)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGNames => GetCCGByNames();
        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGOdsCodes => GetCCGByOdsCodes();

        public IEnumerable<SelectListItem> SearchResultSortOptions => GetSearchResultSortOptions();

        [BindProperty]
        public string SelectedSortOption { get; set; }

        public SearchResult SearchResults { get; set; }

        [Display(Name = DisplayConstants.PROVIDERODSCODE)]
        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessage = SearchConstants.PROVIDERODSCODEVALIDERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string ProviderOdsCode { get; set; }

        [Display(Name = DisplayConstants.PROVIDERNAME)]
        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCCGName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCCGOdsCode { get; set; }
    }
}