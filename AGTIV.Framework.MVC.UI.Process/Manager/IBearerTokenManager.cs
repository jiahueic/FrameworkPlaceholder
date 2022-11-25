using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process.Manager
{
    /// <summary>
    /// Stores and provide the OAuth Bearer Token information.
    /// </summary>
    public interface IBearerTokenManager
    {
        /// <summary>
        /// Gets the Access token of the user.
        /// </summary>
        string AccessToken { get; }

        /// <summary>
        /// Gets the Refresh token of the user.
        /// </summary>
        string RefreshToken { get; }

        /// <summary>
        /// Gets the seconds the authentication will expire.
        /// </summary>
        long ExpiresIn { get; }
    }
}
