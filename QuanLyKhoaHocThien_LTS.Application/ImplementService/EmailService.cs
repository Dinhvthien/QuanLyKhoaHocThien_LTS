using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using QuanLyKhoaHocThien_LTS.Application.HandleEmail;
using QuanLyKhoaHocThien_LTS.Application.InterfaceServices;
using QuanLyKhoaHocThien_LTS.Application.Payloads.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyKhoaHocThien_LTS.Application.ImplementService
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        private readonly EmailConfiguration _emailConfiguration;

        public EmailService(EmailConfiguration emailConfiguration)
        {
            _emailConfiguration = emailConfiguration;
        }

        public string SendEmail(EmailMessage emailMs)
        {
            try
            {
                var message = createEmailMessage(emailMs);
                Send(message);
                var recipients = string.Join(", ", message.To);
                return ResponseMessage.getEmailSuccessMessage(recipients);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private MimeMessage createEmailMessage(EmailMessage emailMessage)
        {
            try
            {
                var emailMs = new MimeMessage();
                emailMs.From.Add(new MailboxAddress("email", _emailConfiguration.From));
                emailMs.To.AddRange(emailMessage.To);
                emailMs.Subject = emailMessage.Subject;
                emailMs.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = emailMessage.Content };
                return emailMs;
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void Send(MimeMessage message)
        {
            using var client = new SmtpClient();
            try
            {
                client.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, true);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate(_emailConfiguration.From, _emailConfiguration.Password);
                client.Send(message);
            }
            catch
            {

                throw;
            }
            finally
            {
                client.Disconnect(true);
                client.Dispose();
            }
        }
    }
}
