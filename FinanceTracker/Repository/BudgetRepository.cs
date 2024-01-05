using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Security.Principal;

namespace FinanceTracker.Repository
{
    public class BudgetRepository : IBudget
    {
        private readonly DataContext _context;

        public BudgetRepository(DataContext context)
        {
            _context = context;
        }

        public bool createBudget(Guid id,Budget budget)
        {
            var model = _context.Users.Where(r => r.Id == id).FirstOrDefault();
            budget.Users = model;
            _context.Budgets.Add(budget);
            return Save();
        }

        public bool deleteBudget(Budget budget)
        {
            if (findBudget(budget.Id))
            {
                var model = GetBudgetById(budget.Id);
                _context.Budgets.Remove(budget);

            }

            return Save();
        }

        public bool findBudget(int Id)
        {
            return _context.Budgets.Any(a => a.Id == Id);
        }

        public Budget GetBudget(Budget budget)
        {
            return _context.Budgets.Where(r => r.Name == budget.Name).FirstOrDefault();
        }

        public Budget GetBudgetById(int budgetID)
        {
            return _context.Budgets.Where(r => r.Id == budgetID).FirstOrDefault();
        }

        public ICollection<Budget> GetBudgets()
        {
            return _context.Budgets.Include(r => r.Users).ToList();
        }

        public bool Save()
        {
            var save = _context.SaveChanges();

            return save > 0 ? true : false;
        }

        public bool updateBudget(Budget budget)
        {
            throw new NotImplementedException();
        }
    }
}
