namespace ProfileBook.API.DTOs
{
    public class MessageResponseDto
    {
        public int MessageId { get; set; }
        public int SenderId { get; set; }
        public string SenderUsername { get; set; } = string.Empty;
        public string? SenderProfileImage { get; set; }

        public int ReceiverId { get; set; }
        public string ReceiverUsername { get; set; } = string.Empty;
        public string? ReceiverProfileImage { get; set; }

        public string MessageContent { get; set; } = string.Empty;
        public DateTime? TimeStamp { get; set; }
        public bool? IsRead { get; set; }
    }
}

