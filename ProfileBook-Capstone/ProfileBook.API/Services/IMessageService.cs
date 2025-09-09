using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IMessageService
    {
        Task<MessageResponseDto?> SendMessageAsync(int senderId, CreateMessageDto messageDto);
        Task<IEnumerable<MessageResponseDto>> GetConversationAsync(int userId1, int userId2, int page = 1, int pageSize = 50);
        Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(int userId);
        Task<bool> MarkMessagesAsReadAsync(int senderId, int receiverId);
        Task<int> GetUnreadMessageCountAsync(int userId);
        Task<bool> DeleteMessageAsync(int messageId, int userId);
    }
}
