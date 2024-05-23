using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DESAFIO.BLOG.Application.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public ChatService(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository ?? throw new ArgumentNullException(nameof(chatMessageRepository));
        }

        public async Task<IEnumerable<ChatMessage>> GetAllMessagesAsync()
        {
            return await _chatMessageRepository.GetAllAsync();
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesBetweenUsersAsync(string senderId, string receiverId)
        {
            if (string.IsNullOrEmpty(senderId) || string.IsNullOrEmpty(receiverId))
            {
                throw new ArgumentException("Sender and receiver IDs cannot be null or empty");
            }

            return await _chatMessageRepository.GetMessagesBetweenUsersAsync(senderId, receiverId);
        }

        public async Task SendMessageAsync(ChatMessage message)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            await _chatMessageRepository.AddAsync(message);
        }

        public async Task<IEnumerable<Guid>> GetChatParticipantsAsync(Guid chatId)
        {
            var chatMessages = await GetAllMessagesAsync();

            var participantIds = new HashSet<Guid>();
            foreach (var message in chatMessages)
            {
                participantIds.Add(message.SenderId);
                participantIds.Add(message.ReceiverId);
            }

            return participantIds;
        }

        public async Task<IEnumerable<ChatMessage>> GetMessagesForUserAsync(Guid userId)
        {
            var allMessages = await GetAllMessagesAsync();

            return allMessages
                .Where(m => m.SenderId == userId || m.ReceiverId == userId)
                .ToList();
        }

    }
}
