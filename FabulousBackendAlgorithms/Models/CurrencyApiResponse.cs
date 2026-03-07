using System.Text.Json.Serialization;

namespace FabulousBackendAlgorithms.Models
{
    public class CurrencyApiResponse
    {
        [JsonPropertyName("data")]
        public Dictionary<string, decimal> Data { get; init; } = new();
    }
}
