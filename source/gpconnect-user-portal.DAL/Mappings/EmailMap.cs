using Dapper.FluentMap.Mapping;
using gpconnect_user_portal.DTO.Response.Application;

namespace gpconnect_user_portal.DAL.Mapping
{
    public class EmailMap : EntityMap<Email>
    {
        public EmailMap()
        {
            Map(p => p.EmailTemplateId).ToColumn("email_template_id"); 
            Map(p => p.MailRecipient).ToColumn("recipients");
            Map(p => p.Subject).ToColumn("subject");
            Map(p => p.Body).ToColumn("body");
        }
    }
}
