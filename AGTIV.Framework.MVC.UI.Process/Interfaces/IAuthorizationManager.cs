using AGTIV.Framework.MVC.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IAuthorizationManager
    {
        /// <summary>
        /// Gets the info of user.
        /// </summary>
        UserInfoViewModel LoginInfo { get; }

        /// <summary>
        /// Sign in the user from the application.
        /// </summary>
        /// <param name="claims">Claims to be stored into ticket</param>
        /// <param name="rememberMe">To remember the user or not</param>
        /// <returns>Sign in result.</returns>
        void SignIn(List<Claim> claims, bool rememberMe);

        /// <summary>
        /// Sign out the user from the application.
        /// </summary>
        /// <param name="callbackUrl">Redirect url after sign out</param>
        /// <returns>Sign out result.</returns>
        void SignOut(string callbackUrl = null);
    }
}
