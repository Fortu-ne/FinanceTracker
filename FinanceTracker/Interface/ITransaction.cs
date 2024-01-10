using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface ITransaction
    {
        ICollection<Transaction> GetAll();
        bool create(Transaction transaction);
        bool update(Transaction transaction);
        bool delete(Transaction transaction);

        Transaction Get(Transaction transaction);

        Transaction GetTransactionById(int transID);
        bool find(int Id);
        bool Save();
    }
}
