using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    /// <summary>
    /// Executes a web request with request type RestSharpMultipartMixedServiceExecutor as request content. 
    /// </summary>
    public class RestSharpMultipartMixedServiceExecutor : RestSharpServiceExecutor
    {
        private readonly IAuthenticator _authenticator;

        public RestSharpMultipartMixedServiceExecutor(IAuthenticator _authenticator)
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

        protected override IRestRequest ConstructRequest(string path, HttpMethod method, params object[] objects)
        {
            IRestRequest request = new RestRequest(path, MapHttpMethodToRestSharpMethod(method));

            request.AddHeader("Content-Type", "multipart/mixed");
            request.Parameters.Clear();
            request.AddParameter("multipart/mixed", objects[0], ParameterType.RequestBody);

            return request;
        }
    }
}
