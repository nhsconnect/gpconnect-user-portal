using Dapper;
using gpconnect_user_portal.DAL.Interfaces;
using gpconnect_user_portal.DTO.Request;
using gpconnect_user_portal.DTO.Response.Application;
using gpconnect_user_portal.Helpers;
using gpconnect_user_portal.Services.Enumerations;
using gpconnect_user_portal.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace gpconnect_user_portal.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;
        private readonly SmtpClient _smtpClient;
        private readonly IOptionsMonitor<DTO.Response.Configuration.Email> _emailOptionsDelegate;
        private readonly IOptionsMonitor<DTO.Response.Configuration.General> _generalOptionsDelegate;
        private readonly IDataService _dataService;
        private readonly IHttpContextAccessor _contextAccessor;

        public EmailService(IHttpContextAccessor contextAccessor, SmtpClient smtpClient, IOptionsMonitor<DTO.Response.Configuration.General> generalOptionsDelegate, IOptionsMonitor<DTO.Response.Configuration.Email> emailOptionsDelegate, ILogger<EmailService> logger, IDataService dataService)
        {
            _logger = logger;
            _emailOptionsDelegate = emailOptionsDelegate;
            _generalOptionsDelegate = generalOptionsDelegate;
            _smtpClient = smtpClient;
            _dataService = dataService;
            _contextAccessor = contextAccessor;
        }

        public async Task SendSiteNotificationEmail(EmailDefinition emailDefinition)
        {            
            var email = await GetEmailTemplate(MailTemplate.SendSiteNotificationEmail);                        
            if (email != null)
            {
                email.Body = email.Body.Replace("<site_definition>", emailDefinition.SiteDefinition.ExportDataTableToHTML());
                email.Body = email.Body.Replace("<site_attributes>", emailDefinition.SiteAttributes.ExportDataTableToHTML());
                //SendEmail(email, true, siteDefinition.SiteDefinitionStatusId == (int)SiteDefinitionStatus.Draft);
                SendEmail(email, true, true);
            }
        }

        private void SendEmail(Email email, bool sendToSender = false, bool sendToRecipient = true)
        {            
            var sender = _emailOptionsDelegate.CurrentValue.SenderAddress;
            var displayName = StringExtensions.Coalesce(_generalOptionsDelegate.CurrentValue.ProductName, _emailOptionsDelegate.CurrentValue.SenderAddress);
            try
            {
                email.Body = PopulateDynamicFields(email.Body);
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(sender, displayName),
                    IsBodyHtml = true,
                    Subject = StringExtensions.Coalesce(email.Subject, _emailOptionsDelegate.CurrentValue.DefaultSubject),
                    Body = email.Body
                };
                if (sendToSender) mailMessage.To.Add(sender);
                if (sendToRecipient) mailMessage.To.Add(email.MailRecipient);
                _smtpClient.Send(mailMessage);
            }
            catch (Exception exception)
            {
                _logger?.LogError(exception, "An error has occurred while attempting to send an email");
            }
        }

        private string PopulateDynamicFields(string body)
        {
            body = body.Replace("<url>", _contextAccessor.HttpContext.GetBaseSiteUrl());
            return body;
        }

        private async Task<Email> GetEmailTemplate(MailTemplate mailTemplate)
        {            
            var query = "application.get_email_template";
            var parameters = new DynamicParameters();
            parameters.Add("_mail_template_id", (int)mailTemplate, DbType.Int16, ParameterDirection.Input);
            var result = await _dataService.ExecuteQueryFirstOrDefault<Email>(query, parameters);
            return result;
        }
    }
}
