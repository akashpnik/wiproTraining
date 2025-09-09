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
    public class MessagesController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessagesController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<MessageResponseDto>> SendMessage([FromBody] CreateMessageDto messageDto)
        {
            var senderId = GetCurrentUserId();
            if (senderId == 0) return Unauthorized();

            var message = await _messageService.SendMessageAsync(senderId, messageDto);
            if (message == null) return BadRequest("Unable to send message");

            return CreatedAtAction(nameof(GetConversation), 
                new { userId = messageDto.ReceiverId }, message);
        }

        [HttpGet("conversation/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<MessageResponseDto>>> GetConversation(
            int userId, [FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == 0) return Unauthorized();

            var messages = await _messageService.GetConversationAsync(currentUserId, userId, page, pageSize);
            return Ok(messages);
        }

        [HttpGet("conversations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<IEnumerable<ConversationDto>>> GetConversations()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var conversations = await _messageService.GetUserConversationsAsync(userId);
            return Ok(conversations);
        }

        [HttpPut("mark-read/{senderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> MarkMessagesAsRead(int senderId)
        {
            var receiverId = GetCurrentUserId();
            if (receiverId == 0) return Unauthorized();

            await _messageService.MarkMessagesAsReadAsync(senderId, receiverId);
            return Ok(new { message = "Messages marked as read" });
        }

        [HttpGet("unread-count")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<int>> GetUnreadCount()
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var count = await _messageService.GetUnreadMessageCountAsync(userId);
            return Ok(count);
        }

        [HttpDelete("{messageId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult> DeleteMessage(int messageId)
        {
            var userId = GetCurrentUserId();
            if (userId == 0) return Unauthorized();

            var result = await _messageService.DeleteMessageAsync(messageId, userId);
            if (!result) return BadRequest("Unable to delete message");

            return Ok(new { message = "Message deleted successfully" });
        }

        private int GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.TryParse(userIdClaim, out int userId) ? userId : 0;
        }
    }
}
