using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class SearchModel : BaseModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly IAggregateService _aggregateService;

        public SearchModel(ILogger<SearchModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
        }

        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostSearchAsync()
        {
            if (ModelState.IsValid)
            {
                await GetSearchResults();
            }
            return Page();
        }

        private async Task GetSearchResults()
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
                SearchResults = searchResults;
            }
            catch
            {
                throw;
            }
        }

        public IActionResult OnPostClear()
        {
            ProviderOdsCode = null;
            CCGOdsCode = null;
            SelectedCCG = CCGs.First().Value;
            ProviderName = null;
            ModelState.Clear();
            return Page();
        }

        private IEnumerable<SelectListItem> GetCCGs()
        {
            yield return new SelectListItem("Leeds", "Leeds");
        }
    }
}
