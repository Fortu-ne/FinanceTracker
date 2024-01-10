using FinanceTracker.Data.DateConverter;
using System.Text.Json.Serialization;

namespace FinanceTracker.Dto
{
    public class TransactionDto
    {
        public String Name { get; set; }

        [JsonConverter(typeof(DateJsonConverter))]
        public DateOnly Date { get; set; }
        public Double Amount { get; set; }
        public Guid AccountId { get; set; }
        public int CategoryId { get; set; }
       
    }

}
