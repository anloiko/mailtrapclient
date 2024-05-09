using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MailtrapClient.Models
{
    public class SendEmailErrorResponse
    {
        [JsonPropertyName("errors")]
        public IEnumerable<string> Errors { get; set; }
    }
}
