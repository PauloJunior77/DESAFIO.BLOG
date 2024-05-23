using Microsoft.AspNetCore.Identity;

namespace DESAFIO.BLOG.Domain.Entities
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string JwtToken { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ChatMessage> ChatMessagesSent { get; set; } // Mensagens enviadas
        public ICollection<ChatMessage> ChatMessagesReceived { get; set; } // Mensagens recebidas
    }
}
