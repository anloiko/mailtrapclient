using System.Text.Json.Serialization;

namespace MailtrapClient.Models
{
    public class AttachmentInfo
    {
        [JsonPropertyName("content")]
        public string Content { get; set; }

        [JsonPropertyName("filename")]
        public string FileName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("disposition")]
        public string Disposition { get; set; }
    }
}
