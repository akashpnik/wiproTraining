using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IAdminService
    {
        Task<IEnumerable<PendingPostDto>> GetPendingPostsAsync();
        Task<bool> ApprovePostAsync(int postId, int adminId);
        Task<bool> RejectPostAsync(int postId, int adminId);
        Task<PostResponseDto?> GetPostByIdAsync(int postId);
    }
}
