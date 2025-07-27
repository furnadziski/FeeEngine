using FeeEngine.Models;

namespace FeeEngine.Services.Interfaces
{
    public interface IRule
    {
        bool IsApplicable(Transaction transaction);
        RuleFeeResult CalculateFee(Transaction transaction);
    
    }
}
  
