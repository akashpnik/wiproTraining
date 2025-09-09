using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;
using ProfileBook.API.Models;

namespace ProfileBook.API.Services
{
    public class PostService : IPostService
    {
        private readonly ProfileBookDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public PostService(ProfileBookDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<PostResponseDto?> CreatePostAsync(CreatePostDto createPostDto, int userId)
        {
            string? postImagePath = null;

            // Handle image upload if provided
            if (createPostDto.PostImage != null)
            {
                postImagePath = await SavePostImageAsync(createPostDto.PostImage);
            }

            var post = new Post
            {
                UserId = userId,
                Content = createPostDto.Content,
                PostImage = postImagePath,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            // Return the created post
            return await GetPostByIdAsync(post.PostId);
        }

        public async Task<IEnumerable<PostResponseDto>> GetApprovedPostsAsync()
        {
            return await _context.Posts
                .Where(p => p.Status == "Approved")
                .Include(p => p.User)
                .Include(p => p.PostLikes)
                .Include(p => p.PostComments)
                .OrderByDescending(p => p.CreatedAt)
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
                .ToListAsync();
        }

        public async Task<IEnumerable<PostResponseDto>> GetUserPostsAsync(int userId)
        {
            return await _context.Posts
                .Where(p => p.UserId == userId)
                .Include(p => p.User)
                .Include(p => p.PostLikes)
                .Include(p => p.PostComments)
                .OrderByDescending(p => p.CreatedAt)
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
                .ToListAsync();
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

        public async Task<string?> SavePostImageAsync(IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
                return null;

            // Validate file type
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var extension = Path.GetExtension(imageFile.FileName).ToLowerInvariant();

            if (!allowedExtensions.Contains(extension))
                throw new ArgumentException("Invalid file type. Only JPG, PNG, and GIF files are allowed.");

            // Generate unique filename
            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadPath = Path.Combine(_environment.WebRootPath, "uploads", "posts");

            // Create directory if it doesn't exist
            Directory.CreateDirectory(uploadPath);

            var filePath = Path.Combine(uploadPath, fileName);

            // Save file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            return $"/uploads/posts/{fileName}";
        }
        


        public async Task<bool> LikePostAsync(int postId, int userId)
{
    var post = await _context.Posts.FindAsync(postId);
    if (post == null || post.Status != "Approved") return false;

    var existingLike = await _context.PostLikes
        .AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);
    
    if (existingLike) return false; // Already liked

    var like = new PostLike
    {
        PostId = postId,
        UserId = userId,
        LikedAt = DateTime.UtcNow
    };

    _context.PostLikes.Add(like);
    await _context.SaveChangesAsync();
    return true;
}

public async Task<bool> UnlikePostAsync(int postId, int userId)
{
    var like = await _context.PostLikes
        .FirstOrDefaultAsync(pl => pl.PostId == postId && pl.UserId == userId);
    
    if (like == null) return false;

    _context.PostLikes.Remove(like);
    await _context.SaveChangesAsync();
    return true;
}

public async Task<bool> IsPostLikedByUserAsync(int postId, int userId)
{
    return await _context.PostLikes
        .AnyAsync(pl => pl.PostId == postId && pl.UserId == userId);
}

public async Task<CommentResponseDto?> AddCommentAsync(int postId, int userId, CreateCommentDto commentDto)
{
    var post = await _context.Posts.FindAsync(postId);
    if (post == null || post.Status != "Approved") return null;

    var comment = new PostComment
    {
        PostId = postId,
        UserId = userId,
        CommentText = commentDto.CommentText,
        CreatedAt = DateTime.UtcNow
    };

    _context.PostComments.Add(comment);
    await _context.SaveChangesAsync();

    return await _context.PostComments
        .Include(c => c.User)
        .Where(c => c.CommentId == comment.CommentId)
        .Select(c => new CommentResponseDto
        {
            CommentId = c.CommentId,
            PostId = c.PostId,
            CommentText = c.CommentText,
            CreatedAt = c.CreatedAt,
            UserId = c.UserId,
            Username = c.User.Username ?? string.Empty,
            UserProfileImage = c.User.ProfileImage
        })
        .FirstOrDefaultAsync();
}

public async Task<IEnumerable<CommentResponseDto>> GetPostCommentsAsync(int postId)
{
    return await _context.PostComments
        .Include(c => c.User)
        .Where(c => c.PostId == postId)
        .OrderBy(c => c.CreatedAt)
        .Select(c => new CommentResponseDto
        {
            CommentId = c.CommentId,
            PostId = c.PostId,
            CommentText = c.CommentText,
            CreatedAt = c.CreatedAt,
            UserId = c.UserId,
            Username = c.User.Username ?? string.Empty,
            UserProfileImage = c.User.ProfileImage
        })
        .ToListAsync();
}

public async Task<bool> DeleteCommentAsync(int commentId, int userId)
{
    var comment = await _context.PostComments
        .FirstOrDefaultAsync(c => c.CommentId == commentId && c.UserId == userId);
    
    if (comment == null) return false;

    _context.PostComments.Remove(comment);
    await _context.SaveChangesAsync();
    return true;
}

    }
}
