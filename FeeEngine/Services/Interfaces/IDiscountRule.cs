using FeeEngine.Models;

namespace FeeEngine.Services.Interfaces
{
    public interface IDiscountRule
    {
        bool IsApplicable(Transaction transaction);
        RuleFeeResult ApplyDiscount(decimal totalFee, Transaction transaction);
    }
}
