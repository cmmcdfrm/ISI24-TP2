using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Utils;
using api.Repositories;

namespace StandManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly JwtTokenGenerator _jwtTokenGenerator;
        private readonly IUserRepository _userRepository;

        public AuthController(JwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _userRepository = userRepository;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username) || string.IsNullOrWhiteSpace(user.Password))
            {
                return BadRequest("Username and Password are required.");
            }

            // Verificar se o utilizador já existe
            var existingUser = _userRepository.GetByUsername(user.Username);
            if (existingUser != null)
            {
                return Conflict("Username already exists.");
            }

            //// Criar utilizador
            //user.Role = "User"; // Define a role padrão
            _userRepository.AddUser(user);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] User user)
        {
            var existingUser = _userRepository.GetByUsername(user.Username);
            if (existingUser == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            // Hash the input password to compare with the stored hash
            var hashedPassword = UserRepository.HashPassword(user.Password);
            if (existingUser.Password != hashedPassword)
            {
                return Unauthorized("Invalid credentials.");
            }

            var token = _jwtTokenGenerator.GenerateToken(existingUser.Username/*, existingUser.Role*/);
            return Ok(new { Token = token });
        }
    }
}
