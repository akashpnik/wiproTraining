
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;
using ProfileBook.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ProfileBook.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly ProfileBookDbContext _context;
        private readonly IConfiguration _configuration;
        //private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(ProfileBookDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            //_passwordHasher = PasswordHasher;

        }

        public async Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto)
        {
            // Check if username or email already exists
            if (await _context.Users.AnyAsync(u => u.Username == registerDto.Username || u.Email == registerDto.Email))
            {
                return null; // User already exists
            }

            // Hash password
            string hashedPassword = HashPassword(registerDto.Password);

            // Create new user
            var user = new User
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = hashedPassword,
                Role = registerDto.Role,
                CreatedAt = DateTime.UtcNow,
                IsActive = true
            };

            // Hash password using ASP.NET Core Identity hasher
            //user.PasswordHash = _passwordHasher.HashPassword(user, registerDto.Password);


            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generate JWT token
            string token = GenerateJwtToken(user.UserId, user.Username, user.Email, user.Role);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto loginDto)
        {
            // Find user by username
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username && u.IsActive == true);

            if (user == null || !VerifyPassword(loginDto.Password, user.PasswordHash))
            {
                return null; // Invalid credentials
            }

            // Generate JWT token
            string token = GenerateJwtToken(user.UserId, user.Username, user.Email, user.Role);

            return new AuthResponseDto
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email,
                Role = user.Role,
                Token = token,
                TokenExpiration = DateTime.UtcNow.AddMinutes(60)
            };
        }

        public string GenerateJwtToken(int userId, string username, string email, string role)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings["SecretKey"];
            var issuer = jwtSettings["Issuer"];
            var audience = jwtSettings["Audience"];
            var expirationMinutes = int.Parse(jwtSettings["ExpirationInMinutes"] ?? "60");

            var key = Encoding.ASCII.GetBytes(secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(expirationMinutes),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] salt = new byte[16];
                rng.GetBytes(salt);

                using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
                {
                    byte[] hash = pbkdf2.GetBytes(32);
                    byte[] hashBytes = new byte[48];
                    Array.Copy(salt, 0, hashBytes, 0, 16);
                    Array.Copy(hash, 0, hashBytes, 16, 32);
                    return Convert.ToBase64String(hashBytes);
                }
            }
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            byte[] hashBytes = Convert.FromBase64String(hashedPassword);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                byte[] hash = pbkdf2.GetBytes(32);
                for (int i = 0; i < 32; i++)
                {
                    if (hashBytes[i + 16] != hash[i])
                        return false;
                }
            }
            return true;
        }
    }
}


