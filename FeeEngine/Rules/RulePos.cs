using FeeEngine.Enums;
using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Rules
{
    public class RulePos : IRule
    {
        public bool IsApplicable(Transaction transaction)
        {
            return transaction.TransactionAttribute.Type == TransactionType.POS;
        }
        
        public RuleFeeResult CalculateFee(Transaction transaction)
        {
            decimal fee = transaction.TransactionAttribute.Amount <= 100 ? 0.2m : transaction.TransactionAttribute.Amount * 0.002m;

            return new RuleFeeResult
            {
                RuleName = transaction.TransactionAttribute.Type.ToString(),
                Fee = fee,
                 Description = transaction.TransactionAttribute.Amount <= 100
                ? "Fixed fee 0.20€ for POS ≤ 100€"
                : $"0.2% fee for POS amount {transaction.TransactionAttribute.Amount}"
            };
        }

    }
}
