/*using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;
using ProfileBook.API.Models;
using BCrypt.Net;

namespace ProfileBook.API.Services
{
    public class UserManagementService : IUserManagementService
    {
        private readonly ProfileBookDbContext _context;

        public UserManagementService(ProfileBookDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserManagementDto>> GetAllUsersAsync(int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;

            return await _context.Users
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new UserManagementDto
                {
                    UserId = u.UserId,
                    Username = u.Username ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    Role = u.Role ?? "User",
                    ProfileImage = u.ProfileImage,
                    CreatedAt = u.CreatedAt,
                    PostsCount = u.Posts.Count(),
                    MessagesCount = u.SentMessages.Count() + u.ReceivedMessages.Count(),
                    ReportsCount = u.ReportsReceived.Count(),
                    IsActive = true // Add IsActive field to User model if needed
                })
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
        }

        public async Task<UserManagementDto?> GetUserByIdAsync(int userId)
        {
            return await _context.Users
                .Where(u => u.UserId == userId)
                .Select(u => new UserManagementDto
                {
                    UserId = u.UserId,
                    Username = u.Username ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    Role = u.Role ?? "User",
                    ProfileImage = u.ProfileImage,
                    CreatedAt = u.CreatedAt,
                    PostsCount = u.Posts.Count(),
                    MessagesCount = u.SentMessages.Count() + u.ReceivedMessages.Count(),
                    ReportsCount = u.ReportsReceived.Count(),
                    IsActive = true
                })
                .FirstOrDefaultAsync();
        }

        public async Task<UserManagementDto?> CreateUserAsync(CreateUserDto createUserDto)
        {
            // Check if username or email already exists
            var existingUser = await _context.Users
                .AnyAsync(u => u.Username == createUserDto.Username || u.Email == createUserDto.Email);
            
            if (existingUser) return null;

            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
                Role = createUserDto.Role,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return await GetUserByIdAsync(user.UserId);
        }

        public async Task<UserManagementDto?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return null;

            // Check for duplicate username/email (excluding current user)
            var duplicateUser = await _context.Users
                .AnyAsync(u => u.UserId != userId && 
                          (u.Username == updateUserDto.Username || u.Email == updateUserDto.Email));
            
            if (duplicateUser) return null;

            user.Username = updateUserDto.Username;
            user.Email = updateUserDto.Email;
            user.Role = updateUserDto.Role;
            user.ProfileImage = updateUserDto.ProfileImage;

            // Update password if provided
            if (!string.IsNullOrEmpty(updateUserDto.NewPassword))
            {
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(updateUserDto.NewPassword);
            }

            await _context.SaveChangesAsync();
            return await GetUserByIdAsync(userId);
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Note: In production, consider soft delete instead of hard delete
            // to maintain referential integrity

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeactivateUserAsync(int userId)
        {
            // This would require adding IsActive field to User model
            // For now, we'll return true as placeholder
            return true;
        }

        public async Task<bool> ActivateUserAsync(int userId)
        {
            // This would require adding IsActive field to User model
            // For now, we'll return true as placeholder
            return true;
        }

        public async Task<UserStatsDto> GetUserStatsAsync()
        {
            var now = DateTime.UtcNow;
            var startOfMonth = new DateTime(now.Year, now.Month, 1);

            return new UserStatsDto
            {
                TotalUsers = await _context.Users.CountAsync(),
                ActiveUsers = await _context.Users.CountAsync(), // All users are active for now
                AdminUsers = await _context.Users.CountAsync(u => u.Role == "Admin"),
                RegularUsers = await _context.Users.CountAsync(u => u.Role == "User"),
                UsersThisMonth = await _context.Users.CountAsync(u => u.CreatedAt >= startOfMonth),
                TotalPosts = await _context.Posts.CountAsync(),
                TotalMessages = await _context.Messages.CountAsync(),
                TotalReports = await _context.Reports.CountAsync()
            };
        }
    }
}*/
