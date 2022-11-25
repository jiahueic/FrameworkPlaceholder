using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.UI.Process.Interfaces;
using AGTIV.Framework.MVC.UI.Process.Manager;
using AGTIV.Framework.MVC.UI.ViewModel;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace AGTIV.Framework.MVC.UI.Web.Manager
{
    public class AuthorizationManager : IBearerTokenManager, IAuthorizationManager
    {
        private string[] authenticationTypes = new string[] {
            CookieAuthenticationDefaults.AuthenticationType,
            DefaultAuthenticationTypes.ApplicationCookie,
        };

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().Authentication;
            }
        }

        public void SignIn(List<Claim> claims, bool rememberMe)
        {

            var claimsIdentity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);
            DateTime expireDateTime = DateTime.UtcNow.Add(rememberMe ? ConstantHelper.Auth.SessionTimeoutRememberMe : ConstantHelper.Auth.SessionTimeout);

            SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true, ExpiresUtc = expireDateTime }, claimsIdentity);

        }

        public void SignOut(string callbackUrl = null)
        {
            if (!string.IsNullOrEmpty(callbackUrl))
            {
                AuthenticationManager.SignOut(new AuthenticationProperties { RedirectUri = callbackUrl }, authenticationTypes);
            }
            else
            {
                AuthenticationManager.SignOut(authenticationTypes);
            }
        }

        public UserInfoViewModel LoginInfo
        {
            get
            {
                UserInfoViewModel userInfo = new UserInfoViewModel();

                IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;
                userInfo.Id = Guid.Parse(claims.Where(c => c.Type == ConstantHelper.Claims.UserId).Select(c => c.Value).FirstOrDefault());
                userInfo.FullName = claims.Where(c => c.Type == ConstantHelper.Claims.FullName).Select(c => c.Value).FirstOrDefault();
                userInfo.Roles = claims.Where(c => c.Type == ConstantHelper.Claims.Roles).Select(c => c.Value).FirstOrDefault();

                return userInfo;
            }
        }

        public string AccessToken
        {
            get
            {
                IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;
                string token = null;
                var cToken = claims.Where(c => c.Type == ConstantHelper.Claims.Token).FirstOrDefault();
                if (cToken != null)
                    token = cToken.Value;
                return token;
            }
        }


        public string RefreshToken
        {
            get { throw new NotImplementedException(); }
        }

        // In seconds
        public long ExpiresIn
        {
            get
            {
                IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;
                long expires = 0;
                var cExpires = claims.Where(c => c.Type == ConstantHelper.Claims.TokenExpiresIn).FirstOrDefault();
                if (cExpires != null)
                    expires = Convert.ToInt64(cExpires.Value);
                return expires;
            }
        }
    }
}