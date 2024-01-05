using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repository
{
    public class AccountRepository : IAccounts
    {

        private readonly DataContext _context;
        public AccountRepository(DataContext context)
        {
            _context = context;
        }

        public bool createAccount(Guid id,Account account)
        {
            var model = _context.Users.Where(r => r.Id == id).FirstOrDefault();
            account.User = model;
            _context.Accounts.Add(account);
            return Save();
        }

        public bool deleteAccount(Account account)
        {
           if(findAccount(account.Id))
            {
                var model = GetAccountById(account.Id);
                _context.Accounts.Remove(account);
              
            }

            return Save();

        }

        public bool findAccount(Guid Id)
        {
            return _context.Accounts.Any(a => a.Id == Id);
        }


        public Account GetAccount(Account account)
        {
            return _context.Accounts.Where(r => r.AccountName == account.AccountName).FirstOrDefault();
        }

        public Account GetAccountById(Guid accountID)
        {
            return _context.Accounts.Where(r=>r.Id == accountID).FirstOrDefault();
        }

        public ICollection<Account> GetAccounts()
        {
            return _context.Accounts.Include(r=>r.User).ToList();
        }

        public bool Save()
        {
           var save = _context.SaveChanges();

            return save > 0? true: false;
        }

        public bool updateAccount(Account account)
        {
            _context.Accounts.Update(account);
            return Save();
        }
    }
}
