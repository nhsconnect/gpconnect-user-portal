using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public class SearchOptionsModel : BaseSiteModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SearchOptionsModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public IEnumerable<SelectListItem> SearchResultSortOptions { get; set; }

        [BindProperty]
        public string SelectedSortOption { get; set; }

        [Display(Name = DisplayConstants.PROVIDERODSCODE)]
        [RegularExpression(ValidationConstants.UPPERCASELETTERSANDNUMBERSONLY, ErrorMessage = MessageConstants.ODSCODEVALIDVALUEERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string ProviderOdsCode { get; set; }

        [Display(Name = DisplayConstants.PROVIDERNAME)]
        [BindProperty(SupportsGet = true)]
        public string ProviderName { get; set; }

        public bool IsValidSearch => !string.IsNullOrEmpty(ProviderOdsCode) || !string.IsNullOrEmpty(ProviderName) || !string.IsNullOrEmpty(SelectedCCGName) || !string.IsNullOrEmpty(SelectedCCGOdsCode);

        public bool DisplaySearchInvalid { get; set; }
    }
}