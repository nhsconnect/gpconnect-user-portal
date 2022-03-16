using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Map(p => p.UserId).ToColumn("user_id");
            Map(p => p.UserSessionId).ToColumn("user_session_id");
            Map(p => p.EmailAddress).ToColumn("email_address");
            Map(p => p.LastLogonDate).ToColumn("last_logon_date");
            Map(p => p.IsAdmin).ToColumn("is_admin");
            Map(p => p.AddedDate).ToColumn("added_date");
            Map(p => p.AuthorisedDate).ToColumn("authorised_date");
        }
    }
}
