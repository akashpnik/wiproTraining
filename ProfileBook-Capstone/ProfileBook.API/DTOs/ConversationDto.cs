namespace ProfileBook.API.DTOs
{
    public class ConversationDto
    {
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? ProfileImage { get; set; }
        public string LastMessage { get; set; } = string.Empty;
        public DateTime? LastMessageTime { get; set; }
        public int UnreadCount { get; set; }
    }
}
