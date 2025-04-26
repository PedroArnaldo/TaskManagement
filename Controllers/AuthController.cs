using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Models.DTOs;
using TaskManagement.Services;

namespace TaskManagement.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("create-account")]
        public IActionResult CreateAccount()
        {
            return View();
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody]UserCreateDTO user)
        {
            try
            {
                if (user == null)
                    return StatusCode(StatusCodes.Status400BadRequest);

                _userService.CreateAccount(user);
                 return Ok(new { success = true});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            //var existingUser = _context.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            //if (existingUser == null)
            //{
            //    return StatusCode(StatusCodes.Status401Unauthorized);
            //}
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
