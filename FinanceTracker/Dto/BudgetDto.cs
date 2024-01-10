using FinanceTracker.Data;
using FinanceTracker.Data.DateConverter;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace FinanceTracker.Dto
{
    public class BudgetDto
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public Double Amount { get; set; }
       // [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly StartDate { get; set; }
        //[JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly EndDate { get; set; }

        public Guid UserId { get; set; }
    }
}
