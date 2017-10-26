using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace BriefAssistant
{
    public class SendEmail
    {
        public SendEmail(string user_email)
        {
            string file = "legalbrief.docx";
            // Create  the file attachment for this e-mail message.
            Attachment document = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = document.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("username", "password") //Username and password of the sender
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("sender@email.com");
            mailMessage.To.Add(user_email); //To be filled in with the users email.
            mailMessage.Body = "Attached is your legal brief draft.";
            mailMessage.Subject = "Your Legal Brief Draft";
            mailMessage.Attachments.Add(document);
            client.Send(mailMessage);
        }  
    }
}
