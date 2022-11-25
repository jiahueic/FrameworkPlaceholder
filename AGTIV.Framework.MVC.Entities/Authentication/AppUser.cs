using AGTIV.Framework.MVC.Entities.User;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;

namespace AGTIV.Framework.MVC.Entities.Authentication
{
    public class AppUser : IdentityUser<Guid, AppUserLogin, AppUserRole, AppUserClaim>, IUser<Guid>
    {
        public virtual UserProfile UserProfile { get; set; }
        public string ActivationToken { get; set; }
        public DateTime? ActivationExpiryTime { get; set; }
    }
}
