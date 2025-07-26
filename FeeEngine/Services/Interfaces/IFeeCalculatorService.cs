using FeeEngine.Models;

namespace FeeEngine.Services.Interfaces
{
    public interface IFeeCalculatorService
    {
        public FeeCalculationResult CalculateFee(Transaction transaction);
    }
}
