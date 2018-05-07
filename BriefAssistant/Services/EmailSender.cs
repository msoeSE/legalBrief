using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Razor;
using FluentEmail.Smtp;
using Microsoft.Extensions.Options;

namespace BriefAssistant.Services
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; }

        public EmailSender(IOptions<AuthMessageSenderOptions> options)
        {
            Options = options.Value;
            Email.DefaultSender = new SmtpSender(CreateSmtpClient);
            Email.DefaultRenderer = new RazorRenderer();
        }

        private SmtpClient CreateSmtpClient()
        {
            return new SmtpClient
            {
                Host = "email-smtp.us-east-1.amazonaws.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Options.SmtpUsername, Options.SmtpPassword)
            };
        }

        public async Task SendEmailAsync<T>(string toAddress, string subject, string templateFilename, T model, CancellationToken token = default(CancellationToken))
        {
            await Email.From("no-reply@briefassistant.com", "Brief Assistant")
                .To(toAddress)
                .Subject(subject)
                .UsingTemplateFromFile(templateFilename, model)
                .SendAsync(token);
        }

        public async Task SendEmailWithAttachmentAsync(string toAddress, string subject, string templateFilename, Stream atttachmentStream, string attachmentName, CancellationToken token = default(CancellationToken))
        {
            await Email.From("no-reply@briefassistant.com", "Brief Assistant")
                .To(toAddress)
                .Subject(subject)
                .UsingTemplateFromFile(templateFilename, new {})
                .Attach(new FluentEmail.Core.Models.Attachment()
                {
                    ContentType = "application/octect",
                    Data = atttachmentStream,
                    Filename = attachmentName
                })
                .SendAsync(token);
        }
    }
}
