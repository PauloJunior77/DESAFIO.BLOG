using System;
using System.Collections.Generic;
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
            // Convertendo string para Guid
            Guid senderGuid = Guid.Parse(senderId);
            Guid receiverGuid = Guid.Parse(receiverId);

            return await _dbContext.ChatMessages
                .Where(m => m.SenderId == senderGuid && m.ReceiverId == receiverGuid)
                .ToListAsync();
        }

        public async Task AddAsync(ChatMessage message)
        {
            await _dbContext.ChatMessages.AddAsync(message);
            await _dbContext.SaveChangesAsync();
        }
    }
}
