using FeeEngine.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace FeeEngine.Rules
{
    public class RuleEcommerce : IRule
    {
        public bool IsApplicable(Transaction transaction, ClientAttribute client)
        {
            return transaction.TransactionAttribute.Type == "ECOM";
        }
        public RuleFeeResult CalculateFee(Transaction transaction, ClientAttribute client)
        {
            decimal rate = 0.18m;
            decimal addOns = 0.15m;
            decimal initialFee = transaction.TransactionAttribute.Amount * rate + addOns;
            decimal fee = initialFee > 120 ? 120m : initialFee;

            return new RuleFeeResult
            {
                RuleName = transaction.TransactionAttribute.Type,
                Fee = fee,
                Description = transaction.TransactionAttribute.Amount >120? "Maximum fee of 120":"0.18% of amount + 0.15"
            };
        }
    }
}
