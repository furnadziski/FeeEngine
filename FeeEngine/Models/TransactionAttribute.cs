using FeeEngine.Enums;
using System.Text.Json.Serialization;

namespace FeeEngine.Models
{
    public class TransactionAttribute
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TransactionType Type { get; set; }
        public bool IsForeign { get; set; }
    }
}
