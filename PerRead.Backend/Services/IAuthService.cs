using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PerRead.Backend.Models;
using PerRead.Backend.Models.Auth;
using PerRead.Backend.Models.FrontEnd;
using PerRead.Backend.Repositories;

namespace PerRead.Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IAuthorRepository _authorRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IOptionsSnapshot<JwtSettings> jwtSettings,
            IAuthorRepository authorRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _authorRepository = authorRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public Task GetCurrentUser()
        {
            throw new NotImplementedException();
        }

        public async Task<JWTModel> Login(ClaimsPrincipal user, string username, string password)
        {
            var userFromDb = await _userManager.Users.SingleOrDefaultAsync(u => u.UserName == username);

            if (userFromDb is null)
            {
                throw new ArgumentException("user not found");
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(userFromDb, password);

            // This should be used for cookie only, lol
            //var loginResult = await _signInManager.PasswordSignInAsync(username, password, true, true);

            if (!userSigninResult)
            {
                throw new ArgumentException("Could not log it");
            }

            var token = GenerateJwt(userFromDb, Enumerable.Empty<string>());
            return new JWTModel
            {
                Token = token
            };
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
                throw new ArgumentException($"Could not register. Please see details: {BuildRegisterError(registrationResult)}");
            }

            await _authorRepository.CreateAsync(new AuthorCommand
            {
                Id = user.Id,
                Name = user.UserName
            });

        }

        private string BuildRegisterError(IdentityResult badResult)
        {
            return string.Join(",", badResult.Errors.Select(x => x.Code).ToList());
        }

        private string GenerateJwt(ApplicationUser user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("userName", user.UserName)
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

    public interface IAuthService
    {
        Task GetCurrentUser();

        Task Register(string username, string password, string email);

        Task<JWTModel> Login(ClaimsPrincipal user, string username, string password);

        Task Logout();
    }
}

