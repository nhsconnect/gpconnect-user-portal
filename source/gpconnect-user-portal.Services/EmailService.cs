using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;

namespace gpconnect_user_portal.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpClient _smtpClient;
        private readonly IOptionsMonitor<DTO.Response.Configuration.Email> _emailOptionsDelegate;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;

        public EmailService(SmtpClient smtpClient, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate, IOptionsMonitor<DTO.Response.Configuration.Email> emailOptionsDelegate, ILogger<EmailService> logger)
        {
            _logger = logger;
            _emailOptionsDelegate = emailOptionsDelegate;
            _generalOptionsDelegate = generalOptionsDelegate;
            _smtpClient = smtpClient;
        }

        public bool SendSiteUpdateEmail(string recipient, string body)
        {
            return SendEmail(recipient, "This is a site update notification email");
        }

        private bool SendEmail(string recipient, string body, bool sendToSender = false, bool sendToRecipient = true)
        {
            if (string.IsNullOrEmpty(recipient)) throw new ArgumentNullException(nameof(recipient));
            var sender = _emailOptionsDelegate.CurrentValue.SenderAddress;
            var displayName = StringExtensions.Coalesce(_generalOptionsDelegate.CurrentValue.ProductName, _emailOptionsDelegate.CurrentValue.SenderAddress);
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(sender, displayName),
                    IsBodyHtml = false,
                    Subject = _emailOptionsDelegate.CurrentValue.DefaultSubject,
                    Body = body
                };
                if (sendToSender) mailMessage.To.Add(sender);
                if (sendToRecipient) mailMessage.To.Add(recipient);
                
                _smtpClient.Send(mailMessage);
                return true;
            }
            catch (WebException webException)
            {
                _logger?.LogError(webException, "A connectivity error has occurred while attempting to send an email");
                return false;
            }
            catch (TimeoutException timeoutException)
            {
                _logger?.LogError(timeoutException, "A timeout error has occurred while attempting to send an email");
                return false;
            }
            catch (ArgumentNullException argumentNullException)
            {
                _logger?.LogError(argumentNullException, "One of the required arguments for sending an email is empty");
                return false;
            }
            catch (SmtpException smtpException)
            {
                _logger?.LogError(smtpException, "An SMTP error has occurred while attempting to send an email");
                return false;
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, "A general error has occurred while attempting to send an email");
                return false;
            }
        }
    }
}
