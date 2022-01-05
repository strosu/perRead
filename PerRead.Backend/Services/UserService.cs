using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using PerRead.Backend.Models;

namespace PerRead.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task Login(ClaimsPrincipal user, string username, string password)
        {
            if (_signInManager.IsSignedIn(user))
            {
                return;
            }

            var loginResult = await _signInManager.PasswordSignInAsync(username, password, true, true);

            if (!loginResult.Succeeded)
            {
                throw new ArgumentException();
            }
        }

        public async Task Register(string username, string password, string? email)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = username,
                LockoutEnabled = true
            };

            await _userManager.CreateAsync(user, password);
        }
    }

    public interface IUserService
    {
        Task Register(string username, string password, string email);

        Task Login(ClaimsPrincipal user, string username, string password);
    }
}

