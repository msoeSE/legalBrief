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

        public Task SendEmailAsync(string toAddress, string subject, string message, string attachmentFileName = null)
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

                    if (attachmentFileName != null)
                    {
                        using (var attachment = new Attachment(attachmentFileName))
                        {
                            var fileInfo = new FileInfo(attachmentFileName);
                            ContentDisposition disposition = attachment.ContentDisposition;
                            disposition.CreationDate = fileInfo.CreationTimeUtc;
                            disposition.ModificationDate = fileInfo.LastWriteTimeUtc;
                            disposition.ReadDate = fileInfo.LastAccessTimeUtc;
                            disposition.FileName = fileInfo.Name;
                            disposition.Size = fileInfo.Length;
                            disposition.DispositionType = DispositionTypeNames.Attachment;

                            mailMessage.Attachments.Add(attachment);
                        }
                    }

                    return client.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
