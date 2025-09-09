namespace ProfileBook.API.DTOs
{
    public class PostResponseDto
    {
        public int PostId { get; set; }
        public string Content { get; set; } = string.Empty;
        public string? PostImage { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }  //keep as non-nullable(always has value)
        public DateTime? ApprovedAt { get; set; }  // keep as nullable(may not have value)
        
        // User information
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string? UserProfileImage { get; set; }
        
        // Interaction counts
        public int LikesCount { get; set; }
        public int CommentsCount { get; set; }
    }
}
