using DESAFIO.BLOG.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(Guid id);
        Task<ApplicationUser> GetByAzureB2CIdAsync(string azureB2CId);
        Task<ApplicationUser> AddAsync(ApplicationUser user);
        Task UpdateAsync(ApplicationUser user);
        Task DeleteAsync(Guid id);
    }
}
