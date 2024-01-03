using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;

namespace FinanceTracker.Data
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }

        [Column("Email")]
        [EmailAddress]
        public string Email { get; set; }
        //[Column("Date of Birth")]
        //public DateOnly DOB { get; set; }
     
        [Column("Monthly Salary")]
        public Double MonthlySalary { get; set; }

        public List<Budget>? Budgets { get; set; }
        public List<Account>? Accounts { get; set; }
    }
}
