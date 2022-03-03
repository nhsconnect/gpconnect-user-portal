using gpconnect_user_portal.Resources;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class LookupDetailModel : BaseSiteModel
    {
        public List<DTO.Response.Reference.Lookup> Lookups { get; set; }
        public Services.Enumerations.LookupType LookupType { get; set; }
        public string LookupName { get; set; }
        public int? UpdateLookupId { get; set; }

        [Required(ErrorMessageResourceName = "UpdateLookupValue", ErrorMessageResourceType = typeof(ErrorMessageResources))] 
        public string UpdateLookupValue { get; set; }

    }
}