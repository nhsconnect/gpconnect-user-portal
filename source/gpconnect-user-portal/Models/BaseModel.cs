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
        private readonly List<Organisation> _organisationList;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
            _organisationList = _aggregateService.ReferenceService.GetOrganisations().Result;            
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

        public IEnumerable<SelectListItem> GetCCGByNames(string selectedCCGName = "")
        {
            var options = _organisationList.OrderBy(x => x.Name)
                .Select(option => new SelectListItem() { Text = option.Name, Value = option.Name, Selected = selectedCCGName == option.Name }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }

        public IEnumerable<SelectListItem> GetCCGByOdsCodes(string selectedCCGCode = "")
        {
            var options = _organisationList.OrderBy(x => x.OdsCode)
                .Select(option => new SelectListItem() { Text = option.OdsCode, Value = option.OdsCode, Selected = selectedCCGCode == option.OdsCode }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }
    }
}