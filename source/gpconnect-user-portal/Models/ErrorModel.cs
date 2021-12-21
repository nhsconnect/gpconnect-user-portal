using gpconnect_user_portal.Models;

namespace gpconnect_user_portal.Pages
{
    public partial class ErrorModel : BaseModel
    {
        public int ErrorStatusCode { get; set; }
        public string RequestId { get; set; }
    }
}