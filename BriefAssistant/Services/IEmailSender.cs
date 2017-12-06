using System.Threading.Tasks;

namespace BriefAssistant.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string message);
    }
}
