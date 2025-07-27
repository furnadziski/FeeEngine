using FeeEngine.Models;

namespace FeeEngine.Rules
{
    public interface IRule
    {
        bool IsApplicable(Transaction transaction);
        RuleFeeResult CalculateFee(Transaction transaction);
    }
}
  
