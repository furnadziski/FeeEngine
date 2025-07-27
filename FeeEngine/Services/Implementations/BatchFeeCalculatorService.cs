using FeeEngine.Models;
using FeeEngine.Rules;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Services.Implementations
    {
        public class BatchFeeCalculatorService :IBatchFeeCalculatorService
        {
            private readonly IEnumerable<IRule> _rules;
            private readonly ITransactionHistoryService _transactionHistoryService;

            public BatchFeeCalculatorService(IEnumerable<IRule> rules, ITransactionHistoryService transactionHistoryService)
            {
                _rules = rules;
                _transactionHistoryService = transactionHistoryService;
            }

            // Приватен метод за пресметка на поединечна трансакција
            private FeeCalculationResult CalculateFee(Transaction transaction)
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

               
                var discountRule = _rules.OfType<RuleCreditScoreDiscount>().FirstOrDefault();
                if (discountRule != null && discountRule.IsApplicable(transaction))
                {
                    var discountResult = discountRule.ApplyDiscount(totalFee, transaction);
                    appliedRules.Add(discountResult);
                    totalFee += discountResult.Fee; // Fee е негативна за попуст
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

            // Јавен метод за batch пресметување со паралелно обработување
            public List<FeeCalculationResult> CalculateBatch(List<Transaction> transactions)
            {
                return transactions
                    .AsParallel()    // Паралелно обработување
                    .AsOrdered()     // По ред да се обработуваат како што се внесени
                    .Select(CalculateFee)
                    .ToList();
            }
        }
    }

