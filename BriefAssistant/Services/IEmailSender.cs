using System.IO;
using System.Threading.Tasks;

namespace BriefAssistant.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toAddress, string subject, string message, Stream attachmentStream = null, string attachmentFilename = null);
    }
}
