using FeeEngine.Models;

namespace FeeEngine.Services.Interfaces
{
    public interface ITransactionHistoryService
    {
        void Save(TransactionHistory history);
        List<TransactionHistory> GetAll();
    }
}
