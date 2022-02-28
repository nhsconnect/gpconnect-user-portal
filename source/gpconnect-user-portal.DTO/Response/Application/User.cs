using System;

namespace gpconnect_user_portal.DTO.Response.Application
{
    public class User
    {
        public int UserId { get; set; }
        public int UserSessionId { get; set; }
        public string EmailAddress { get; set; }
        public DateTimeOffset? LastLogonDate { get; set; }
        public bool IsAdmin { get; set; }
        public DateTimeOffset? AddedDate { get; set; }
        public DateTimeOffset? AuthorisedDate { get; set; }
    }
}
