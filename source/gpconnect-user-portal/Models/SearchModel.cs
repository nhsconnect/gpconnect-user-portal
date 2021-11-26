using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public partial class SearchModel : BaseModel
    {
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGNames => GetCCGs();
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGOdsCodes => GetCCGs();

        public IEnumerable<SelectListItem> SearchResultSortOptions => GetSearchResultSortOptions();

        [BindProperty]
        public string SelectedSortOption { get; set; }

        public SearchResult SearchResults { get; set; }

        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessage = SearchConstants.PROVIDERODSCODEVALIDERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string ProviderOdsCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSWITHLEADINGTRAILINGSPACESANDCOMMASPACEONLY, ErrorMessage = SearchConstants.CCGODSCODEVALIDERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string CCGOdsCode { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCCGName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedCCGOdsCode { get; set; }
    }
}