using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpAuthenticatorFactory : IRestSharpAuthenticatorFactory
    {
        public IAuthenticator GetAuthenticator(string type, params string[] param)
        {
            type = type.ToLower();

            if (type == "basic")
            {
                return new HttpBasicAuthenticator(param[0], param[1]);
            }
            else if (type == "bearer")
            {
                return new BearerAuthenticator(param[0]);
            }
            else if (type == "ntlm")
            {
                return new NtlmAuthenticator(param[0], param[1]);
            }

            return null;
        }
    }
}
