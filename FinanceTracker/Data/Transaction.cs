using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Data
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Name { get; set; }
        public DateTime Date { get; set; }
        public Double Amount { get; set; }

        [ForeignKey("Account")]
        public Guid AccountsId { get; set; }
        public Account Accounts { get; set; }

        [ForeignKey("Category")]
        public int CategorysId { get; set; }
        public Category Categorys { get; set; }
    }

}
