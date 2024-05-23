using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DESAFIO.BLOG.Application.Services;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Presentation.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace DESAFIO.BLOG.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChatController : ControllerBase
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IChatService _chatService;

        public ChatController(IHubContext<ChatHub> chatHubContext, IChatService chatService)
        {
            _chatHubContext = chatHubContext ?? throw new ArgumentNullException(nameof(chatHubContext));
            _chatService = chatService ?? throw new ArgumentNullException(nameof(chatService));
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage(string receiver, string message)
        {
            if (string.IsNullOrEmpty(receiver))
            {
                return BadRequest("Receiver cannot be null or empty");
            }

            if (string.IsNullOrEmpty(message))
            {
                return BadRequest("Message cannot be null or empty");
            }

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var senderRole = User.FindFirstValue(ClaimTypes.Role);

            try
            {
                if (senderRole == "Admin")
                {
                    await _chatService.SendMessageAsync(new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        Content = message,
                        SentAt = DateTime.UtcNow,
                        SenderId = Guid.Parse(senderId),
                        ReceiverId = Guid.Parse(receiver)
                    });
                }
                else
                {
                    var chatParticipants = await _chatService.GetChatParticipantsAsync(Guid.Parse(receiver));
                    if (chatParticipants.ToList().Count == 2 && !chatParticipants.Contains(Guid.Parse(senderId)))
                    {
                        return Forbid("You are not allowed to send messages to this chat");
                    }

                    await _chatService.SendMessageAsync(new ChatMessage
                    {
                        Id = Guid.NewGuid(),
                        Content = message,
                        SentAt = DateTime.UtcNow,
                        SenderId = Guid.Parse(senderId),
                        ReceiverId = Guid.Parse(receiver)
                    });
                }

                await _chatHubContext.Clients.User(receiver).SendAsync("ReceiveMessage", senderId, message);

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while sending message: {ex.Message}");
            }
        }

        [HttpGet("ChatHistory")]
        public async Task<IActionResult> GetChatHistory(string receiver)
        {
            if (string.IsNullOrEmpty(receiver))
            {
                return BadRequest("Receiver cannot be null or empty");
            }

            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var senderRole = User.FindFirstValue(ClaimTypes.Role);
                if (senderRole == "Admin")
                {
                    var messages = await _chatService.GetMessagesForUserAsync(Guid.Parse(receiver));
                    return Ok(messages);
                }
                else
                {
                    var chatParticipants = await _chatService.GetChatParticipantsAsync(Guid.Parse(receiver));
                    
                    if (chatParticipants.ToList().Count != 2 || !chatParticipants.Contains(Guid.Parse(senderId)))
                    {
                        return Forbid("You are not allowed to access this chat history");
                    }

                    var messages = await _chatService.GetMessagesBetweenUsersAsync(senderId, receiver);
                    return Ok(messages);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving chat history: {ex.Message}");
            }
        }
    }
}
