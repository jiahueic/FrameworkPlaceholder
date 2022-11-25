using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpServiceExecutorWithAuth : RestSharpServiceExecutor
    {
        private readonly IAuthenticator _authenticator;

        public RestSharpServiceExecutorWithAuth(IAuthenticator _authenticator)
        {
            this._authenticator = _authenticator;
        }

        protected override IRestClient ConstructClient(string baseUrl)
        {
            IRestClient client = base.ConstructClient(baseUrl);

            if (_authenticator != null)
                client.Authenticator = _authenticator;

            return client;
        }
    }
}
