using DESAFIO.BLOG.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Post> Posts { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }
    public DbSet<Chat> ChatRooms { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Sender)
            .WithMany(u => u.ChatMessagesSent)
            .HasForeignKey(cm => cm.SenderId)
            .IsRequired();

        modelBuilder.Entity<ChatMessage>()
            .HasOne(cm => cm.Receiver)
            .WithMany(u => u.ChatMessagesReceived)
            .HasForeignKey(cm => cm.ReceiverId)
            .IsRequired();

        modelBuilder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.UserId)
            .IsRequired();

        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var adminId = Guid.NewGuid();
        var user1Id = Guid.NewGuid();
        var user2Id = Guid.NewGuid();
        var user3Id = Guid.NewGuid();

        var hasher = new PasswordHasher<ApplicationUser>();

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = adminId,
                UserName = "admin@example.com",
                NormalizedUserName = "ADMIN@EXAMPLE.COM",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "AdminPassword123!"),
                SecurityStamp = string.Empty,
                IsAdmin = true
            },
            new ApplicationUser
            {
                Id = user1Id,
                UserName = "user1@example.com",
                NormalizedUserName = "USER1@EXAMPLE.COM",
                Email = "user1@example.com",
                NormalizedEmail = "USER1@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "UserPassword123!"),
                SecurityStamp = string.Empty,
                IsAdmin = false
            },
            new ApplicationUser
            {
                Id = user2Id,
                UserName = "user2@example.com",
                NormalizedUserName = "USER2@EXAMPLE.COM",
                Email = "user2@example.com",
                NormalizedEmail = "USER2@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "UserPassword123!"),
                SecurityStamp = string.Empty,
                IsAdmin = false
            },
            new ApplicationUser
            {
                Id = user3Id,
                UserName = "user3@example.com",
                NormalizedUserName = "USER3@EXAMPLE.COM",
                Email = "user3@example.com",
                NormalizedEmail = "USER3@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "UserPassword123!"),
                SecurityStamp = string.Empty,
                IsAdmin = false
            }
        );

        modelBuilder.Entity<Post>().HasData(
            new Post
            {
                Id = Guid.NewGuid(),
                Title = "First Post",
                Content = "This is the first post content",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user1@example.com",
                UserId = user1Id,
                UpdatedAt = null
            },
            new Post
            {
                Id = Guid.NewGuid(),
                Title = "Second Post",
                Content = "This is the second post content",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user2@example.com",
                UserId = user2Id,
                UpdatedAt = null
            },
            new Post
            {
                Id = Guid.NewGuid(),
                Title = "Third Post",
                Content = "This is the third post content",
                CreatedAt = DateTime.UtcNow,
                CreatedBy = "user3@example.com",
                UserId = user3Id,
                UpdatedAt = null
            }
        );
    }
}
