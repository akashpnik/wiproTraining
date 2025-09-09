using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
        string GenerateJwtToken(int userId, string username, string email, string role);
    }
}
