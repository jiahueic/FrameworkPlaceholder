using AGTIV.Framework.MVC.Framework.Configurations;
using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.ViewModel;
using AGTIV.Framework.MVC.UI.ViewModel.User;
using AGTIV.Framework.MVC.UI.ViewModel.General;
using AGTIV.Framework.MVC.UI.Web.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.IO;
using AGTIV.Framework.MVC.Framework.Helper;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IAppSetting _appSetting;
        private readonly IAuthorizationManager _authorizationManager;
        private readonly ICalendarProcess _calendarProcess;
        private readonly IUserProcess _userProcess;
        private readonly IRoleProcess _roleProcess;

        public UserController(
            IAppSetting appSetting,
            IAuthorizationManager authorizationManager,
            ICalendarProcess calendarProcess,
            IUserProcess userProcess,
            IRoleProcess roleProcess)
        {
            _appSetting = appSetting;
            _authorizationManager = authorizationManager;
            _calendarProcess = calendarProcess;
            _userProcess = userProcess;
            _roleProcess = roleProcess;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;

            if (!ModelState.IsValid)
            {
                //TempData[ConstantHelper.Dictionary.ErrorMessage] = ConstantHelper.Error.Common.ModelStateError;
                return View(model);
            }

            var userLoginInfo = _userProcess.Login(model.Email, model.Password/*, model.RememberMe*/);

            if (userLoginInfo == null)
            {
                ViewBag.ErrorMsg = ConstantHelper.Error.User.LoginFail;
                return View(model);
            }

            List<Claim> claims = GetClaims(userLoginInfo);

            if (claims != null)
            {
                Session.Abandon();
                _authorizationManager.SignIn(claims, false /*model.RememberMe, _appSetting.UseADFSAuthentication*/);
            }

            return RedirectToLocal(returnUrl);
        } // pending ADFS

        public ActionResult Logout()
        {
            Session.Abandon();

            //if (_appSetting.UseADFSAuthentication)
            //    _authorizationManager.SignOut(Url.Action("Index", "AgentComm", routeValues: null, protocol: Request.Url.Scheme));
            //else
            //{
            _authorizationManager.SignOut();
            //}

            return RedirectToAction("Login", "User");
        } // pending ADFS

        [AllowAnonymous]
        public ActionResult ForgotPassword(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //TempData[ConstantHelper.Dictionary.ErrorMessage] = ConstantHelper.Error.Common.ModelStateError;
                return View(model);
            }

            _userProcess.ForgotPassword(model.Email);

            TempData["ForgotPassword_Success"] = "Please check your email to reset your password"; //Resources.Main.Profile_RPW_EmailIsSent;
            return RedirectToAction("Login", "User");
        } // pending


        [AllowAnonymous]
        public ActionResult ResetPassword(string token, Guid userId)
        {
            var vm = new ResetPasswordVM
            {
                UserId = userId,
                Token = token
            };

            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            _userProcess.ResetPassword(vm);
            return RedirectToAction("Login");
        }

        [AllowAnonymous]
        public ActionResult Signup(string returnUrl)
        {
            if (Request.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //TempData[ConstantHelper.Dictionary.ErrorMessage] = ConstantHelper.Error.Common.ModelStateError;
                return View(model);
            }

            User user = new User()
            {
                Id = Guid.NewGuid(),
                FullName = model.Name,
                EmailAddress = model.Email,
                Status = 1,
                Password = model.Password,
                //Roles = new string[] { ConstantHelper.Role.Member },
                RolesString = ConstantHelper.Role.Member,
                CreatedBy = new Guid("178EEB55-5357-4919-ADF4-6CE6D0DC5BC6"),
                CreatedOn = DateTime.Now,
                ModifiedBy = new Guid("178EEB55-5357-4919-ADF4-6CE6D0DC5BC6"),
                ModifiedOn = DateTime.Now
            };

            var success = _userProcess.Signup(user);

            if (success)
            {
                ViewBag.SuccessMsg = "Account successfully created";
            }
            else
            {
                ViewBag.ErrorMsg = "Account creation failed";
            }

            return View();
        }

        public ActionResult Update(Guid id)
        {
            var vm = new UpdateUserVM
            {
                User = _userProcess.Get(id),
                RoleDDL = _roleProcess.Get(),
                CalendarDdl = _calendarProcess.GetCalendarProfileDdl(),
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(UpdateUserVM vm)
        {
            vm.User.Id = _authorizationManager.LoginInfo.Id;
            _userProcess.Update(vm.User);

            return RedirectToAction("Update", "User", new { id = vm.User.Id });
        }

        public PartialViewResult Create()
        {
            var pVM = new CreateUserVM();
            pVM.RoleDDL = _roleProcess.Get();

            return PartialView(pVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserVM pVM)
        {
            var isCreated = _userProcess.Create(pVM);

            if (!isCreated)
            {
                ViewBag.ErrorMsg = ConstantHelper.Error.User.DuplicateEmail;
                pVM.RoleDDL = _roleProcess.Get();
                return View(pVM);
            }

            return RedirectToAction("Index", "Role");
        }

        [HttpPost]
        public ActionResult _DeleteUser(GridVM<User> value)
        {
            _userProcess.Delete(new Guid(value.key.ToString()));

            return RedirectToAction("Index", "Role");
        }

        public ActionResult ChangePassword(Guid appUserId, Guid userProfileId)
        {
            var vm = new ChangePasswordVM
            {
                AppUserId = appUserId,
                UserProfileId = userProfileId
            };
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordVM vm)
        {
            string message = _userProcess.ChangePassword(vm);

            if (string.IsNullOrEmpty(message))
            {
                ViewBag.SuccessMsg = ConstantHelper.Success.User.ChangePasswordSuccess;
            }
            else
            {
                message = message.Replace("'", "\"");
                ViewBag.ErrorMsg = message;
            }

            return View(vm);
        }

        public void UploadProfilePicture(HttpPostedFileBase UploadFiles)
        {
            var image = new Image
            {
                Title = Path.GetFileNameWithoutExtension(UploadFiles.FileName),
                Extension = Path.GetExtension(UploadFiles.FileName),
                ContentType = UploadFiles.ContentType,
                FileBytes = ConversionHelper.StreamToByteArray(UploadFiles.InputStream)
            };

            _userProcess.UploadProfilePicture(image);
        }

        private List<Claim> GetClaims(UserInfoViewModel userInfo)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(ConstantHelper.Claims.UserId, userInfo.Id.ToString()),
                new Claim(ConstantHelper.Claims.Username, userInfo.Username),
                new Claim(ConstantHelper.Claims.FullName, userInfo.FullName),
                new Claim(ConstantHelper.Claims.Token, userInfo.access_token),
                new Claim(ConstantHelper.Claims.TokenExpiresIn, userInfo.expires_in.ToString()),
                new Claim(ConstantHelper.Claims.Roles, userInfo.Roles),
            };

            return claims;
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Request");
        }
    }
}