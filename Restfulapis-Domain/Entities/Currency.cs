using System.Text.Json.Serialization;

namespace Restfulapis_Domain.Entities
{
    public sealed class Currency
    {
        [JsonPropertyName("timestamp")]
        public long Timestamp { get; set; }

        [JsonPropertyName("base")]
        public string Base { get; set; }

        [JsonPropertyName("date")]
        public DateOnly Date { get; set; }

        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; } = new Dictionary<string, decimal>();
    }
}
