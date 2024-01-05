using FinanceTracker.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinanceTracker.Interface
{
    public interface IBudget
    {
         ICollection<Budget> GetBudgets();
         bool createBudget(Guid id,Budget budget);
         bool updateBudget(Budget budget);
         bool findBudget(int Id);
         bool deleteBudget(Budget budget);

        Budget GetBudget(Budget budget);

        Budget GetBudgetById(int budgetID);
        bool Save();
    }
}
