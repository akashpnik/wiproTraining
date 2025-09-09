using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SecureWebApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly AuthService _authService;
        private readonly DataEncryptionService _encryptionService;

        public UsersController(ApplicationDbContext context, AuthService authService, DataEncryptionService encryptionService)
        {
            _context = context;
            _authService = authService;
            _encryptionService = encryptionService;
        }

        /// <summary>
        /// Registers a new user in the system.
        /// </summary>
        /// <param name="userDto">The user registration data.</param>
        /// <returns>A confirmation of user creation.</returns>
        [HttpPost("register")]
        [AllowAnonymous] // This endpoint is public for anyone to register
        public async Task<IActionResult> Register([FromBody] RegisterUserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if the username or email already exists to prevent duplicates
            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username || u.Email == userDto.Email))
            {
                return Conflict("A user with this username or email already exists.");
            }

            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                CreditCardNumberEncrypted = _encryptionService.Encrypt(userDto.CreditCardNumber) // Encrypt sensitive data
            };

            // Hash the password using the authentication service
            _authService.RegisterUser(user, userDto.Password);

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Return a view model to avoid exposing sensitive data like password hash
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return CreatedAtAction(nameof(GetUserByUsername), new { username = user.Username }, userViewModel);
        }

        /// <summary>
        /// Retrieves a user's public information by their username.
        /// </summary>
        /// <param name="username">The username to search for.</param>
        /// <returns>The user's public data if found.</returns>
        [HttpGet("search")]
        [Authorize] // Only authenticated users can search
        public async Task<IActionResult> GetUserByUsername(string username)
        {
            // This is SAFE. Entity Framework Core automatically parameterizes the 'username' variable
            // in the query, which prevents SQL Injection attacks.
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                return NotFound($"User '{username}' not found.");
            }

            // Return a safe DTO/ViewModel instead of the User entity
            // to avoid exposing the PasswordHash or other sensitive data.
            var userViewModel = new UserViewModel
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };

            return Ok(userViewModel);
        }

        /// <summary>
        /// Creates a new user. (Admin-only endpoint)
        /// Note: ValidateAntiForgeryToken is typically for cookie-based auth (MVC apps).
        /// For JWT-based APIs, CSRF protection is handled differently.
        /// </summary>
        /// <param name="newUserDto">The data for the new user.</param>
        /// <returns>A confirmation of the created user.</returns>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")] // This endpoint is restricted to users with the "Admin" role
        [ValidateAntiForgeryToken] // Protects against CSRF attacks in session-based authentication scenarios
        public async Task<IActionResult> CreateUser([FromBody] RegisterUserDto newUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             
            if (await _context.Users.AnyAsync(u => u.Username == newUserDto.Username))
            {
                 return Conflict("Username already exists.");
            }

            var newUser = new User
            {
                Username = newUserDto.Username,
                Email = newUserDto.Email,
                CreditCardNumberEncrypted = _encryptionService.Encrypt(newUserDto.CreditCardNumber)
            };

            _authService.RegisterUser(newUser, newUserDto.Password);

            _context.Users.Add(newUser);
            await _context.SaveChangesAsync();
            
            var userViewModel = new UserViewModel
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email
            };

            return Ok(userViewModel);
        }
    }

    // DTOs and ViewModels for clean API contracts

    public class RegisterUserDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string CreditCardNumber { get; set; }
    }
    
    public class UserViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
