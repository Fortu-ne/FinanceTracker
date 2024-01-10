using FinanceTracker.Data;
using FinanceTracker.Data.DbDataContext;
using FinanceTracker.Interface;
using Microsoft.EntityFrameworkCore;

namespace FinanceTracker.Repository
{
    public class UserRepository : IUser
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool createUser(User user)
        {
            _context.Users.Add(user);

            return Save();
        }

        public bool deleteUser(User user)
        {
           _context.Remove(user);

            return Save();
        }

        public bool findUser(Guid Id)
        {
           return _context.Users.Any(r=>r.Id == Id);
        }

        public User GetUser(Guid Id)
        {
            return _context.Users.Where(r => r.Id == Id).FirstOrDefault();
        }

        public ICollection<User> GetUsers()
        {
            return _context.Users.Include(r=>r.Accounts).Include(r=>r.Budgets).ToList();
        }

        public bool Save()
        {
            var savings = _context.SaveChanges();

            return savings > 0 ? true : false;
        }

        public bool updateUser(User user)
        {
            _context.Update(user);

            return Save();
        }
    }
}
