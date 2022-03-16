namespace gpconnect_user_portal.DTO.Response.Application
{
    public class Email
    {
        public int EmailTemplateId { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string MailRecipient { get; set; }
    }

    public enum MailTemplate
    {
        SendSiteNotificationEmail = 1
    }
}