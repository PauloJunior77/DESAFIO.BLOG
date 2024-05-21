using DESAFIO.BLOG.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Domain.Repositories
{
    public interface IPostRepository
    {
        Task<IEnumerable<Post>> GetAllAsync();
        Task<Post> GetByIdAsync(Guid id);
        Task AddAsync(Post post);
        Task UpdateAsync(Post post);
        Task DeleteAsync(Guid id);
    }
}
