using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface IUser
    {
        ICollection<User> GetUsers();
        bool createUser(User user);
        bool updateUser(User user);
        bool findUser(Guid id);
        bool deleteUser(User user);

        User GetUser(Guid Id);

        bool Save();
    }
}
