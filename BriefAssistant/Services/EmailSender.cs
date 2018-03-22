using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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

        public async Task SendEmailAsync(string toAddress, string subject, string message, Stream attachmentStream = null, string attachmentName = null)
        {
            using (var client = new SmtpClient("email-smtp.us-east-1.amazonaws.com", 587))
            {
                client.EnableSsl = true;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(Options.SmtpUsername, Options.SmtpPassword);

                using (var mailMessage = new MailMessage("no-reply@briefassistant.com", toAddress))
                {
                    mailMessage.Subject = subject;
                    mailMessage.Body = message;
                    mailMessage.IsBodyHtml = true;

                    if (attachmentStream != null)
                    {
                        using (var attachment = new Attachment(attachmentStream, "application/octect"))
                        {
                            ContentDisposition disposition = attachment.ContentDisposition;
                            disposition.CreationDate = DateTime.UtcNow;
                            disposition.ModificationDate = DateTime.UtcNow;
                            disposition.ReadDate = DateTime.UtcNow;
                            disposition.FileName = "brief.docx";
                            disposition.Size = attachmentStream.Length;
                            disposition.DispositionType = DispositionTypeNames.Attachment;

                            mailMessage.Attachments.Add(attachment);
                        }
                    }

                    await client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
