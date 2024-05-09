using MailtrapClient.Models;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace MailtrapClient
{
    public interface IMailtrapEmailService
    {
        Task<SendEmailResponse> Send(SendEmailRequest request, CancellationToken cancellationToken);

        Task<SendEmailResponse> Send(
            string subject,
            string text,
            string senderEmail,
            string recipientEmail,
            string? senderName = null,
            string? recipientName = null,
            string? html = null,
            IEnumerable<AttachmentInfo>? attachments = null,
            CancellationToken cancellationToken = default);
    }
}
