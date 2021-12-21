﻿using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Helpers.Validators;
using gpconnect_user_portal.Models;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.Pages
{
    public abstract class BaseSiteModel : BaseModel
    {
        private readonly IAggregateService _aggregateService;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        protected BaseSiteModel(IAggregateService aggregateService, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate) : base(aggregateService, generalOptionsDelegate)
        {
            _aggregateService = aggregateService;
            _generalOptionsDelegate = generalOptionsDelegate;
        }

        [Required(ErrorMessage = MessageConstants.NAMEOFSUBMITTERREQUIREDERRORMESSAGE)]
        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSONLY, ErrorMessage = MessageConstants.NAMEOFSUBMITTERVALIDVALUEERRORMESSAGE)]
        [Display(Name = DisplayConstants.NAMEOFSUBMITTER)]
        [BindProperty(SupportsGet = true)]
        public string Submitter { get; set; }

        [Required(ErrorMessage = MessageConstants.CONTACTEMAILADDRESSREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.CONTACTEMAILADDRESS)]
        [BindProperty(SupportsGet = true)]
        public string ContactEmailAddress { get; set; }

        [Required(ErrorMessage = MessageConstants.CONTACTTELEPHONEREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.CONTACTTELEPHONE)]
        [BindProperty(SupportsGet = true)]
        public string ContactTelephone { get; set; }

        [Display(Name = DisplayConstants.NOODSISSUED)]
        [BindProperty(SupportsGet = true)]
        public bool NoOdsIssued { get; set; }

        [RequiredIfFalse(nameof(NoOdsIssued), ErrorMessage = MessageConstants.ODSCODEREQUIREDERRORMESSAGE)]
        [RegularExpression(ValidationConstants.UPPERCASELETTERSANDNUMBERSONLY, ErrorMessage = MessageConstants.ODSCODEVALIDVALUEERRORMESSAGE)]
        [Display(Name = DisplayConstants.ODSCODE)]
        [BindProperty(SupportsGet = true)]
        public string FormOdsCode { get; set; }

        [Required(ErrorMessage = MessageConstants.SITENAMEREQUIREDERRORMESSAGE)]
        [RegularExpression(ValidationConstants.ALPHANUMERICCHARACTERSONLY, ErrorMessage = MessageConstants.SITENAMEVALIDVALUEERRORMESSAGE)]
        [Display(Name = DisplayConstants.SITENAME)]
        [BindProperty(SupportsGet = true)]
        public string SiteName { get; set; }

        [Required(ErrorMessage = MessageConstants.SITEPOSTCODEREQUIREDERRORMESSAGE)]
        [RegularExpression(ValidationConstants.UPPERCASELETTERSANDNUMBERSANDSPACESONLY, ErrorMessage = MessageConstants.SITEPOSTCODEVALIDVALUEERRORMESSAGE)]
        [Display(Name = DisplayConstants.SITEPOSTCODE)]
        [BindProperty(SupportsGet = true)]
        public string SitePostcode { get; set; }

        [RequiredIfTrue(nameof(IsHtmlEnabled), ErrorMessage = MessageConstants.RECORDACCESSHTMLVIEWREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.RECORDACCESSHTMLVIEW)]
        [BindProperty(SupportsGet = true)]
        public string RecordAccessHtmlView { get; set; }

        [RequiredIfTrue(nameof(IsStructuredEnabled), ErrorMessage = MessageConstants.RECORDACCESSSTRUCTUREDREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.RECORDACCESSSTRUCTURED)]
        [BindProperty(SupportsGet = true)]
        public string RecordAccessStructured { get; set; }

        [RequiredIfTrue(nameof(IsAppointmentEnabled), ErrorMessage = MessageConstants.APPOINTMENTREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.APPOINTMENT)]
        [BindProperty(SupportsGet = true)]
        public string Appointment { get; set; }

        public bool IsHtmlEnabled => SupplierProducts != null ? SupplierProducts.IsHtmlEnabled : false;
        public bool IsAppointmentEnabled => SupplierProducts != null ? SupplierProducts.IsAppointmentEnabled : false;
        public bool IsStructuredEnabled => SupplierProducts != null ? SupplierProducts.IsStructuredEnabled : false;

        public EnabledSupplierProduct SupplierProducts { get; set; }

        [Required(ErrorMessage = MessageConstants.SELECTEDCARESETTINGREQUIREDERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string SelectedCareSetting { get; set; }

        [Required(ErrorMessage = MessageConstants.SELECTEDSUPPLIERREQUIREDERRORMESSAGE)]
        [BindProperty(SupportsGet = true)]
        public string SelectedSupplier { get; set; }

        public bool DisplayGpConnectProducts { get; set; }
        
        protected IEnumerable<SelectListItem> GetDropDown(Services.Enumerations.LookupType lookupType)
        {
            var lookup = _aggregateService.ReferenceService.GetLookup(lookupType).Result;
            var options = lookup.Select(option => new SelectListItem() { Text = option.LookupValue, Value = option.LookupId.ToString() }).ToList();
            options.Insert(0, new SelectListItem());
            return options;
        }
    }
}