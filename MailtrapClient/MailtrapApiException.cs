using MailtrapClient.Models;
using System;

namespace MailtrapClient
{
    public class MailtrapApiException : Exception
    {
        public MailtrapApiException(SendEmailErrorResponse body, Exception inner)
            : base($"Mailtrap API has return a non-success status code. Response body: {string.Join("; ", body?.Errors)}", inner)
        {
        }
    }
}
