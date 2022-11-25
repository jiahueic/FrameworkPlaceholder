using AGTIV.Framework.MVC.DTO.Authentication;
using AGTIV.Framework.MVC.Framework.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Authentication
{
    public interface IAuthenticationComponent
    {
        bool IsAuthenticated(string clientId, string clientSecret);

        Task<IdentityResult> Register(AppUserManager userManager, Registration user);
    }
}
