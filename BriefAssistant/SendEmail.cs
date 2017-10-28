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
        public SendEmail(string userEmail, string filePath)
        {
            string password = System.IO.File.ReadAllText("password.txt");
            
            // Create  the file attachment for this e-mail message.
            Attachment document = new Attachment(filePath, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = document.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("legalbriefassistant@gmail.com", password) //Username and password of the sender
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("legalbriefassistant@gmail.com");
            mailMessage.To.Add(userEmail); //To be filled in with the users email.
            mailMessage.Body = "Attached is your legal brief draft.";
            mailMessage.Subject = "Your Legal Brief Draft";
            mailMessage.Attachments.Add(document);
            client.Send(mailMessage);
        }  
    }
}
