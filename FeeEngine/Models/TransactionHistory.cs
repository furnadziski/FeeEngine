namespace FeeEngine.Models
{
    public class TransactionHistory
    {
        public string TransactionId { get; set; }
        public decimal TotalFee { get; set; }
        public string AppliedRules { get; set; } 
        public DateTime CalculatedAt { get; set; }
    }
}
