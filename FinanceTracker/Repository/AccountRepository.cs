using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;

namespace FinanceTracker.Repository
{
    public class AccountRepository : IAccounts
    {

        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public bool createAccount(Account account)
        {
            if(account == null)
            {
                _context.Accounts.Add(account);

            }

            return Save();
        }

        public bool deleteAccount(Account account)
        {
            var model = _context.Accounts.FirstOrDefault(c => c.Id == account.Id);
            if (model != null)
            {
                _context.Accounts.Remove(account);
            
            }

           return Save();

        }

        public bool findAccount(int Id)
        {
            return _context.Accounts.Any(a => a.Id == Id);
        }


        public Account GetAccount(Account account)
        {
            return _context.Accounts.Where(r => r.AccountName == account.AccountName).FirstOrDefault();
        }

        public List<Account> GetAccounts()
        {
            return _context.Accounts.ToList();
        }

        public bool Save()
        {
           var save = _context.SaveChanges();

            return save > 0? true: false;
        }

        public bool updateAccount(Account account)
        {
            _context.Update(account);
            return Save();
        }
    }
}
