using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DESAFIO.BLOG.Application.Services;
using DESAFIO.BLOG.Domain.Entities;  // Certifique-se de que este é o namespace correto para ChatMessage
using DESAFIO.BLOG.Presentation.Hubs;

namespace DESAFIO.BLOG.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
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

            var senderId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            try
            {
                var chatMessage = new ChatMessage
                {
                    Id = Guid.NewGuid(),
                    Content = message,
                    SentAt = DateTime.UtcNow,
                    SenderId = senderId, 
                    ReceiverId = Guid.Parse(receiver)
                };

                await _chatService.SendMessageAsync(chatMessage);
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
                var messages = await _chatService.GetMessagesBetweenUsersAsync(senderId, receiver);
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while retrieving chat history: {ex.Message}");
            }
        }
    }
}
