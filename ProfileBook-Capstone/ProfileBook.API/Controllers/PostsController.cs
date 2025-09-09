using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileBook.API.DTOs;
using ProfileBook.API.Services;
using System.Security.Claims;

namespace ProfileBook.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetApprovedPosts()
        {
            var posts = await _postService.GetApprovedPostsAsync();
            return Ok(posts);
        }

        [HttpGet("my-posts")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<PostResponseDto>>> GetMyPosts()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var posts = await _postService.GetUserPostsAsync(userId);
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostResponseDto>> GetPost(int id)
        {
            var post = await _postService.GetPostByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(PostResponseDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<PostResponseDto>> CreatePost([FromForm] CreatePostDto createPostDto)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            try
            {
                var post = await _postService.CreatePostAsync(createPostDto, userId);

                if (post == null)
                {
                    return BadRequest("Failed to create post");
                }

                return CreatedAtAction(nameof(GetPost), new { id = post.PostId }, post);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
        



[HttpPost("{postId}/like")]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult> LikePost(int postId)
{
    var userId = GetCurrentUserId();
    if (userId == 0) return Unauthorized();

    var result = await _postService.LikePostAsync(postId, userId);
    if (!result) return BadRequest("Unable to like post or already liked");

    return Ok(new { message = "Post liked successfully" });
}

[HttpDelete("{postId}/like")]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult> UnlikePost(int postId)
{
    var userId = GetCurrentUserId();
    if (userId == 0) return Unauthorized();

    var result = await _postService.UnlikePostAsync(postId, userId);
    if (!result) return BadRequest("Unable to unlike post");

    return Ok(new { message = "Post unliked successfully" });
}

[HttpPost("{postId}/comments")]
[Authorize]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult<CommentResponseDto>> AddComment(int postId, [FromBody] CreateCommentDto commentDto)
{
    var userId = GetCurrentUserId();
    if (userId == 0) return Unauthorized();

    var comment = await _postService.AddCommentAsync(postId, userId, commentDto);
    if (comment == null) return BadRequest("Unable to add comment to this post");

    return CreatedAtAction(nameof(GetPostComments), new { postId }, comment);
}

[HttpGet("{postId}/comments")]
[ProducesResponseType(StatusCodes.Status200OK)]
public async Task<ActionResult<IEnumerable<CommentResponseDto>>> GetPostComments(int postId)
{
    var comments = await _postService.GetPostCommentsAsync(postId);
    return Ok(comments);
}

[HttpDelete("comments/{commentId}")]
[Authorize]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public async Task<ActionResult> DeleteComment(int commentId)
{
    var userId = GetCurrentUserId();
    if (userId == 0) return Unauthorized();

    var result = await _postService.DeleteCommentAsync(commentId, userId);
    if (!result) return BadRequest("Unable to delete comment or not found");

    return Ok(new { message = "Comment deleted successfully" });
}

    }
}
