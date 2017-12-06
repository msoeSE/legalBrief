using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace BriefAssistant.Services
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string email, string link)
        {
            var message = $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>";
            return emailSender.SendEmailAsync(email, "Confirm your email", message);
        }
    }
}
