using gpconnect_user_portal.Resources;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierModel : BaseSiteModel
    {
        [Required(ErrorMessageResourceName = "SupplierName", ErrorMessageResourceType = typeof(ErrorMessageResources))]
        [BindProperty(SupportsGet = true)]
        [Display(Name = "SupplierName", ResourceType = typeof(DataFieldNameResources))]
        public string SupplierName { get; set; }
    }
}