namespace ProfileBook.API.DTOs
{
    public class UserStatsDto
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int AdminUsers { get; set; }
        public int RegularUsers { get; set; }
        public int UsersThisMonth { get; set; }
        public int TotalPosts { get; set; }
        public int TotalMessages { get; set; }
        public int TotalReports { get; set; }
    }
}
