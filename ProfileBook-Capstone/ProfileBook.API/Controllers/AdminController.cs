using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileBook.API.DTOs;
using ProfileBook.API.Services;
using System.Security.Claims;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IReportService _reportService;

        public AdminController(IAdminService adminService, IReportService reportService)
        {
            _adminService = adminService;
            _reportService = reportService;
        }

        [HttpGet("pending-posts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PendingPostDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<PendingPostDto>>> GetPendingPosts()
        {
            var posts = await _adminService.GetPendingPostsAsync();
            return Ok(posts);
            //var posts = await _adminService.GetPendingPostsAsync();
            //return Ok(posts);
            //Debug: Log authentication info
            /*var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            Console.WriteLine($"Auth header: {authHeader}");

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            Console.WriteLine($"User role: {userRole}, User ID: {userId}");

            if (!User.IsInRole("Admin"))
            {
              Console.WriteLine("User is not in Admin role");
              return Forbid();
            }

            var posts = await _adminService.GetPendingPostsAsync();
            Console.WriteLine($"Found {posts.Count()} pending posts");
            return Ok(posts);
            /*/
        }

        [HttpPost("approve-post")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> ApprovePost(int postId)
        {
            var adminId = GetCurrentUserId();
            if (adminId == 0) return Unauthorized();

            var result = await _adminService.ApprovePostAsync(postId, adminId);

            if (!result)
            {
                return BadRequest("Unable to approve post. Post may not exist or may not be pending.");
            }

            return Ok(new { message = "Post approved successfully" });
        }

        [HttpPost("reject-post/{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> RejectPost(int postId)
        {
            var adminId = GetCurrentUserId();
            if (adminId == 0) return Unauthorized();

            var result = await _adminService.RejectPostAsync(postId, adminId);

            if (!result)
            {
                return BadRequest("Unable to reject post. Post may not exist or may not be pending.");
            }

            return Ok(new { message = "Post rejected successfully" });
        }

        [HttpGet("post/{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PostResponseDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PostResponseDto>> GetPost(int postId)
        {
            var post = await _adminService.GetPostByIdAsync(postId);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }


        [HttpGet("reports")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetAllReports()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return Ok(reports);
        }

        [HttpGet("reports/pending")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<ReportResponseDto>>> GetPendingReports()
        {
            var reports = await _reportService.GetPendingReportsAsync();
            return Ok(reports);
        }

        [HttpPost("reports/{reportId}/dismiss")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DismissReport(int reportId)
        {
            var result = await _reportService.UpdateReportStatusAsync(reportId, "Dismissed");
            if (!result) return BadRequest("Unable to dismiss report");

            return Ok(new { message = "Report dismissed successfully" });
        }

       /* private readonly IUserManagementService _userManagementService; // Add this

        // Update constructor
        public AdminController(IAdminService adminService, IReportService reportService, IUserManagementService userManagementService)
        {
            _adminService = adminService;
            _reportService = reportService;
            _userManagementService = userManagementService; // Add this
        }

        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<UserManagementDto>>> GetAllUsers([FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            var users = await _userManagementService.GetAllUsersAsync(page, pageSize);
            return Ok(users);
        }

        [HttpGet("users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserManagementDto>> GetUser(int userId)
        {
            var user = await _userManagementService.GetUserByIdAsync(userId);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("users")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserManagementDto>> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var user = await _userManagementService.CreateUserAsync(createUserDto);
            if (user == null) return BadRequest("Username or email already exists");

            return CreatedAtAction(nameof(GetUser), new { userId = user.UserId }, user);
        }

        [HttpPut("users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserManagementDto>> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = await _userManagementService.UpdateUserAsync(userId, updateUserDto);
            if (user == null) return BadRequest("User not found or duplicate username/email");

            return Ok(user);
        }

        [HttpDelete("users/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeleteUser(int userId)
        {
            var result = await _userManagementService.DeleteUserAsync(userId);
            if (!result) return NotFound();

            return Ok(new { message = "User deleted successfully" });
        }

        [HttpGet("users/stats")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<UserStatsDto>> GetUserStats()
        {
            var stats = await _userManagementService.GetUserStatsAsync();
            return Ok(stats);
        }

        [HttpPost("users/{userId}/deactivate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> DeactivateUser(int userId)
        {
            var result = await _userManagementService.DeactivateUserAsync(userId);
            if (!result) return NotFound();

            return Ok(new { message = "User deactivated successfully" });
        }

        [HttpPost("users/{userId}/activate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult> ActivateUser(int userId)
        {
            var result = await _userManagementService.ActivateUserAsync(userId);
            if (!result) return NotFound();

            return Ok(new { message = "User activated successfully" });
        }*/




    }
}
