using System;

namespace AGTIV.Framework.MVC.DTO.User
{
    public class ResetPasswordDto
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }

        public string NewPassword { get; set; }
    }
}