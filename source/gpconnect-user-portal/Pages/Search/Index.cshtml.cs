using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;

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

        public IActionResult OnPostSearchAsync()
        {
            if (ModelState.IsValid)
            {
                GetSearchResults();
            }
            return Page();
        }

        private void GetSearchResults()
        {
            try
            {
                var searchResults = new DTO.Response.SearchResult
                {
                    SearchResults = new List<DTO.Response.SearchResultEntry>
                    {
                        new DTO.Response.SearchResultEntry
                        {
                            CCGName = "NHS LEEDS CCG",
                            CCGODSCode = "15F",
                            HasAppointment = true,
                            HasHtmlView = false,
                            HasStructured = false,
                            SiteName = "LEEDS STUDENT MEDICAL PRACTICE",
                            SiteODSCode = "B86110",
                            UseCase = ""
                        },
                        new DTO.Response.SearchResultEntry
                        {
                            CCGName = "NHS LEEDS CCG",
                            CCGODSCode = "15F",
                            HasAppointment = true,
                            HasHtmlView = true,
                            HasStructured = true,
                            SiteName = "LEEDS CITY MEDICAL PRACTICE",
                            SiteODSCode = "B86012",
                            UseCase = ""
                        }
                    }
                };
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

        public FileStreamResult OnPostExportAll()
        {
            //var exportTable = _aggregateService;
            //return ExportResult(exportTable);
            return null;
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
