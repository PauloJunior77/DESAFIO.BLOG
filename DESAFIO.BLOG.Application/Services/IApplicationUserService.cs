using DESAFIO.BLOG.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Application.Services
{
    public interface IApplicationUserService
    {
        Task<ApplicationUser> GetUserByIdAsync(Guid id);
        Task<ApplicationUser> GetUserByAzureB2CIdAsync(string azureB2CId);
        Task<ApplicationUser> CreateUserAsync(ApplicationUser user);
        Task UpdateUserAsync(ApplicationUser user);
        Task DeleteUserAsync(Guid id);
    }
}
