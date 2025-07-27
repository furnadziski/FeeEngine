using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Services.Implementations
    {
        public class BatchFeeCalculatorService :IBatchFeeCalculatorService
        {
       
        private readonly IFeeCalculatorService _feeCalculatorService;

            public BatchFeeCalculatorService (IFeeCalculatorService feeCalculatorService)
            {
                
            _feeCalculatorService = feeCalculatorService;
            }

                // Јавен метод за batch пресметување со паралелно обработување
            public List<FeeCalculationResult> CalculateBatch(List<Transaction> transactions)
            {
                return transactions
                    .AsParallel()    // Паралелно обработување
                    .AsOrdered()     // По ред да се обработуваат како што се внесени
                    .Select(_feeCalculatorService.CalculateFee)
                    .ToList();
            }
        }
    }

