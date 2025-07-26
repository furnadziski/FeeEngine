namespace FeeEngine.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public TransactionAttribute TransactionAttribute { get; set; }
        public ClientAttribute ClientAttribute { get; set; }
    }
}
