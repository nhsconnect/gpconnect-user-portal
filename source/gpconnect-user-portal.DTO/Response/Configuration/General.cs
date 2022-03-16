using System;

namespace gpconnect_user_portal.DTO.Response.Configuration
{
    public class General
    {
        public string ProductName { get; set; }
        public string ProductVersion { get; set; }
        public string AdminProductName { get; set; }
        public string GetAccessEmailAddress { get; set; }
        public string FormattedApplicationCurrentDateTime => DateTime.UtcNow.ToString("F");

    }
}
