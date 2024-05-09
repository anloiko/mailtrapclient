using System.Threading.Tasks;
using System.Threading;
using MailtrapClient.Models;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

[assembly: InternalsVisibleTo("MailtrapClient.Tests")]
namespace MailtrapClient.Services
{
    internal class MailtrapEmailService : IMailtrapEmailService
    {
        private readonly IMailtrapClient _mailtrapClient;

        public MailtrapEmailService(IMailtrapClient mailtrapClient)
        {
            _mailtrapClient = mailtrapClient;
        }

        public Task<SendEmailResponse> Send(
            string subject,
            string text,
            string senderEmail,
            string recipientEmail,
            string? senderName = null,
            string? recipientName = null, 
            string? html = null, 
            IEnumerable<AttachmentInfo>? attachments = null, 
            CancellationToken cancellationToken = default)
        {
            var request = new SendEmailRequest()
            {
                Subject = subject,
                Text = text,
                From = new PersonInfo() { Email = senderEmail, Name = senderName },
                To = new[] { new PersonInfo() { Email = recipientEmail, Name = recipientName } },
                Html = html,
                Attachments = attachments
            };

            return Send(request, cancellationToken);
        }

        public async Task<SendEmailResponse> Send(SendEmailRequest request, CancellationToken cancellationToken)
        {
            try
            {
                return await _mailtrapClient.Send(request, cancellationToken);
            }
            catch (Refit.ApiException ex)
            {
                var responseBody = await ex.GetContentAsAsync<SendEmailErrorResponse>();
                throw new MailtrapApiException(responseBody, ex);
            }
        }
    }
}
 