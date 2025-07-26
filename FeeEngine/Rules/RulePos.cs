using FeeEngine.Models;

namespace FeeEngine.Rules
{
    public class RulePos : IRule
    {
        public bool IsApplicable(Transaction transaction, ClientAttribute client)
        {
            return transaction.TransactionAttribute.Type == "POS";
        }
        
        public RuleFeeResult CalculateFee(Transaction transaction, ClientAttribute client)
        {
            decimal fee = transaction.TransactionAttribute.Amount <= 100 ? 0.20m : transaction.TransactionAttribute.Amount * 0.02m;

            return new RuleFeeResult
            {
                RuleName = transaction.TransactionAttribute.Type,
                Fee = fee,
                 Description = transaction.TransactionAttribute.Amount <= 100
                ? "Fixed fee 0.20€ for POS ≤ 100€"
                : $"0.2% fee for POS amount {transaction.TransactionAttribute.Amount}"
            };
        }

    }
}
