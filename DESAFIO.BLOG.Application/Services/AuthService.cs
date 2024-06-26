﻿
using DESAFIO.BLOG.Domain.Entities;
using DESAFIO.BLOG.Domain.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DESAFIO.BLOG.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<ApplicationUser> RegisterOrUpdateUserAsync(ClaimsPrincipal user)
        {
            var email = user.FindFirst(ClaimTypes.Email)?.Value;
            var name = user.FindFirst(ClaimTypes.Name)?.Value;

            var existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = email,
                    Email = email,
                    Name = name
                };
                var result = await _userManager.CreateAsync(newUser);
                if (result.Succeeded)
                    return newUser;
                else
                    return null;
            }
            else
            {
                existingUser.Email = email;
                existingUser.Name = name;
                await _userManager.UpdateAsync(existingUser);
                return existingUser;
            }
        }
    }
}


#region B2C AZURE
//using DESAFIO.BLOG.Domain.Entities;
//using DESAFIO.BLOG.Domain.Repositories;
//using System;
//using System.Linq;
//using System.Security.Claims;
//using System.Threading.Tasks;

//namespace DESAFIO.BLOG.Application.Services
//{
//    public class AuthService : IAuthService
//    {
//        private readonly IUserRepository _userRepository;

//        public AuthService(IUserRepository userRepository)
//        {
//            _userRepository = userRepository;
//        }

//        public async Task<ApplicationUser> RegisterOrUpdateUserAsync(ClaimsPrincipal user)
//        {
//            var azureB2CId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//            var email = user.FindFirst(ClaimTypes.Email)?.Value;
//            var name = user.FindFirst(ClaimTypes.Name)?.Value;

//            var existingUser = await _userRepository.GetByAzureB2CIdAsync(azureB2CId);
//            if (existingUser == null)
//            {
//                var newUser = new ApplicationUser
//                {
//                    Id = Guid.NewGuid(),
//                    AzureB2CId = azureB2CId,
//                    Email = email,
//                    Name = name
//                };
//                await _userRepository.AddAsync(newUser);
//                return newUser;
//            }
//            else
//            {
//                existingUser.Email = email;
//                existingUser.Name = name;
//                await _userRepository.UpdateAsync(existingUser);
//                return existingUser;
//            }
//        }
//    }
//}
#endregion