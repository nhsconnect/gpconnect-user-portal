using gpconnect_user_portal.DTO.Response;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace gpconnect_user_portal.Pages
{
    public abstract class SearchBaseModel : BaseModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly List<Organisation> _organisationList;
        public readonly SearchOptionsModel SearchOptions;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SearchResult SearchResult { get; set; }

        protected SearchBaseModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
            _organisationList = _aggregateService.ReferenceService.GetOrganisations().Result;
            SearchOptions = new SearchOptionsModel()
            {
                CCGNames = GetCCGByNames(),
                CCGOdsCodes = GetCCGByOdsCodes(),
                SearchResultSortOptions = GetSearchResultSortOptions()
            };
        }

        public IEnumerable<SelectListItem> GetCCGByNames()
        {
            var options = _organisationList.OrderBy(x => x.Name)
                .Select(option => new SelectListItem() { Text = option.Name, Value = option.OrgId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        public IEnumerable<SelectListItem> GetCCGByOdsCodes()
        {
            var options = _organisationList.OrderBy(x => x.OrgId)
                .Select(option => new SelectListItem() { Text = option.OrgId, Value = option.OrgId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        public static IEnumerable<SelectListItem> GetSearchResultSortOptions()
        {
            return SearchConstants.SortOptions.Select(option => new SelectListItem() { Text = option.Value, Value = option.Key });
        }

        protected FileStreamResult ExportResult(DataTable dataTable)
        {
            var memoryStream = _aggregateService.ReportingService.CreateReport(dataTable);
            return _aggregateService.ReportingService.GetFileStream(memoryStream);
        }        
    }    
}