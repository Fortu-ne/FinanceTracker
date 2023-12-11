//using FinanceTracker.Data;
//using FinanceTracker.Data.DbDataContext;
//using FinanceTracker.Interface;

//namespace FinanceTracker.Repository
//{
//    public class BudgetRepository : IBudget
//    {
//        private readonly DataContext _context;

//        public BudgetRepository(DataContext context)
//        {
//            _context = context;
//        }

//        public bool createBudget(Budget budget)
//        {
//            if(budget == null)
//            {
//                _context.Add(budget);
//            }

//            return Save();
//        }

//        public bool deleteBudget(Budget budget)
//        {
//            var model = _context.Budgets.Where(r=>r.Id == budget.Id).FirstOrDefault();

//            if(model != null)
//            {
//                _context.Remove(model);
//            }

//            return Save();
//        }

//        public bool findBudget(int Id)
//        {
//           return _context.Budgets.Any(r => r.Id == Id);
//        }

//        public List<Budget> GetBudgets()
//        {
//            return _context.Budgets.ToList();
//        }

//        public bool Save()
//        {
//            var save_model = _context.SaveChanges();

//            return save_model > 0 ? true : false;
//        }

//        public bool updateBudget(Budget budget)
//        {
//            _context.Update(budget);

//            return Save();
//        }
//    }
//}
