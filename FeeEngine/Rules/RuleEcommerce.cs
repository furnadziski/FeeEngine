using FeeEngine.Enums;
using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Rules
{
    public class RuleEcommerce : IRule
    {
        public bool IsApplicable(Transaction transaction)
        {
            return transaction.TransactionAttribute.Type ==TransactionType.ECOMERCE;
        }
        public RuleFeeResult CalculateFee(Transaction transaction)
        {
            decimal rate = 0.018m;
            decimal addOns = 0.15m;
            decimal initialFee = transaction.TransactionAttribute.Amount * rate + addOns;
            decimal fee = initialFee > 120 ? 120m : initialFee;

            return new RuleFeeResult
            {
                RuleName = transaction.TransactionAttribute.Type.ToString(),
                Fee = fee,
                Description = transaction.TransactionAttribute.Amount >120? "Maximum fee of 120":"1.8% of amount + 0.15"
            };
        }
    }
}
