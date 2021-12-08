using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class SearchModel : SearchBaseModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SearchModel(ILogger<SearchModel> logger, IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
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
                var searchResults = await _aggregateService.QueryService.GetSites(searchRequest);
                SearchResult = searchResults;
            }
            catch
            {
                throw;
            }
        }

        public FileStreamResult OnPostExportResults()
        {
            //var exportTable = _aggregateService;
            //return ExportResult(exportTable);
            return null;
        }

        public async Task<FileStreamResult> OnPostExportAll()
        {
            //var searchResults = await _aggregateService.ReportingService.ExportAllSites();

            //var exportTable = _aggregateService;
            //return ExportResult(exportTable);
            return null;
        }

        public IActionResult OnPostClear()
        {
            ProviderOdsCode = null;
            SelectedCCGName = null;
            SelectedCCGOdsCode = null;
            ProviderName = null;
            ModelState.Clear();
            return Page();
        }
    }
}
