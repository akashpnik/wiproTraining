using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileBook.API.DTOs;
using ProfileBook.API.Services;
using System.Security.Claims;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ReportResponseDto>> CreateReport([FromBody] CreateReportDto reportDto)
        {
            var reportingUserId = GetCurrentUserId();
            if (reportingUserId == 0) return Unauthorized();

            var report = await _reportService.CreateReportAsync(reportingUserId, reportDto);
            if (report == null) return BadRequest("Unable to create report. User may not exist or already reported.");

            return CreatedAtAction(nameof(GetUserReports), report);
        }

        [HttpGet("my-reports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetUserReports()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var reports = await _reportService.GetUserReportsAsync(userId);
            return Ok(reports);
        }

        [HttpDelete("{reportId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteReport(int reportId)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var result = await _reportService.DeleteReportAsync(reportId, userId);
            if (!result) return BadRequest("Unable to delete report");

            return Ok(new { message = "Report deleted successfully" });
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
