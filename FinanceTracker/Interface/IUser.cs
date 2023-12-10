using FinanceTracker.Data;

namespace FinanceTracker.Interface
{
    public interface IUser
    {
        List<User> GetUsers();
        bool createUser(User user);
        bool updateUser(User user);
        bool findUser(Guid Id, String name);
        bool deleteUser(User user);

        User GetUser(Guid Id, String search);

        bool Save();
    }
}
