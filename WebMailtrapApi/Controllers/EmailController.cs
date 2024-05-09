using MailtrapClient;
using MailtrapClient.Models;
using Microsoft.AspNetCore.Mvc;
using WebMailtrapApi.Models;

namespace WebMailtrapApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private const string RecipientEmail = "PLEASE PROVIDER YOUR RECIPIENT EMAIL!";

        private readonly IMailtrapEmailService _mailtrapEmailService;

        public EmailController(IMailtrapEmailService mailtrapEmailService)
        {
            _mailtrapEmailService = mailtrapEmailService;
        }

        [HttpPost]
        public Task<SendEmailResponse> SendEmail(EmailInfo emailInfo, CancellationToken ct)
        {
            var emailRequest = new SendEmailRequest()
            {
                From = new PersonInfo() { Email = "mailtrap@demomailtrap.com", Name = "Mailtrap Test" },
                To = new[] { new PersonInfo() { Email = RecipientEmail } },
                Subject = emailInfo.Subject,
                Text = emailInfo.Text,
                Attachments = new[] { new AttachmentInfo() 
                { 
                    Content = "PCFET0NUWVBFIGh0bWw+CjxodG1sIGxhbmc9ImVuIj4KCiAgICA8aGVhZD4KICAgICAgICA8bWV0YSBjaGFyc2V0PSJVVEYtOCI+CiAgICAgICAgPG1ldGEgaHR0cC1lcXVpdj0iWC1VQS1Db21wYXRpYmxlIiBjb250ZW50PSJJRT1lZGdlIj4KICAgICAgICA8bWV0YSBuYW1lPSJ2aWV3cG9ydCIgY29udGVudD0id2lkdGg9ZGV2aWNlLXdpZHRoLCBpbml0aWFsLXNjYWxlPTEuMCI+CiAgICAgICAgPHRpdGxlPkRvY3VtZW50PC90aXRsZT4KICAgIDwvaGVhZD4KCiAgICA8Ym9keT4KCiAgICA8L2JvZHk+Cgo8L2h0bWw+Cg==",
                    Disposition = "attachment",
                    FileName = "index.html",
                    Type = "text/html"
                } }
            };

            return _mailtrapEmailService.Send(emailRequest, ct);
        }
    }
}
