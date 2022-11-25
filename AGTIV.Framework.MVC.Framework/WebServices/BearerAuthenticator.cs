using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class BearerAuthenticator : IAuthenticator
    {
        private readonly string authHeader;

        public BearerAuthenticator(string token)
        {
            this.authHeader = string.Format("Bearer {0}", token);
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (!request.Parameters.Any(p => p.Name.Equals("Authorization", StringComparison.OrdinalIgnoreCase)))
            {
                request.AddParameter("Authorization", this.authHeader, ParameterType.HttpHeader);
            }
        }
    }
}
