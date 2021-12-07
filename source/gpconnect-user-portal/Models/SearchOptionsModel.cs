using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public class SearchOptionsModel
    {
        [Display(Name = DisplayConstants.CCGICBNAME)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGNames { get; set; }

        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGOdsCodes { get; set; }

        public IEnumerable<SelectListItem> SearchResultSortOptions { get; set; }

        [BindProperty]
        public string SelectedSortOption { get; set; }

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