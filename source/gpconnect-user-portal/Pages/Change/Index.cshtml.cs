using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.Generic;

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

        public IActionResult OnPostSearch()
        {
            if (ModelState.IsValid)
            {
                GetSearchResults();
            }
            return Page();
        }

        public IActionResult OnGetSite(string odscode)
        {
            return Page();
        }

        private void GetSearchResults()
        {
            try
            {
                var searchResults = new DTO.Response.SearchResult
                {
                    SearchResults = new List<DTO.Response.SearchResultEntry>()
                };

                for(int i = 1; i<=100; i++)
                {
                    searchResults.SearchResults.Add(new DTO.Response.SearchResultEntry
                    {
                        CCGName = "NHS LEEDS CCG",
                        CCGODSCode = "15F",
                        HasAppointment = true,
                        HasHtmlView = false,
                        HasStructured = false,
                        SiteName = $"LEEDS STUDENT MEDICAL PRACTICE {i}",
                        SiteODSCode = $"B86110-{i}",
                        UseCase = ""
                    });
                }

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
