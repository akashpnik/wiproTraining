using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;
using ProfileBook.API.Models;

namespace ProfileBook.API.Services
{
    public class MessageService : IMessageService
    {
        private readonly ProfileBookDbContext _context;

        public MessageService(ProfileBookDbContext context)
        {
            _context = context;
        }

        public async Task<MessageResponseDto?> SendMessageAsync(int senderId, CreateMessageDto messageDto)
        {
            // Verify both users exist
            var sender = await _context.Users.FindAsync(senderId);
            var receiver = await _context.Users.FindAsync(messageDto.ReceiverId);

            if (sender == null || receiver == null) return null;

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = messageDto.ReceiverId,
                MessageContent = messageDto.MessageContent,
                TimeStamp = DateTime.UtcNow,
                IsRead = false
            };

            _context.Messages.Add(message);
            await _context.SaveChangesAsync();

            return new MessageResponseDto
            {
                MessageId = message.MessageId,
                SenderId = message.SenderId,
                SenderUsername = sender.Username ?? string.Empty,
                SenderProfileImage = sender.ProfileImage,
                ReceiverId = message.ReceiverId,
                ReceiverUsername = receiver.Username ?? string.Empty,
                ReceiverProfileImage = receiver.ProfileImage,
                MessageContent = message.MessageContent,
                TimeStamp = message.TimeStamp,
                IsRead = message.IsRead
            };
        }

        public async Task<IEnumerable<MessageResponseDto>> GetConversationAsync(int userId1, int userId2, int page = 1, int pageSize = 50)
        {
            var skip = (page - 1) * pageSize;

            return await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                           (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderByDescending(m => m.TimeStamp)
                .Skip(skip)
                .Take(pageSize)
                .Select(m => new MessageResponseDto
                {
                    MessageId = m.MessageId,
                    SenderId = m.SenderId,
                    SenderUsername = m.Sender.Username ?? string.Empty,
                    SenderProfileImage = m.Sender.ProfileImage,
                    ReceiverId = m.ReceiverId,
                    ReceiverUsername = m.Receiver.Username ?? string.Empty,
                    ReceiverProfileImage = m.Receiver.ProfileImage,
                    MessageContent = m.MessageContent,
                    TimeStamp = m.TimeStamp,
                    IsRead = m.IsRead
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ConversationDto>> GetUserConversationsAsync(int userId)
        {
            var conversations = await _context.Messages
                .Include(m => m.Sender)
                .Include(m => m.Receiver)
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .GroupBy(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
                .Select(g => new
                {
                    OtherUserId = g.Key,
                    LastMessage = g.OrderByDescending(m => m.TimeStamp).FirstOrDefault()
                })
                .ToListAsync();

            var result = new List<ConversationDto>();

            foreach (var conv in conversations)
            {
                var otherUser = await _context.Users.FindAsync(conv.OtherUserId);
                var unreadCount = await _context.Messages
                    .CountAsync(m => m.SenderId == conv.OtherUserId && 
                               m.ReceiverId == userId && 
                               m.IsRead == false);

                if (otherUser != null && conv.LastMessage != null)
                {
                    result.Add(new ConversationDto
                    {
                        UserId = otherUser.UserId,
                        Username = otherUser.Username ?? string.Empty,
                        ProfileImage = otherUser.ProfileImage,
                        LastMessage = conv.LastMessage.MessageContent,
                        LastMessageTime = conv.LastMessage.TimeStamp,
                        UnreadCount = unreadCount
                    });
                }
            }

            return result.OrderByDescending(c => c.LastMessageTime);
        }

        public async Task<bool> MarkMessagesAsReadAsync(int senderId, int receiverId)
        {
            var messages = await _context.Messages
                .Where(m => m.SenderId == senderId && m.ReceiverId == receiverId && m.IsRead == false)
                .ToListAsync();

            if (!messages.Any()) return false;

            foreach (var message in messages)
            {
                message.IsRead = true;
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<int> GetUnreadMessageCountAsync(int userId)
        {
            return await _context.Messages
                .CountAsync(m => m.ReceiverId == userId && m.IsRead == false);
        }

        public async Task<bool> DeleteMessageAsync(int messageId, int userId)
        {
            var message = await _context.Messages
                .FirstOrDefaultAsync(m => m.MessageId == messageId && m.SenderId == userId);

            if (message == null) return false;

            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
