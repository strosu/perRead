using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PerRead.Backend.Models.Commands;
using PerRead.Backend.Services;

namespace PerRead.Backend.Controllers
{
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterUserComand registerCommand)
        {
            await _userService.Register(registerCommand.UserName, registerCommand.Password, registerCommand.Email);

            return Redirect("");
        }

        [HttpPost("/login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand loginCommand, string returnUrl)
        {
            var user = HttpContext.User;

            await _userService.Login(user, loginCommand.UserName, loginCommand.PassWord);

            return Redirect(returnUrl);
        }
    }
}

