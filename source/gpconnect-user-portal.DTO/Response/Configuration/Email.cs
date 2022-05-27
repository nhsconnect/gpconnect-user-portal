using System.Security.Authentication;

namespace gpconnect_user_portal.DTO.Response.Configuration
{
    public class Email
    {
        public string SenderAddress { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DefaultSubject { get; set; }
    }
}
