using Dapper.FluentMap.Mapping;
using GpConnect.NationalDataSharingPortal.Api.Dto.Response;

namespace GpConnect.NationalDataSharingPortal.Api.Dal.Mapping
{
    public class UserMap : EntityMap<User>
    {
        public UserMap()
        {
            Map(p => p.UserId).ToColumn("user_id");
            Map(p => p.EmailAddress).ToColumn("email_address");
            Map(p => p.LastLogonDate).ToColumn("last_logon_date");
            Map(p => p.IsAdmin).ToColumn("is_admin");
        }
    }
}
