using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.Models
{
    public abstract class BaseModel : PageModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly List<Lookup> _ccgOdsCodeList;
        private readonly List<Lookup> _ccgNameList;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
            _ccgOdsCodeList = _aggregateService.ReferenceService.GetLookup((int)Services.Enumerations.LookupType.CCGICBODSCode).Result;
            _ccgNameList = _aggregateService.ReferenceService.GetLookup((int)Services.Enumerations.LookupType.CCGICBName).Result;
        }

        public string ApplicationName => _generalOptionsDelegate.CurrentValue.ProductName;
        public string AssemblyName => _aggregateService.CoreService.GetApplicationDetails().AssemblyName;
        public string LastUpdated => $"{DateTime.UtcNow:MMMM yyyy}";        

        public HtmlString GetAccessEmailAddressLink => new HtmlString($"<a href=\"mailto:{_aggregateService.CoreService.GetApplicationDetails().ApplicationEmailAddress}\">{_aggregateService.CoreService.GetApplicationDetails().ApplicationEmailAddress}</a>");

        [Display(Name = DisplayConstants.CCGICBNAME)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGNames => GetCCGByNames();

        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGOdsCodes => GetCCGByOdsCodes();

        [BindProperty(SupportsGet = true)]
        [Display(Name = DisplayConstants.CCGICBNAME)]
        public string SelectedCCGName { get; set; }

        [BindProperty(SupportsGet = true)]
        [Display(Name = DisplayConstants.CCGICBODSCODE)]
        public string SelectedCCGOdsCode { get; set; }

        public IEnumerable<SelectListItem> GetCCGByNames(int? selectedCCGName = 0)
        {
            var options = _ccgNameList.OrderBy(x => x.LookupValue)
                .Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedCCGName == option.LookupId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        public IEnumerable<SelectListItem> GetCCGByOdsCodes(int? selectedCCGCode = 0)
        {
            var options = _ccgOdsCodeList.OrderBy(x => x.LookupValue)
                .Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString(), Selected = selectedCCGCode == option.LookupId }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }
    }
}