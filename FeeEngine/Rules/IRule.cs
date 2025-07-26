using FeeEngine.Models;

namespace FeeEngine.Rules
{
    public interface IRule
    {
        bool IsApplicable(Transaction transaction, ClientAttribute client);
        RuleFeeResult CalculateFee(Transaction transaction, ClientAttribute client);
    }
}
  
