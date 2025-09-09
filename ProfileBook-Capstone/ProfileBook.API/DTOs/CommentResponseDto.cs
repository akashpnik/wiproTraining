namespace ProfileBook.API.DTOs
{
    public class CommentResponseDto
    {
        public int CommentId { get; set; }           // ← Matches your database
        public int PostId { get; set; }
        public string CommentText { get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; }     // ← Nullable like your model
        
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? UserProfileImage { get; set; }
    }
}
