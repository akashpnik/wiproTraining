using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IPostService
    {
        Task<PostResponseDto?> CreatePostAsync(CreatePostDto createPostDto, int userId);
        Task<IEnumerable<PostResponseDto>> GetApprovedPostsAsync();
        Task<IEnumerable<PostResponseDto>> GetUserPostsAsync(int userId);
        Task<PostResponseDto?> GetPostByIdAsync(int postId);
        Task<string?> SavePostImageAsync(IFormFile imageFile);


        // Like functionality
        Task<bool> LikePostAsync(int postId, int userId);
        Task<bool> UnlikePostAsync(int postId, int userId);  
        Task<bool> IsPostLikedByUserAsync(int postId, int userId);

// Comment functionality
        Task<CommentResponseDto?> AddCommentAsync(int postId, int userId, CreateCommentDto commentDto);
        Task<IEnumerable<CommentResponseDto>> GetPostCommentsAsync(int postId);
        Task<bool> DeleteCommentAsync(int commentId, int userId);

    }
}
