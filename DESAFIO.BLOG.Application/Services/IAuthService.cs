using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Application.Services
{
    public interface IAuthService
    {
        Task<ApplicationUser> RegisterOrUpdateUserAsync(ClaimsPrincipal user);
    }
}
