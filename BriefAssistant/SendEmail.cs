using System.Net;
using System.Net.Mail;
using System.Net.Mime;

namespace BriefAssistant
{
    public class SendEmail
    {
        private SmtpClient client;

        public SendEmail()
        {
            var password = System.IO.File.ReadAllText("password.txt");

            client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("legalbriefassistant@gmail.com", password) //Username and password of the sender
            };
        }  

        /// <summary>
        /// This method sends an email to a given user with the legal brief attached to it.
        /// </summary>
        /// <param name="userEmail">The address of the email being sent</param>
        /// <param name="filePath">The path for the legal brief draft</param>
        public void SendUserEmail(string userEmail, string filePath)
        {
            // Create  the file attachment for this e-mail message.
            Attachment document = new Attachment(filePath, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = document.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(filePath);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(filePath);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(filePath);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("legalbriefassistant@gmail.com");
            mailMessage.To.Add(userEmail); //To be filled in with the users email.
            mailMessage.Body = "Thank you for using the Legal Brief Assistant. Attached is a copy of your Legal Brief Draft.";
            mailMessage.Subject = "Your Legal Brief Draft";
            mailMessage.Attachments.Add(document);
            client.Send(mailMessage);
        }
    }
}
