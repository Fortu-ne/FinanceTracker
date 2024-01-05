using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;

namespace FinanceTracker.Repository
{
    public class TransactionRepository : ITransaction
    {
        private readonly DataContext _context;
        public TransactionRepository(DataContext context)
        {
            _context = context;
        }

        public bool create(Transaction transaction)
        {
            if(transaction == null)
                _context.Transactions.Add(transaction);

            return Save();
        }

        public bool delete(Transaction transaction)
        {

            var model = _context.Transactions.Where(r=>r.Id == transaction.Id).FirstOrDefault();

            if(model != null)
            {
                _context.Transactions.Remove(model);
            }

            return Save();
        }

        public bool find(int Id)
        {
           return _context.Transactions.Any(r=>r.Id == Id);
        }

        public Transaction Get(Transaction transaction)
        {
            return _context.Transactions.FirstOrDefault(r=>r.Id == transaction.Id);
        }

        public ICollection<Transaction> GetAll()
        {
            return _context.Transactions.ToList();
        }

        public bool Save()
        {

            var save =_context.SaveChanges();

            return save > 0? true : false;
        }

        public bool update(Transaction transaction)
        {
            _context.Update(transaction);
            return Save();
        }
    }
}
