using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using WebClient.BookStore.Models;
using System.Text;
namespace WebClient.BookStore.Service
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplate/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;
        }
        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, this is test email from Book Store Application",userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("TestEmail"),userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }


        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, this is test email from Book Store Application", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("EmailConfirm"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForgotPassword(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceHolders("Hello {{UserName}}, reset password link.", userEmailOptions.PlaceHolders);
            userEmailOptions.Body = UpdatePlaceHolders(GetEmailBody("ForgotPassword"), userEmailOptions.PlaceHolders);
            await SendEmail(userEmailOptions);
        }

        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            NetworkCredential networkCredentials = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpClient = new SmtpClient
            {
                Port = _smtpConfig.Port,
                Host = _smtpConfig.Host,
                EnableSsl = _smtpConfig.EnableSSL,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredentials
            };
            foreach (var toEmail in userEmailOptions.ToMails) { mail.To.Add(toEmail); }
            mail.BodyEncoding = Encoding.Default;
            await smtpClient.SendMailAsync(mail);


        }
        private string GetEmailBody(string TemplateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, TemplateName));
            return body;
        }

        private string UpdatePlaceHolders(string text,List<KeyValuePair<string,string>> keyValuePairs) {
            if (!String.IsNullOrEmpty(text) && keyValuePairs != null) {
                foreach (var placeholder in keyValuePairs)
                {
                    if (text.Contains(placeholder.Key)) 
                    {
                        text = text.Replace(placeholder.Key,placeholder.Value);
                    }
                }
            }
            return text;
        }
    }
}
