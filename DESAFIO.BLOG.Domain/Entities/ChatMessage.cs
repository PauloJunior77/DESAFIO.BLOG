using DESAFIO.BLOG.Domain.Entities;

namespace DESAFIO.BLOG.Domain.Entities
{
    public class ChatMessage
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public Guid SenderId { get; set; }
        public ApplicationUser Sender { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public ApplicationUser Receiver { get; set; }
    }
}