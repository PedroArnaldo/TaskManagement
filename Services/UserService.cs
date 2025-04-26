using TaskManagement.Models.DTOs;
using TaskManagement.Repositories;
using System.Security.Cryptography;
using TaskManagement.Models;

namespace TaskManagement.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository repository)
        {
            _userRepository = repository;
        }
        public bool CreateAccount(UserCreateDTO user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                throw new Exception("All fields are required");

            //check if email exists
            var IsEmailExists = _userRepository.EmailExists(user.Email).Result;
            if (IsEmailExists)
                throw new Exception("Email already exists");
            //hash password
            var passwordHashed = HashPassword(user.Password);
            //create user
            var newUser = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = passwordHashed,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            //save user

            var isCreated = _userRepository.Register(newUser).Result;
            if (!isCreated)
                throw new Exception("User not created");
            return true;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string hashedPass)
        {
            var hash = HashPassword(password);
            return hash == hashedPass;
        }
    }
}
