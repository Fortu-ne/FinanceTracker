using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface IAccounts
    {
        ICollection<Account> GetAccounts();
        bool createAccount(Guid id,Account account);
        bool updateAccount(Account account);
        bool deleteAccount(Account account);

        Account GetAccount(Account account);

        Account GetAccountById(Guid accountID);
        bool findAccount(Guid Id);
        bool Save();
    }
}
