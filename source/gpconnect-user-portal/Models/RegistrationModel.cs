using gpconnect_user_portal.Helpers.Constants;
using Microsoft.AspNetCore.Mvc;
using System;

namespace gpconnect_user_portal.Pages.Change
{
    public partial class RegistrationModel : BaseSiteModel
    {
        [BindProperty]
        public Models.EndpointRegistration EndpointRegistration { get; set; }
        public string PageTitle => string.IsNullOrEmpty(EndpointRegistration.SiteUniqueIdentifier) ? DisplayConstants.CHANGEREGISTERTITLE : DisplayConstants.CHANGEUPDATETITLE;
    }
}