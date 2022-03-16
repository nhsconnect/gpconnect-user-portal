using gpconnect_user_portal.DTO.Response.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace gpconnect_user_portal.Core.Configuration.Infrastructure
{
    public static class SmtpClientExtensions
    {
        public static void AddSmtpClientServices(IServiceCollection services)
        {
            var scope = services.BuildServiceProvider().CreateScope();
            var emailOptions = scope.ServiceProvider.GetRequiredService<IOptions<Email>>();

            services.AddScoped(serviceProvider => new SmtpClient
            {
                Host = emailOptions.Value.HostName,
                Port = emailOptions.Value.Port,
                DeliveryFormat = SmtpDeliveryFormat.SevenBit,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = true,
                Credentials = GetCredentials(emailOptions)
            });
        }

        private static ICredentialsByHost GetCredentials(IOptions<Email> emailOptions)
        {
            return new NetworkCredential
            {
                UserName = emailOptions.Value.UserName,
                Password = emailOptions.Value.Password
            };
        }
    }
}
