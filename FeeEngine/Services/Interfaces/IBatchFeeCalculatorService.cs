using FeeEngine.Models;

namespace FeeEngine.Services.Interfaces
{
    public interface IBatchFeeCalculatorService
    {
               public List<FeeCalculationResult> CalculateBatch(List<Transaction> transactions);

    }
}
