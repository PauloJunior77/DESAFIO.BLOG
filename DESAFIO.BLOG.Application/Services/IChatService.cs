using DESAFIO.BLOG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Application.Services
{
    public interface IChatService
    {
        Task<IEnumerable<ChatMessage>> GetAllMessagesAsync();
        Task<IEnumerable<ChatMessage>> GetMessagesBetweenUsersAsync(string senderId, string receiverId);
        Task SendMessageAsync(ChatMessage message);
        Task<IEnumerable<Guid>> GetChatParticipantsAsync(Guid chatId);
        Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(Guid userId);
    }
}
