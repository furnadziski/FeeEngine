namespace FeeEngine.Models
{
    public class TransactionAttribute
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public string Type { get; set; }
        public bool IsForeign { get; set; }
    }
}
