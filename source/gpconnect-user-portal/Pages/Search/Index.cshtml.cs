using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Pages
{
    public partial class SearchModel : BaseModel
    {
        private readonly ILogger<SearchModel> _logger;
        private readonly IAggregateService _aggregateService;
        private readonly DTO.Response.CCG _ccgList;

        public SearchModel(ILogger<SearchModel> logger, IAggregateService aggregateService) : base(aggregateService)
        {
            _logger = logger;
            _aggregateService = aggregateService;
            _ccgList = _aggregateService.OrganisationDataService.GetCCGs().Result;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            //CCGNames = await GetCCGs();
            //CCGOdsCodes = await GetCCGs();
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
            SelectedCCGName = CCGNames.First().Value;
            SelectedCCGOdsCode = CCGOdsCodes.First().Value;
            ProviderName = null;
            ModelState.Clear();
            return Page();
        }

        private IEnumerable<SelectListItem> GetCCGs()
        {
            //var ccgs = await _aggregateService.OrganisationDataService.GetCCGs();
            var options = _ccgList.Organisations.Select(ccg => new SelectListItem()
            {
                Text = ccg.Name,
                Value = ccg.OrgId
            }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        private IEnumerable<SelectListItem> GetSearchResultSortOptions()
        {
            var options = new string[] { "Sort by:", "No Record Access: HTML View", "Has Record Access: HTML View", "No Record Access: Structured", "Has Record Access: Structured", "No Appointment", "Has Appointment" };
            return options.Select(option => new SelectListItem { Value = option, Text = option });
        }
    }
}
