using System;
using System.Threading.Tasks;
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;

namespace DESAFIO.BLOG.Application.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IUserRepository _userRepository;

        public ApplicationUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<ApplicationUser> GetUserByAzureB2CIdAsync(string azureB2CId)
        {
            return await _userRepository.GetByAzureB2CIdAsync(azureB2CId);
        }

        public async Task<ApplicationUser> CreateUserAsync(ApplicationUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(ApplicationUser user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(Guid id)
        {
            await _userRepository.DeleteAsync(id);
        }
    }
}
