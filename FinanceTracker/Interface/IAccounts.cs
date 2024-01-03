using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface IAccounts
    {
        List<Account> GetAccounts();
        bool createAccount(Account account);
        bool updateAccount(Account account);
        bool deleteAccount(Account account);

        Account GetAccount(Account account);
        bool findAccount(int Id);
        bool Save();
    }
}
