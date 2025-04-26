using TaskManagement.Models.DTOs;

namespace TaskManagement.Services
{
    public interface IUserService
    {
        bool CreateAccount(UserCreateDTO user);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPass);
    }
}
