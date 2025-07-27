

using FeeEngine.Models;
using FeeEngine.Services.Interfaces;

namespace FeeEngine.Services.Implementations
{
    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly List<TransactionHistory> _history = new();
        private int _currentId=0;
              public void Save(TransactionHistory history)
        {
            history.Id = Interlocked.Increment(ref _currentId);
            history.CalculatedAt = DateTime.UtcNow;
            _history.Add(history);
        }
        List<TransactionHistory> ITransactionHistoryService.GetAll()
        {
            return _history.OrderBy(h => h.Id).ToList();
        }
    }
}
