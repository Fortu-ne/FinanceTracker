using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Data
{
    public class Account
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 
        public string AccountName { get; set; }
        public Double Balance { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction> Transaction { get; set; }

    }
}
