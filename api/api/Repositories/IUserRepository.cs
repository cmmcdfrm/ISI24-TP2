using api.Models;

namespace api.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        User? GetByUsername(string username);
    }
}
