using DESAFIO.BLOG.Application.Services;
using DESAFIO.BLOG.Domain.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Post>>> GetPosts()
        {
            var posts = await _postService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var post = await _postService.GetPostByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Post>> CreatePost(Post post)
        {
            post.UserId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _postService.CreatePostAsync(post, User);
            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> UpdatePost(Guid id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            var isAdminClaim = User.FindFirst("isAdmin");
            if (userIdClaim == null || isAdminClaim == null)
            {
                return Unauthorized();
            }

            var userId = Guid.Parse(userIdClaim.Value);
            if (userId != post.UserId && !bool.Parse(isAdminClaim.Value))
            {
                return Forbid();
            }

            await _postService.UpdatePostAsync(post, User);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var post = await _postService.GetPostByIdAsync(id);
            var isAdminClaim = User.FindFirst("isAdmin");
            if (post == null || isAdminClaim == null)
            {
                return NotFound();
            }

            if (userId != post.UserId && !bool.Parse(isAdminClaim.Value))
            {
                return Forbid();
            }

            await _postService.DeletePostAsync(id, User);
            return NoContent();
        }
    }
}
