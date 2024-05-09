using MailtrapClient.Models;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace MailtrapClient.Services
{
    [Headers("Content-Type: application/json")]
    public interface IMailtrapClient
    {
        [Post("/send")]
        Task<SendEmailResponse> Send([Body] SendEmailRequest request, CancellationToken ct);
    }
}
