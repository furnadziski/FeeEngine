using FeeEngine.Models;
using FeeEngine.Rules;
using FeeEngine.Services.Interfaces;
using Nancy;
using Nancy.Json;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace FeeEngine.Services.Implementations
{
    public class FeeCalculatorService : IFeeCalculatorService
    {
        private readonly IEnumerable<IRule> _rules;
        private readonly ITransactionHistoryService _transactionHistoryService;

        public FeeCalculatorService(IEnumerable<IRule> rules,ITransactionHistoryService transactionHistoryService)
        {
            _rules = rules;
            _transactionHistoryService = transactionHistoryService;
            
        }

        public FeeCalculationResult CalculateFee( Transaction transaction)
        {
          
            var appliedRules = new List<RuleFeeResult>();
            decimal totalFee = 0;

            foreach (var rule in _rules)
            {
                if (rule is RuleCreditScoreDiscount) continue;
                if (rule.IsApplicable(transaction, transaction.ClientAttribute))
                {
                    var result = rule.CalculateFee(transaction, transaction.ClientAttribute);
                    appliedRules.Add(result);
                    totalFee += result.Fee;
                }
            }
            var discountRule = _rules.OfType<RuleCreditScoreDiscount>().FirstOrDefault();
            if (discountRule != null && discountRule.IsApplicable(transaction, transaction.ClientAttribute))
            {
                var discountResult = discountRule.ApplyDiscount(totalFee, transaction);
                appliedRules.Add(discountResult);
                totalFee += discountResult.Fee; // Fee is negative (it's a discount)
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
