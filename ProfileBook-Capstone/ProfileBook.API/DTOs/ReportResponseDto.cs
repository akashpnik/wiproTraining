namespace ProfileBook.API.DTOs
{
    public class ReportResponseDto
    {
        public int ReportId { get; set; }
        public int ReportedUserId { get; set; }
        public string ReportedUsername { get; set; } = string.Empty;
        public string? ReportedUserProfileImage { get; set; }
        
        public int ReportingUserId { get; set; }
        public string ReportingUsername { get; set; } = string.Empty;
        
        public string Reason { get; set; } = string.Empty;
        public DateTime? TimeStamp { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Reviewed, Dismissed
    }
}
