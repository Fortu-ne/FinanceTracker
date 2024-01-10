using FinanceTracker.Data.DateConverter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceTracker.Data
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Name { get; set; }

        [JsonConverter(typeof(DateJsonConverter))]
        public DateOnly Date { get; set; }
        public Double Amount { get; set; }

        [ForeignKey("AccountId")]
        public Guid AccountId    { get; set; }
        public Account Account { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
