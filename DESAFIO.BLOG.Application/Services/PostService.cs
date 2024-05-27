using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;

namespace DESAFIO.BLOG.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            return await _postRepository.GetAllAsync();
        }

        public async Task<Post> GetPostByIdAsync(Guid id)
        {
            return await _postRepository.GetByIdAsync(id);
        }

        public async Task CreatePostAsync(Post post, ClaimsPrincipal user)
        {
            post.Id = Guid.NewGuid();
            post.UserId = Guid.Parse(GetCurrentUserId(user));
            post.CreatedAt = DateTime.UtcNow;
            await _postRepository.AddAsync(post);
        }

        public async Task UpdatePostAsync(Post post, ClaimsPrincipal user)
        {
            var existingPost = await _postRepository.GetByIdAsync(post.Id);

            if (existingPost == null)
            {
                throw new KeyNotFoundException("Postagem não encontrada");
            }

            var userId = Guid.Parse(GetCurrentUserId(user));

            if (existingPost.UserId != userId && !IsUserAdmin(user))
            {
                throw new UnauthorizedAccessException("Você não está autorizado a editar esta postagem");
            }

            existingPost.Title = post.Title;
            existingPost.Content = post.Content;
            existingPost.UpdatedAt = DateTime.UtcNow;

            await _postRepository.UpdateAsync(existingPost);
        }

        public async Task DeletePostAsync(Guid id, ClaimsPrincipal user)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                throw new KeyNotFoundException("Postagem não encontrada");
            }

            var userId = Guid.Parse(GetCurrentUserId(user));

            if (post.UserId != userId && !IsUserAdmin(user))
            {
                throw new UnauthorizedAccessException("\r\nVocê não está autorizado a excluir esta postagem");
            }

            await _postRepository.DeleteAsync(id);
        }

        private string GetCurrentUserId(ClaimsPrincipal user)
        {
            return user?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private bool IsUserAdmin(ClaimsPrincipal user)
        {
            return user?.IsInRole("Admin") ?? false;
        }
    }
}
