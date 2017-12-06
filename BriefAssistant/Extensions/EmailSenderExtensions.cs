using System.Text.Encodings.Web;
using System.Threading.Tasks;
using BriefAssistant.Services;

namespace BriefAssistant.Extensions
{
    public static class EmailSenderExtensions
    {
        public static Task SendEmailConfirmationAsync(this IEmailSender emailSender, string emailAddress, string link)
        {
            var message = $"Please confirm your account by clicking this link: <a href='{HtmlEncoder.Default.Encode(link)}'>link</a>";
            return emailSender.SendEmailAsync(emailAddress, "Confirm your email", message);
        }

        public static Task SendBriefAsync(this IEmailSender emailSender, string emailAddress, string briefFileName)
        {
            var message = "Your completed brief is attached. Thank you for using the Brief Assistant";
            return emailSender.SendEmailAsync(emailAddress, "Your completed brief", message, briefFileName);
        }
    }
}
