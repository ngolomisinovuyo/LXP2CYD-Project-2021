using LXP2CYD.Email.Dto;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace LXP2CYD.Email
{
    public class EmailAppService : IEmailAppService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";

        private readonly SMTPConfigModel _smtpConfig;
        public EmailAppService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task Send(UserEmailOptionsDto userEmailOptions)
        {
            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailConfirmation(UserEmailOptionsDto userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{UserName}}, this is an account confirmation email", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("ConfirmEmail"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendEqnuiryResponseEmail(UserEmailOptionsDto userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{UserName}}", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("UserEmailResponse"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }

        public async Task SendForgotPasswordEmail(UserEmailOptionsDto userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{UserName}}, reset your password here", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);

            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptionsDto userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach (var toEmail in userEmailOptions.ToEmails)
            {
                mail.To.Add(toEmail);
            }

            foreach (var ccEmail in userEmailOptions.CcEmails)
            {
                mail.CC.Add(ccEmail);
            }

            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);

            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential
            };

            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }

        private string UpdatePlaceholders(string text, List<KeyValuePair<string, string>> keyValuePairs)
        {
            if (!string.IsNullOrEmpty(text) && keyValuePairs != null)
                foreach (var placeholder in keyValuePairs)
                    if (text.Contains(placeholder.Key))
                        text = text.Replace(placeholder.Key, placeholder.Value);
            return text;

        }
    }
}
