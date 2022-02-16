using gpconnect_user_portal.Admin.Models;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class CompletedModel : BaseModel
    {
        public List<EndpointChange> EndpointChanges { get; set; }        
    }
}