//using System;
//using System.Threading.Tasks;
//using DESAFIO.BLOG.Domain.Entities;
//using DESAFIO.BLOG.Domain.Repositories;
//using DESAFIO.BLOG.Infrastructure.Data;
//using Microsoft.EntityFrameworkCore;

//namespace DESAFIO.BLOG.Infrastructure.Repositories
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly ApplicationDbContext _dbContext;

//        public UserRepository(ApplicationDbContext dbContext)
//        {
//            _dbContext = dbContext;
//        }

//        public async Task<ApplicationUser> GetByIdAsync(Guid id)
//        {
//            return await _dbContext.ApplicationUsers.FindAsync(id);
//        }

//        public async Task<ApplicationUser> GetByAzureB2CIdAsync(string azureB2CId)
//        {
//            return await _dbContext.ApplicationUsers.FirstOrDefaultAsync(u => u.AzureB2CId == azureB2CId);
//        }

//        public async Task<ApplicationUser> AddAsync(ApplicationUser user)
//        {
//            await _dbContext.ApplicationUsers.AddAsync(user);
//            await _dbContext.SaveChangesAsync();
//            return user;
//        }

//        public async Task UpdateAsync(ApplicationUser user)
//        {
//            _dbContext.ApplicationUsers.Update(user);
//            await _dbContext.SaveChangesAsync();
//        }

//        public async Task DeleteAsync(Guid id)
//        {
//            var user = await _dbContext.ApplicationUsers.FindAsync(id);
//            if (user != null)
//            {
//                _dbContext.ApplicationUsers.Remove(user);
//                await _dbContext.SaveChangesAsync();
//            }
//        }
//    }
//}
