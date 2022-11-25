using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AGTIV.Framework.MVC.UI.ViewModel.User
{
    public class ChangePasswordVM
    {
        public Guid UserProfileId { get; set; }

        public Guid AppUserId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [DisplayName("Old Password")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [DisplayName("New Password")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Confirm password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}