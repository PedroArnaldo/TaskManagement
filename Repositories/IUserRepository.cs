using TaskManagement.Models;

namespace TaskManagement.Repositories
{
    public interface IUserRepository
    {
        Task<bool> Register(User user);
        Task<User?> Get(string email);
        Task<User?> Update(User user);
        Task<User?> Delete(string email);
        Task<bool> EmailExists(string email);
    }
}
