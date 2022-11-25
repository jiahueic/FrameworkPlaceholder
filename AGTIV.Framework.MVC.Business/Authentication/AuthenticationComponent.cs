using AGTIV.Framework.MVC.Business.UnitOfWork;
using AGTIV.Framework.MVC.DTO.Authentication;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Framework.Authentication;
using AGTIV.Framework.MVC.Framework.Configurations;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Authentication
{
    public class AuthenticationComponent : IAuthenticationComponent
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppSetting _appSetting;
        
        public AuthenticationComponent(
            IUnitOfWork unitOfWork,
            IAppSetting appSetting)
        {
            _unitOfWork = unitOfWork;
            _appSetting = appSetting;
        }

        public bool IsAuthenticated(string clientId, string clientSecret)
        {
            if (string.IsNullOrEmpty(clientId) || string.IsNullOrEmpty(clientSecret))
                throw new ArgumentNullException();

            if (clientId.Equals(_appSetting.ClientId))
            {
                return _unitOfWork.Repository.Get<AppSecret>(c => c.ClientSecret.Equals(clientSecret)).SingleOrDefault() != null;
            }

            return false;
        }

        public async Task<IdentityResult> Register(AppUserManager userManager, Registration user)
        {
            var newUser = new AppUser
            {
                Id = Guid.NewGuid(),
                UserName = user.Email,
                Email = user.Email,
                UserProfile = new UserProfile
                {
                    FullName = user.FullName,
                    EmailAddress = user.Email,
                    MobileNo = user.MobileNo,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                }
            };

            var result = await userManager.CreateAsync(newUser,user.Password);

            return result;
        }
    }
}
