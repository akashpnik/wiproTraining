using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly ProfileBookDbContext _context;

        public AdminService(ProfileBookDbContext context)
        {
            _context = context;
        }

        /*public async Task<IEnumerable<PendingPostDto>> GetPendingPostsAsync()
        {
            // Debug: Check total posts
            var totalPosts = await _context.Posts.CountAsync();
            Console.WriteLine($"Total posts in database: {totalPosts}");

            // Debug: Check pending posts specifically  
            var pendingPosts = await _context.Posts
                .Where(p => p.Status == "Pending")
                .ToListAsync();
            Console.WriteLine($"Pending posts found: {pendingPosts.Count}");

            // Log each pending post
            foreach (var post in pendingPosts)
            {
                Console.WriteLine($"Post ID: {post.PostId}, Status: '{post.Status}', Content: {post.Content}");
            }

            return pendingPosts.Select(p => new PendingPostDto
            {
                PostId = p.PostId,
                Content = p.Content,
                PostImage = p.PostImage,
                CreatedAt = p.CreatedAt ?? DateTime.MinValue,
                UserId = p.UserId,
                Username = "Debug",
                Email = "debug@test.com"
            });
        }*/

        public async Task<IEnumerable<PendingPostDto>> GetPendingPostsAsync()
        {
            var pendingPosts = await _context.Posts
                .Where(p => p.Status == "Pending")
                .Include(p => p.User) // Include User to access username and email
                .ToListAsync();

            return pendingPosts.Select(p => new PendingPostDto
            {
                PostId = p.PostId,
                Content = p.Content,
                PostImage = p.PostImage,
                CreatedAt = p.CreatedAt ?? DateTime.MinValue,
                UserId = p.UserId,
                Username = p.User.Username ?? string.Empty,
                Email = p.User.Email ?? string.Empty
            });
        }



        

        public async Task<bool> ApprovePostAsync(int postId, int adminId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || post.Status != "Pending")
                return false;

            post.Status = "Approved";
            post.ApprovedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectPostAsync(int postId, int adminId)
        {
            var post = await _context.Posts.FindAsync(postId);

            if (post == null || post.Status != "Pending")
                return false;

            post.Status = "Rejected";

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<PostResponseDto?> GetPostByIdAsync(int postId)
        {
            return await _context.Posts
                .Include(p => p.User)
                .Include(p => p.PostLikes)
                .Include(p => p.PostComments)
                .Where(p => p.PostId == postId)
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
                .FirstOrDefaultAsync();
        }
    }
}
