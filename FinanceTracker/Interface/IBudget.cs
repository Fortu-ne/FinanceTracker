using FinanceTracker.Data;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace FinanceTracker.Interface
{
    public interface IBudget
    {
         List<Budget> GetBudgets();
         bool createBudget(Budget budget);
         bool updateBudget(Budget budget);
         bool findBudget(int Id);
         bool deleteBudget(Budget budget);
         bool Save();
    }
}
