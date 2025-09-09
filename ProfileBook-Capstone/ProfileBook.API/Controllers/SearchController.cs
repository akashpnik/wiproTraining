/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileBook.API.DTOs;
using ProfileBook.API.Services;
using System.Security.Claims;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }

        [HttpGet("users")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchResultDto<UserSearchDto>>> SearchUsers(
            [FromQuery] string? q, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20)
        {
            var results = await _searchService.SearchUsersAsync(q, page, pageSize);
            return Ok(results);
        }

        [HttpGet("posts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<SearchResultDto<PostResponseDto>>> SearchPosts(
            [FromQuery] string? q, 
            [FromQuery] int page = 1, 
            [FromQuery] int pageSize = 20)
        {
            var results = await _searchService.SearchPostsAsync(q, page, pageSize);
            return Ok(results);
        }

        [HttpGet("users/suggestions")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<UserSearchDto>>> GetSuggestedUsers([FromQuery] int count = 10)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var suggestions = await _searchService.GetSuggestedUsersAsync(userId, count);
            return Ok(suggestions);
        }

        [HttpGet("users/recent")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserSearchDto>>> GetRecentUsers([FromQuery] int count = 10)
        {
            var recentUsers = await _searchService.GetRecentUsersAsync(count);
            return Ok(recentUsers);
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
*/