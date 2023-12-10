using FinanceTracker.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinanceTracker.Dto
{
    public class BudgetDto
    {
        public String Name { get; set; }
        public Double Amount { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }


    }

}
