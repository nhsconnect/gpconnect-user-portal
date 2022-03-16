using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.Admin.Pages
{
    public partial class UsersModel : BaseModel
    {
        public List<User> Users { get; set; }        
    }
}