﻿using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace BriefAssistant.Services
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> options)
        {
            Options = options.Value;
        }

        public Task SendEmailAsync(string toAddress, string subject, string message)
        {
            using (var client = new SmtpClient("smtp.gmail.com", 465))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Options.EmailAddress, Options.Password);

                using (var mailMessage = new MailMessage(Options.EmailAddress, toAddress))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    return client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
