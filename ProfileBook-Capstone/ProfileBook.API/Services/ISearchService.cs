/*using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface ISearchService
    {
        Task<SearchResultDto<UserSearchDto>> SearchUsersAsync(string? query, int page = 1, int pageSize = 20);
        Task<SearchResultDto<PostResponseDto>> SearchPostsAsync(string? query, int page = 1, int pageSize = 20);
        Task<IEnumerable<UserSearchDto>> GetSuggestedUsersAsync(int userId, int count = 10);
        Task<IEnumerable<UserSearchDto>> GetRecentUsersAsync(int count = 10);
    }
}
*/