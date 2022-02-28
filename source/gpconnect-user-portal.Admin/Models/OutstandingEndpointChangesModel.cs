using gpconnect_user_portal.DTO.Response.Application;
using Microsoft.AspNetCore.Mvc;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class OutstandingEndpointChangesModel : BaseSiteModel
    {
        [BindProperty]
        public List<EndpointChange> EndpointChanges { get; set; }
    }
}