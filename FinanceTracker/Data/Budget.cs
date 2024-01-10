using FinanceTracker.Data.DateConverter;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceTracker.Data
{
    public class Budget
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Amount { get; set; }

        [JsonConverter(typeof(DateJsonConverter))]
        public DateOnly StartDate { get; set; }
        [JsonConverter(typeof(DateJsonConverter))]
        public DateOnly EndDate { get; set; }

        [ForeignKey("UserId")]
        public Guid UserId { get; set; }
        public User User { get; set; }
    }

}
