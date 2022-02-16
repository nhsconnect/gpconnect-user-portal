using gpconnect_user_portal.Admin.Models;
using gpconnect_user_portal.DTO.Response.Application;
using gpconnect_user_portal.DTO.Response.Reference;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class IndexModel : BaseModel
    {
        public List<EndpointChangeCountByStatus> EndpointChangeCountByStatus { get; set; }        
        public List<LookupDataCountByType> LookupDataCountByType { get; set; }
    }
}