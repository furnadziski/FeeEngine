using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Services.Implementations
{
    public class FeeCalculatorService : IFeeCalculatorService
    {
        private readonly IEnumerable<IRule> _rules;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IEnumerable<IDiscountRule> _discountRules;

        public FeeCalculatorService(IEnumerable<IRule> rules,ITransactionHistoryService transactionHistoryService, IEnumerable<IDiscountRule> discountRules)
        {
            _rules = rules;
            _transactionHistoryService = transactionHistoryService;
            _discountRules = discountRules;
        }

        public FeeCalculationResult CalculateFee( Transaction transaction)
        {
          
            var appliedRules = new List<RuleFeeResult>();
            decimal totalFee = 0;

            foreach (var rule in _rules)
            {
               
                if (rule.IsApplicable(transaction))
                {
                    var result = rule.CalculateFee(transaction);
                    appliedRules.Add(result);
                    totalFee += result.Fee;
                }
            }
            foreach (var dRule in _discountRules)
            {
                if (dRule.IsApplicable(transaction))
                {
                    var discountResult = dRule.ApplyDiscount(totalFee, transaction);
                    appliedRules.Add(discountResult);
                    totalFee += discountResult.Fee;
                }
            }

            _transactionHistoryService.Save(new TransactionHistory
            {
                TransactionId = transaction.TransactionId.ToString(),
                TotalFee = totalFee,
                AppliedRules = string.Join(", ", appliedRules.Select(r => r.RuleName)),
                CalculatedAt = DateTime.UtcNow

            });

            return new FeeCalculationResult
            {
                TotalFee = totalFee,
                AppliedRules = appliedRules
            };
        }
    }
}
