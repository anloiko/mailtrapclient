using System.Text.Json.Serialization;

namespace MailtrapClient.Models
{
    public class PersonInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
    }
}
