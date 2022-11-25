using AGTIV.Framework.MVC.Business.Authentication;
using AGTIV.Framework.MVC.Business.Role;
using AGTIV.Framework.MVC.Business.User;
using AGTIV.Framework.MVC.Data.Context;
using AGTIV.Framework.MVC.DTO.Authentication;
using AGTIV.Framework.MVC.DTO.User;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Entities.Shared;
using AGTIV.Framework.MVC.Entities.User;
using AGTIV.Framework.MVC.Framework.Authentication;
using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Helper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Mail;
using System.Web;
using System.Web.Http;

namespace AGTIV.Framework.MVC.WebAPI.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        private readonly IAppSetting _appSetting;
        private readonly IUserComponent _userComponent;
        private readonly IRoleComponent _roleComponent;
        private readonly IAuthenticationComponent _authenticationComponent;

        public UserController(
            IAppSetting appSetting, 
            IUserComponent userComponent,
            IRoleComponent roleComponent,
            IAuthenticationComponent authenticationComponent)
        {
            _appSetting = appSetting;
            _userComponent = userComponent;
            _roleComponent = roleComponent;
            _authenticationComponent = authenticationComponent;
        }

        public IHttpActionResult Get()
        {
            var result = _userComponent.Get();

            return Ok(result);
        }

        public IHttpActionResult Get(Guid id)
        {
            var result = _userComponent.Get(id);

            return Ok(result);
        }

        public IHttpActionResult Put(UserProfile userProfile)
        {
            AppDbContext context = new AppDbContext();
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

            foreach (var role in userProfile.Roles)
            {
                userManager.AddToRole(userProfile.Id, role);
            }

            var roles = _roleComponent.Get();
            roles = roles.Except(roles.Where(c => userProfile.Roles.Contains(c.Name)));
            var excludedRoles = roles.Select(c => c.Name).ToArray();

            foreach (var role in excludedRoles)
            {
                userManager.RemoveFromRole(userProfile.Id, role);
            }

            _userComponent.UpdateUserProfile(userProfile);

            return Ok();
        }

        public async System.Threading.Tasks.Task<IHttpActionResult> PostAsync(UserProfile userProfile)
        {
            AppDbContext context = new AppDbContext();
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

            var user = new AppUser() { UserName = userProfile.EmailAddress, Email = userProfile.EmailAddress, Id = Guid.NewGuid() };

            IdentityResult result = await userManager.CreateAsync(user, "Agtiv@123");

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }
            else
            {
                var createdUser = userManager.FindByEmail(userProfile.EmailAddress);
                userProfile.Id = createdUser.Id;
                _userComponent.CreateUserProfile(userProfile);

                foreach (var role in userProfile.Roles)
                {
                    await userManager.AddToRoleAsync(createdUser.Id, role);
                }
            }



            return Ok((userProfile.Id != new Guid() && user.Id != new Guid()));
        }

        public async System.Threading.Tasks.Task<IHttpActionResult> DeleteAsync(Guid Id)
        {
            AppDbContext context = new AppDbContext();
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();
            var userProfile = _userComponent.Get(Id);
            var deleteUser = await userManager.FindByIdAsync(userProfile.Id);

            await userManager.DeleteAsync(deleteUser);

            return Ok();
        }

        private IHttpActionResult GetErrorResult(IdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest.
                    return BadRequest();
                }

                //return BadRequest(ModelState);
                return Content(System.Net.HttpStatusCode.Forbidden, ModelState);
            }

            return null;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/Password/Forgot")]
        public async System.Threading.Tasks.Task<IHttpActionResult> ForgotPassword(string email)
        {
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();

            var user = userManager.FindByEmail(email);

            if (user == null)
                return Ok(true);

            var userId = user.Id;

            HashHelper hashHelper = new HashHelper();
            var activationToken = await userManager.GeneratePasswordResetTokenAsync(user.Id);
            var activationExpiredTime = DateTime.Now.AddDays(1);

            var appUser = new AppUser()
            {
                Id = userId,
                ActivationToken = activationToken,
                ActivationExpiryTime = activationExpiredTime
            };

            using(SmtpClient smtpClient = new SmtpClient())
            {
                using(MailMessage message = new MailMessage())
                {
                    smtpClient.Host = _appSetting.AG_EmailHost;
                    smtpClient.Port = Int32.Parse(_appSetting.AG_EmailPort);
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = true;
                    smtpClient.Credentials = new System.Net.NetworkCredential(_appSetting.AG_EmailUsername, _appSetting.AG_EmailPassword);

                    message.From = new MailAddress(_appSetting.AG_EmailFrom);
                    message.To.Add(email);
                    message.Subject = "AGTIV Framework - Reset Password";
                    message.Body = _appSetting.WebUrl + "User/ResetPassword?token=" + HttpUtility.UrlEncode(activationToken) + "&userId=" + appUser.Id.ToString();

                    smtpClient.Send(message);
                }
            }

            return Ok(true);
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("api/User/Register")]
        public async System.Threading.Tasks.Task<IHttpActionResult> Signup(Registration model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();
            var result = await _authenticationComponent.Register(userManager, model);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("~/api/Password/Reset")]
        public IHttpActionResult ResetPassword(ResetPasswordDto dto)
        {
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();
            var result = userManager.ResetPassword(dto.UserId, dto.Token, dto.NewPassword);
            return Ok();
        }

        [HttpPost]
        [Route("~/api/Password/Change")]
        public IHttpActionResult ChangePassword(ChangePasswordDto dto)
        {
            var userManager = Request.GetOwinContext().GetUserManager<AppUserManager>();
            var result = userManager.ChangePassword(dto.UserId, dto.OldPassword, dto.NewPassword);

            if(!result.Succeeded)
            {
                var errorsDto = new IdentityResultDto
                {
                    Errors = result.Errors
                };
                return Ok(errorsDto);
            }
            else
            {
                return Ok();
            }
        }

        [HttpPost]
        [Route("api/User/Image")]
        public IHttpActionResult UploadProfilePicture(Image image)
        {
            _userComponent.UploadProfilePicture(image);

            return Ok();
        }
    }
}
