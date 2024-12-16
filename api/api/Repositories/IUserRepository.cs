using api.Models;

namespace api.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetByUsername(string username);
    }
}
