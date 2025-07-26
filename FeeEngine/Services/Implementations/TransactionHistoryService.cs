

using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Services.Implementations
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly List<TransactionHistory> _history = new();
              public void Save(TransactionHistory history)
        {
            history.Id = _history.Count + 1;
            history.CalculatedAt = DateTime.UtcNow;
            _history.Add(history);
        }
        List<TransactionHistory> ITransactionHistoryService.GetAll()
        {
            return _history;
        }
    }
}
