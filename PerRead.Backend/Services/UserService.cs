﻿using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Auth;

namespace PerRead.Backend.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<string> Login(ClaimsPrincipal user, string username, string password)
        {
            var userFromDb = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);

            if (userFromDb is null)
            {
                throw new ArgumentException("user not found");
            }
            var userSigninResult = await _userManager.CheckPasswordAsync(userFromDb, password);

            var loginResult = await _signInManager.PasswordSignInAsync(username, password, true, true);

            if (!loginResult.Succeeded)
            {
                throw new ArgumentException("Could not log it");
            }

            return GenerateJwt(userFromDb, Enumerable.Empty<string>());

            //if (!loginResult.Succeeded)
            //{
            //    throw new ArgumentException("Could not log it");
            //}

            //var roles = await _userManager.GetRolesAsync()
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task Register(string username, string password, string? email)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = username,
                LockoutEnabled = true
            };

            var registrationResult = await _userManager.CreateAsync(user, password);

            if (!registrationResult.Succeeded)
            {
                throw new ArgumentException("could not register");
            }

        }

        private string GenerateJwt(ApplicationUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public interface IUserService
    {
        Task Register(string username, string password, string email);

        Task<string> Login(ClaimsPrincipal user, string username, string password);

        Task Logout();
    }
}

