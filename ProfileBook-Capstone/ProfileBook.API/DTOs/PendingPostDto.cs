namespace ProfileBook.API.DTOs
{
    public class PendingPostDto
    {
        public int PostId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? PostImage { get; set; }
        public DateTime CreatedAt { get; set; }
        
        // User information
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
