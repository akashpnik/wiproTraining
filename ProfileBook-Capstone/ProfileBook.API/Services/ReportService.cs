using Microsoft.EntityFrameworkCore;
using ProfileBook.API.Data;
using ProfileBook.API.DTOs;
using ProfileBook.API.Models;

namespace ProfileBook.API.Services
{
    public class ReportService : IReportService
    {
        private readonly ProfileBookDbContext _context;

        public ReportService(ProfileBookDbContext context)
        {
            _context = context;
        }

        public async Task<ReportResponseDto?> CreateReportAsync(int reportingUserId, CreateReportDto reportDto)
        {
            // Prevent self-reporting
            if (reportingUserId == reportDto.ReportedUserId) return null;

            // Check if both users exist
            var reportingUser = await _context.Users.FindAsync(reportingUserId);
            var reportedUser = await _context.Users.FindAsync(reportDto.ReportedUserId);
            
            if (reportingUser == null || reportedUser == null) return null;

            // Check for duplicate reports (same user reporting same user)
            var existingReport = await _context.Reports
                .AnyAsync(r => r.ReportingUserId == reportingUserId && 
                              r.ReportedUserId == reportDto.ReportedUserId);
            
            if (existingReport) return null; // Already reported

            var report = new Report
            {
                ReportedUserId = reportDto.ReportedUserId,
                ReportingUserId = reportingUserId,
                Reason = reportDto.Reason,
                TimeStamp = DateTime.UtcNow
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return new ReportResponseDto
            {
                ReportId = report.ReportId,
                ReportedUserId = report.ReportedUserId,
                ReportedUsername = reportedUser.Username ?? string.Empty,
                ReportedUserProfileImage = reportedUser.ProfileImage,
                ReportingUserId = report.ReportingUserId,
                ReportingUsername = reportingUser.Username ?? string.Empty,
                Reason = report.Reason,
                TimeStamp = report.TimeStamp,
                Status = "Pending"
            };
        }

        public async Task<IEnumerable<ReportResponseDto>> GetUserReportsAsync(int userId)
        {
            return await _context.Reports
                .Include(r => r.ReportedUser)
                .Include(r => r.ReportingUser)
                .Where(r => r.ReportingUserId == userId)
                .OrderByDescending(r => r.TimeStamp)
                .Select(r => new ReportResponseDto
                {
                    ReportId = r.ReportId,
                    ReportedUserId = r.ReportedUserId,
                    ReportedUsername = r.ReportedUser.Username ?? string.Empty,
                    ReportedUserProfileImage = r.ReportedUser.ProfileImage,
                    ReportingUserId = r.ReportingUserId,
                    ReportingUsername = r.ReportingUser.Username ?? string.Empty,
                    Reason = r.Reason,
                    TimeStamp = r.TimeStamp,
                    Status = "Pending"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReportResponseDto>> GetAllReportsAsync()
        {
            return await _context.Reports
                .Include(r => r.ReportedUser)
                .Include(r => r.ReportingUser)
                .OrderByDescending(r => r.TimeStamp)
                .Select(r => new ReportResponseDto
                {
                    ReportId = r.ReportId,
                    ReportedUserId = r.ReportedUserId,
                    ReportedUsername = r.ReportedUser.Username ?? string.Empty,
                    ReportedUserProfileImage = r.ReportedUser.ProfileImage,
                    ReportingUserId = r.ReportingUserId,
                    ReportingUsername = r.ReportingUser.Username ?? string.Empty,
                    Reason = r.Reason,
                    TimeStamp = r.TimeStamp,
                    Status = "Pending"
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ReportResponseDto>> GetPendingReportsAsync()
        {
            // For now, all reports are pending. In future, you can add a Status column
            return await GetAllReportsAsync();
        }

        public async Task<bool> UpdateReportStatusAsync(int reportId, string status)
        {
            // This would require adding a Status column to Reports table
            // For now, we'll just return true as reports don't have status in current schema
            return true;
        }

        public async Task<bool> DeleteReportAsync(int reportId, int userId)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(r => r.ReportId == reportId && r.ReportingUserId == userId);
            
            if (report == null) return false;

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
