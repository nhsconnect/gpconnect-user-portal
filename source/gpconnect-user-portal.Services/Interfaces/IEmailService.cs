namespace gpconnect_user_portal.Services.Interfaces
{
    public interface IEmailService
    {
        bool SendSiteUpdateEmail(string recipient, string body);
    }
}
