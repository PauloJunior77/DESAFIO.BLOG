namespace DESAFIO.BLOG.Domain.Entities
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string AzureB2CId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
