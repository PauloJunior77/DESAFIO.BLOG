using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;
using DESAFIO.BLOG.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DESAFIO.BLOG.Infrastructure.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly JsonSerializerOptions _jsonOptions;

        public PostRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
        }

        public async Task<IEnumerable<Post>> GetAllAsync()
        {
            var posts = await _dbContext.Posts.Include(p => p.User).ToListAsync();
            return JsonSerializer.Deserialize<IEnumerable<Post>>(JsonSerializer.Serialize(posts, _jsonOptions), _jsonOptions);
        }

        public async Task<Post> GetByIdAsync(Guid id)
        {
            var post = await _dbContext.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
            return JsonSerializer.Deserialize<Post>(JsonSerializer.Serialize(post, _jsonOptions), _jsonOptions);
        }

        public async Task AddAsync(Post post)
        {
            await _dbContext.Posts.AddAsync(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Post post)
        {
            _dbContext.Posts.Update(post);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var post = await _dbContext.Posts.FindAsync(id);
            if (post != null)
            {
                _dbContext.Posts.Remove(post);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
