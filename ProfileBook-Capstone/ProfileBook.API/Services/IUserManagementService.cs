/*using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IUserManagementService
    {
        Task<IEnumerable<UserManagementDto>> GetAllUsersAsync(int page = 1, int pageSize = 20);
        Task<UserManagementDto?> GetUserByIdAsync(int userId);
        Task<UserManagementDto?> CreateUserAsync(CreateUserDto createUserDto);
        Task<UserManagementDto?> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task<bool> DeleteUserAsync(int userId);
        Task<bool> DeactivateUserAsync(int userId);
        Task<bool> ActivateUserAsync(int userId);
        Task<UserStatsDto> GetUserStatsAsync();
    }
}
*/