using FinanceManagement.Entities;

namespace FinanceManagement.Dao
{
    public interface IUserRepository
    {
        bool Register(User user);
        User Login(string username, string password);
    }
}
