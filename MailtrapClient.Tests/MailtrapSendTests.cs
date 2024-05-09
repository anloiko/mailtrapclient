using MailtrapClient.Models;
using MailtrapClient.Services;
using Refit;
using System.Net.Http.Headers;
using static System.Net.Mime.MediaTypeNames;

namespace MailtrapClient.Tests
{
    public class MailtrapSendTests
    {
        private const string AuthenticationType = "Bearer";
        private const string ApiToken = "9fb679db6bc8f1f69d773cde52896917";
        private const string RecipientEmail = "PLEASE PROVIDER YOUR RECIPIENT EMAIL!";

        private readonly MailtrapEmailService _mailtrapEmailService;

        public MailtrapSendTests()
        {
            var client = new HttpClient(new HttpClientHandler())
            {
                BaseAddress = new Uri("https://send.api.mailtrap.io/api")
            };

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthenticationType, ApiToken);

            var builder = RequestBuilder.ForType<IMailtrapClient>();
            var mailtrapClient = RestService.For(client, builder);

            _mailtrapEmailService = new MailtrapEmailService(mailtrapClient);
        }

        [Fact]
        public async Task SendEmailWithoutAttachmentSuccess()
        {
            //arrange
            var emailRequest = new SendEmailRequest()
            {
                From = new PersonInfo() { Email = "mailtrap@demomailtrap.com", Name = "Mailtrap Test" },
                To = new[] { new PersonInfo() { Email = RecipientEmail } },
                Subject = "New Email WITHOUT attachments", 
                Text = "Hello you received new email WITHOUT attachments. Congrats!"
            };

            //act
            var result = await _mailtrapEmailService.Send(emailRequest, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task SendEmailWithAttachmentSuccess()
        {
            //arrange
            var emailRequest = new SendEmailRequest()
            {
                From = new PersonInfo() { Email = "mailtrap@demomailtrap.com", Name = "Mailtrap Test" },
                To = new[] { new PersonInfo() { Email = RecipientEmail } },
                Subject = "New Email WITH attachments",
                Text = "Hello you received new email WITH attachments. Congrats!",
                Attachments = new[] { new AttachmentInfo()
                {
                    Content = "PCFET0NUWVBFIGh0bWw+CjxodG1sIGxhbmc9ImVuIj4KCiAgICA8aGVhZD4KICAgICAgICA8bWV0YSBjaGFyc2V0PSJVVEYtOCI+CiAgICAgICAgPG1ldGEgaHR0cC1lcXVpdj0iWC1VQS1Db21wYXRpYmxlIiBjb250ZW50PSJJRT1lZGdlIj4KICAgICAgICA8bWV0YSBuYW1lPSJ2aWV3cG9ydCIgY29udGVudD0id2lkdGg9ZGV2aWNlLXdpZHRoLCBpbml0aWFsLXNjYWxlPTEuMCI+CiAgICAgICAgPHRpdGxlPkRvY3VtZW50PC90aXRsZT4KICAgIDwvaGVhZD4KCiAgICA8Ym9keT4KCiAgICA8L2JvZHk+Cgo8L2h0bWw+Cg==",
                    Disposition = "attachment",
                    FileName = "index.html",
                    Type = "text/html"
                } }
            };

            //act
            var result = await _mailtrapEmailService.Send(emailRequest, CancellationToken.None);

            //assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }

        [Fact]
        public async Task SendEmailError()
        {
            //arrange
            var emailRequest = new SendEmailRequest()
            {
                From = new PersonInfo() { Email = "mailtrap@test.com", Name = "Mailtrap Test" },
                To = new[] { new PersonInfo() { Email = RecipientEmail } },
                Subject = "New FAILED Email",
                Text = "Hello you SHOULDN'T receive this email. Else your test was FAILED!"
            };

            //act
            //assert
            await Assert.ThrowsAsync<MailtrapApiException>(async() => await _mailtrapEmailService.Send(emailRequest, CancellationToken.None));
        }

        [Fact]
        public async Task SendMinimumEmailSuccess()
        {
            //arrange
            //act
            var result = await _mailtrapEmailService.Send(
                subject: "New minimum Email",
                text: "Hello you received minimum email. Congrats!",
                senderEmail: "mailtrap@demomailtrap.com",
                recipientEmail: RecipientEmail);

            //assert
            Assert.NotNull(result);
            Assert.True(result.IsSuccess);
        }
    }
}