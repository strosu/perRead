using System;
namespace PerRead.Backend.Models.Commands
{
    public class RegisterUserComand
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string? Email { get; set; }
    }

    public class LoginUserCommand
    {
        public string UserName { get; set; }

        public string PassWord { get; set; }
    }
}