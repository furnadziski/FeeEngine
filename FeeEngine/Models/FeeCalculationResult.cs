namespace FeeEngine.Models
{
    public class FeeCalculationResult
    {
        public decimal TotalFee { get; set; }
        public List<RuleFeeResult> AppliedRules { get; set; }
    }
}
