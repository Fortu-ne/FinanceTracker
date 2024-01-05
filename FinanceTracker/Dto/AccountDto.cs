using FinanceTracker.Data;

namespace FinanceTracker.Dto
{
    public class AccountDto
    {
        public string AccountName { get; set; }
        public Double Balance { get; set; }
        public AccountType AccountType { get; set; }

        public Guid UserId { get; set; }
    }
}
