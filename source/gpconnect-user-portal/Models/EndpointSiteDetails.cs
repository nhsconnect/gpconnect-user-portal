using gpconnect_user_portal.Resources;
using gpconnect_user_portal.DTO.Response.Reference;
using gpconnect_user_portal.Helpers.Constants;
using gpconnect_user_portal.Helpers.Validators;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace gpconnect_user_portal.Models
{
    public class EndpointSiteDetails
    {
        [Required(ErrorMessageResourceName = "SiteName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [Display(Name = "SiteName", ResourceType = typeof(DataFieldNameResources))]
        public string SiteName { get; set; }

        public bool CanEditEndpointSiteDetails { get; set; }

        [Required(ErrorMessageResourceName = "SitePostcode", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [RegularExpression(ValidationConstants.UPPERCASELETTERSANDNUMBERSANDSPACESONLY, ErrorMessageResourceName = "SitePostcode", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "SitePostcode", ResourceType = typeof(DataFieldNameResources))]
        public string SitePostcode { get; set; }

        [RequiredIfFalse(nameof(NoOdsIssued), "OdsCode", typeof(ErrorMessageResources))]
        [RegularExpression(ValidationConstants.UPPERCASELETTERSANDNUMBERSONLY, ErrorMessageResourceName = "OdsCode", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "OdsCode", ResourceType = typeof(DataFieldNameResources))]
        public string OdsCode { get; set; }

        [Display(Name = "NoOdsIssued", ResourceType = typeof(DataFieldNameResources))]
        public bool NoOdsIssued { get; set; }

        [Display(Name = "SelectedCCGNameIfApplicable", ResourceType = typeof(DataFieldNameResources))]
        public IEnumerable<SelectListItem> CCGNames { get; set; }

        [Display(Name = "SelectedCCGName", ResourceType = typeof(DataFieldNameResources))]
        public string SelectedCCGName { get; set; }
    }
}