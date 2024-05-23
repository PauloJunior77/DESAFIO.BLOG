using DESAFIO.BLOG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Domain.Repositories
{
    public interface IChatMessageRepository
    {
        Task<IEnumerable<ChatMessage>> GetAllAsync();
        Task<IEnumerable<ChatMessage>> GetMessagesBetweenUsersAsync(string senderId, string receiverId);
        Task AddAsync(ChatMessage message);
        Task<IEnumerable<Guid>> GetChatParticipantsAsync(Guid chatId);
        Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(Guid userId);
    }
}
