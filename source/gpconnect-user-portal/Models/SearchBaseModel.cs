using gpconnect_user_portal.DTO.Response.Application.Search;
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
        public readonly SearchOptionsModel SearchOptions;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public SearchResult SearchResult { get; set; }

        protected SearchBaseModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
            SearchOptions = new SearchOptionsModel(_aggregateService, _generalOptionsDelegate)
            {
                SearchResultSortOptions = GetSearchResultSortOptions()
            };
        }

        public static IEnumerable<SelectListItem> GetSearchResultSortOptions()
        {
            return SearchConstants.SortOptions.Select(option => new SelectListItem() { Text = option.Value, Value = option.Key.ToString() });
        }

        protected FileStreamResult ExportResult(DataTable dataTable, string reportName = "")
        {
            var memoryStream = _aggregateService.ReportingService.CreateReport(dataTable, reportName);
            return _aggregateService.ReportingService.GetFileStream(memoryStream);
        }        
    }    
}