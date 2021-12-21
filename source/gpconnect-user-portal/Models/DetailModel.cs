using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Pages
{
    public partial class DetailModel : BaseSiteModel
    {
        public Guid SiteInstanceGuid { get; set; }
        public string OdsCode { get; set; }
        public string PageTitle => !string.IsNullOrEmpty(OdsCode) ? DisplayConstants.CHANGEUPDATETITLE : DisplayConstants.CHANGEREGISTERTITLE;

        [Required(ErrorMessage = MessageConstants.USECASEDESCRIPTIONREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.USECASEDESCRIPTIONINPUT)]
        [BindProperty(SupportsGet = true)]
        public string UseCaseDescription { get; set; }

        [Required(ErrorMessage = MessageConstants.SELECTEDCARESETTINGREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.CARESETTINGINPUT)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CareSettings => GetDropDown(Services.Enumerations.LookupType.CareSetting);

        [Required(ErrorMessage = MessageConstants.SELECTEDSUPPLIERREQUIREDERRORMESSAGE)]
        [Display(Name = DisplayConstants.SUPPLIERINPUT)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> Suppliers => GetDropDown(Services.Enumerations.LookupType.Supplier);        

        [Display(Name = DisplayConstants.CCGICBNAMEIFAPPLICABLE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGNames => GetCCGByNames();

        [Display(Name = DisplayConstants.CCGICBODSCODEIFAPPLICABLE)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> CCGOdsCodes => GetCCGByOdsCodes();
    }
}