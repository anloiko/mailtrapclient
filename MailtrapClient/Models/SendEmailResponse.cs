using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MailtrapClient.Models
{
    public class SendEmailResponse
    {
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }

        [JsonPropertyName("message_ids")]
        public IEnumerable<string> MessageIds { get; set; }
    }
}
