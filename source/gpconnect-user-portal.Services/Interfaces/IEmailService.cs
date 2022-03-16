using gpconnect_user_portal.DTO.Request;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendSiteNotificationEmail(int siteDefinitionStatus, EmailDefinition emailDefinition);
    }
}
