using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;
using DESAFIO.BLOG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.BLOG.Infrastructure.Repositories
{
    public class ChatMessageRepository : IChatMessageRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ChatMessageRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ChatMessage>> GetAllAsync()
        {
            return await _dbContext.ChatMessages.ToListAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesBetweenUsersAsync(string senderId, string receiverId)
        {

            return await _dbContext.ChatMessages
                .Where(m => m.SenderId == Guid.Parse(senderId) && m.ReceiverId == Guid.Parse(receiverId))
                .ToListAsync();
        }

        public async Task AddAsync(ChatMessage message)
        {
            await _dbContext.ChatMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Guid>> GetChatParticipantsAsync(Guid chatId)
        {
            var senderIds = await _dbContext.ChatMessages
                .Where(m => m.ChatId == chatId)
                .Select(m => m.SenderId)
                .Distinct()
                .ToListAsync();

            var receiverIds = await _dbContext.ChatMessages
                .Where(m => m.ChatId == chatId)
                .Select(m => m.ReceiverId)
                .Distinct()
                .ToListAsync();

            var participantIds = senderIds.Union(receiverIds);

            return participantIds;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(Guid userId)
        {
            return await _dbContext.ChatMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToListAsync();
        }
    }
}
