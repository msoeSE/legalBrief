using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace BriefAssistant.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync<T>(string toAddress, string subject, string templateFilename, T model, CancellationToken token = default(CancellationToken));
        Task SendEmailWithAttachmentAsync(string toAddress, string subject, string templateFilename, Stream atttachmentStream, string attachmentName, CancellationToken token = default(CancellationToken));
    }
}
