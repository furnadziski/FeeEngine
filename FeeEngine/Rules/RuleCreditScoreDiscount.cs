using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Rules
{
    //посебен интерфејс за дискаунти
    public class RuleCreditScoreDiscount :IDiscountRule
    {
        public bool IsApplicable(Transaction transaction)
        {

            return transaction.ClientAttribute.CreditScore > 400;
        }
           
        public RuleFeeResult ApplyDiscount(decimal totalFee, Transaction transaction)
        {
            decimal discountAmount = totalFee * 0.01m;

            return new RuleFeeResult
            {
                RuleName = "Credit Score > 400 Discount",
                Fee = -discountAmount,
                Description = $"1% discount applied for credit score >400 ({transaction.ClientAttribute.CreditScore})"
            };
        }
    }

}