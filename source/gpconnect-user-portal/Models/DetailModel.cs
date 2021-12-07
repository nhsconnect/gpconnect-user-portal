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

        [Display(Name = DisplayConstants.USECASEDESCRIPTIONINPUT)]
        [BindProperty(SupportsGet = true)]
        public string UseCaseDescription { get; set; }

        [Display(Name = DisplayConstants.USECASEINPUT)]
        [BindProperty(SupportsGet = true)]
        public IEnumerable<SelectListItem> UseCases => GetUseCases();
    }
}