using System;

namespace AGTIV.Framework.MVC.DTO.User
{
    public class ChangePasswordDto
    {
        public Guid UserId { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}