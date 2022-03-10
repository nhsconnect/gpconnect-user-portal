using Microsoft.AspNetCore.Mvc;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class SupplierDetailModel : BaseSiteModel
    {
        [BindProperty]
        public Models.SupplierProductCapabilityModel SupplierProductCapabilityModel { get; set; }
    }
}