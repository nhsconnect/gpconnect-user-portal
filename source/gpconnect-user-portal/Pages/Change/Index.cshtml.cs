using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class ChangeModel : SearchBaseModel
    {
        private readonly ILogger<ChangeModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public ChangeModel(ILogger<ChangeModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (ModelState.IsValid && IsValidSearch)
            {
                DisplaySearchInvalid = false;
                await GetSearchResults();
            }
            else
            {
                DisplaySearchInvalid = true;
            }
            return Page();
        }

        public IActionResult OnPostCreate()
        {
            return RedirectToPagePermanent("Detail");
        }

        private async Task GetSearchResults()
        {
            try
            {
                var searchRequest = new SearchRequest()
                {
                    ProviderOdsCode = ProviderOdsCode,
                    ProviderName = ProviderName,
                    CCGOdsCode = SelectedCCGOdsCode,
                    CCGName = SelectedCCGName
                };
                var searchResults = await _aggregateService.QueryService.GetSearchResults(searchRequest);
                SearchResult = searchResults;
            }
            catch
            {                
                throw;
            }
        }

        public IActionResult OnPostClear()
        {
            SearchOptions.ProviderOdsCode = null;
            SearchOptions.SelectedCCGName = null;
            SearchOptions.SelectedCCGOdsCode = null;            
            SearchOptions.ProviderName = null;
            ModelState.Clear();
            return Page();
        }
    }
}
