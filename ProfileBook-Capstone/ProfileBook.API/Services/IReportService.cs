using ProfileBook.API.DTOs;

namespace ProfileBook.API.Services
{
    public interface IReportService
    {
        Task<ReportResponseDto?> CreateReportAsync(int reportingUserId, CreateReportDto reportDto);
        Task<IEnumerable<ReportResponseDto>> GetUserReportsAsync(int userId);
        Task<IEnumerable<ReportResponseDto>> GetAllReportsAsync(); // Admin only
        Task<IEnumerable<ReportResponseDto>> GetPendingReportsAsync(); // Admin only
        Task<bool> UpdateReportStatusAsync(int reportId, string status); // Admin only
        Task<bool> DeleteReportAsync(int reportId, int userId);
    }
}

