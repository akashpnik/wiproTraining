using Microsoft.AspNetCore.Mvc;
using ProfileBook.API.DTOs;
using ProfileBook.API.Services;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto registerDto)
        {
            var result = await _authService.RegisterAsync(registerDto);

            if (result == null)
            {
                return BadRequest("Username or email already exists");
            }

            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseDto>> Login(LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(result);
        }
    }
}
