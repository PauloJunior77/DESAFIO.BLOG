using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;

namespace DESAFIO.BLOG.Application.Services
{
    public interface IPostService
    {
        Task<Post> GetPostByIdAsync(Guid id);
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task CreatePostAsync(Post post, ClaimsPrincipal user);
        Task UpdatePostAsync(Post post, ClaimsPrincipal user);
        Task DeletePostAsync(Guid id, ClaimsPrincipal user);
    }
}
