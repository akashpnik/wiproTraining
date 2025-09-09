/*using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public class SearchService : ISearchService
    {
        private readonly ProfileBookDbContext _context;

        public SearchService(ProfileBookDbContext context)
        {
            _context = context;
        }

        public async Task<SearchResultDto<UserSearchDto>> SearchUsersAsync(string? query, int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;
            
            var queryable = _context.Users.AsQueryable();

            // Apply search filter if query is provided
            if (!string.IsNullOrWhiteSpace(query))
            {
                var searchTerm = query.ToLower().Trim();
                queryable = queryable.Where(u => 
                    u.Username!.ToLower().Contains(searchTerm) ||
                    u.Email!.ToLower().Contains(searchTerm));
            }

            var totalCount = await queryable.CountAsync();
            
            var users = await queryable
                .Skip(skip)
                .Take(pageSize)
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    Username = u.Username ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    ProfileImage = u.ProfileImage,
                    Role = u.Role ?? "User",
                    CreatedAt = u.CreatedAt,
                    PostsCount = u.Posts.Count(),
                    IsActive = true
                })
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new SearchResultDto<UserSearchDto>
            {
                Results = users,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasNextPage = page < totalPages,
                HasPreviousPage = page > 1
            };
        }

        public async Task<SearchResultDto<PostResponseDto>> SearchPostsAsync(string? query, int page = 1, int pageSize = 20)
        {
            var skip = (page - 1) * pageSize;
            
            var queryable = _context.Posts
                .Where(p => p.Status == "Approved")
                .AsQueryable();

            // Apply search filter if query is provided
            if (!string.IsNullOrWhiteSpace(query))
            {
                var searchTerm = query.ToLower().Trim();
                queryable = queryable.Where(p => 
                    p.Content.ToLower().Contains(searchTerm));
            }

            var totalCount = await queryable.CountAsync();
            
            var posts = await queryable
                .Include(p => p.User)
                .Include(p => p.PostLikes)
                .Include(p => p.PostComments)
                .Skip(skip)
                .Take(pageSize)
                .Select(p => new PostResponseDto
                {
                    PostId = p.PostId,
                    Content = p.Content,
                    PostImage = p.PostImage,
                    Status = p.Status,
                    CreatedAt = p.CreatedAt ?? DateTime.MinValue,
                    ApprovedAt = p.ApprovedAt,
                    UserId = p.UserId,
                    Username = p.User.Username ?? string.Empty,
                    UserProfileImage = p.User.ProfileImage,
                    LikesCount = p.PostLikes.Count,
                    CommentsCount = p.PostComments.Count
                })
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            return new SearchResultDto<PostResponseDto>
            {
                Results = posts,
                TotalCount = totalCount,
                PageNumber = page,
                PageSize = pageSize,
                TotalPages = totalPages,
                HasNextPage = page < totalPages,
                HasPreviousPage = page > 1
            };
        }

        public async Task<IEnumerable<UserSearchDto>> GetSuggestedUsersAsync(int userId, int count = 10)
        {
            // Simple suggestion: users who joined recently, excluding current user
            return await _context.Users
                .Where(u => u.UserId != userId)
                .OrderByDescending(u => u.CreatedAt)
                .Take(count)
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    Username = u.Username ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    ProfileImage = u.ProfileImage,
                    Role = u.Role ?? "User",
                    CreatedAt = u.CreatedAt,
                    PostsCount = u.Posts.Count(),
                    IsActive = true
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<UserSearchDto>> GetRecentUsersAsync(int count = 10)
        {
            return await _context.Users
                .OrderByDescending(u => u.CreatedAt)
                .Take(count)
                .Select(u => new UserSearchDto
                {
                    UserId = u.UserId,
                    Username = u.Username ?? string.Empty,
                    Email = u.Email ?? string.Empty,
                    ProfileImage = u.ProfileImage,
                    Role = u.Role ?? "User",
                    CreatedAt = u.CreatedAt,
                    PostsCount = u.Posts.Count(),
                    IsActive = true
                })
                .ToListAsync();
        }
    }
}
*/