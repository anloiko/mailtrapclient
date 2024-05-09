using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MailtrapClient.Models
{
    public class SendEmailRequest
    {
        [JsonPropertyName("to")]
        public IEnumerable<PersonInfo> To { get; set; }

        [JsonPropertyName("from")]
        public PersonInfo From { get; set; } 

        [JsonPropertyName("subject")]
        public string Subject { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("html")]
        public string? Html { get; set; }

        [JsonPropertyName("attachments")]
        public IEnumerable<AttachmentInfo>? Attachments { get; set; }
    }
}
