using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.Interfaces
{
    /// <summary>
    /// A factory to construct authenticators for RestSharp.
    /// </summary>
    public interface IRestSharpAuthenticatorFactory
    {
        /// <summary>
        /// Gets an authenticator that can be attached to RestSharp client.
        /// </summary>
        /// <param name="type">Authenticator type.</param>
        /// <param name="param">Additional parameters, required depending on the <paramref name="type"/>.</param>
        /// <returns>Implementation of RestSharp IAuthenticator based on the <paramref name="type"/> passed.</returns>
        IAuthenticator GetAuthenticator(string type, params string[] param);
    }
}
