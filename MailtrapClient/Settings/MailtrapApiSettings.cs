using System;

namespace MailtrapClient.Settings
{
    public class MailtrapApiSettings
    {
        public Uri BaseAddress { get; set; }
        public string ApiToken { get; set; }
        public int RetryCount { get; set; } = 5;
        public int RetryWait { get; set; } = 10;
    }
}
