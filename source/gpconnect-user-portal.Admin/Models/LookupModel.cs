using gpconnect_user_portal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class LookupModel : BaseSiteModel
    {
        [BindProperty(SupportsGet = true)]
        public DTO.Response.Reference.LookupType LookupType { get; set; }
                
        [Required(ErrorMessageResourceName = "UpdateLookupValue", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "LookupValue", ResourceType = typeof(DataFieldNameResources))]
        public string LookupValue { get; set; }
    }
}