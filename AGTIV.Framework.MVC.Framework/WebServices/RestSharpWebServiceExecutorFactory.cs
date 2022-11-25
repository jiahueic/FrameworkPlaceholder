using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    public class RestSharpWebServiceExecutorFactory : IWebServiceExecutorFactory
    {
        private readonly IRestSharpAuthenticatorFactory _authFactory;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="authFactory">RestSharp authenticator factory. Needed to construct authenticator for some implementation.</param>
        public RestSharpWebServiceExecutorFactory(IRestSharpAuthenticatorFactory authFactory)
        {
            _authFactory = authFactory;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// This factory is able to create a set of instances of implementation using RestSharp library.
        /// The available <paramref name="name"/> can be gotten in <see cref="RestSharpWebServiceExecutorType"/> class.
        /// </remarks>
        public IWebServiceExecutor CreateInstance(string name, params string[] param)
        {
            IWebServiceExecutor instance = null;

            switch (name)
            {
                case "basic":
                    instance = new RestSharpServiceExecutor();
                    break;
                case "form":
                    instance = new RestSharpFormServiceExecutor();
                    break;
                case "form-raw":
                    instance = new RestSharpRawServiceExecutor("application/x-www-form-urlencoded");
                    break;
                case "bearer":
                    if (param.Length < 1)
                    {
                        throw new ArgumentException("Bearer token must be passed as parameter 1.");
                    }
                    instance = new RestSharpServiceExecutorWithAuth(_authFactory.GetAuthenticator("bearer", param[0]));
                    break;
                case "bearerbson":
                    if (param.Length < 1)
                    {
                        throw new ArgumentException("Bearer token must be passed as parameter 1.");
                    }
                    instance = new RestSharpServiceExecutorWithAuthBson(_authFactory.GetAuthenticator("bearer", param[0]));
                    break;
                case "basicauth":
                    if (param.Length < 2)
                    {
                        throw new ArgumentException("Username and password must be passed as parameter 1 and 2.");
                    }
                    instance = new RestSharpServiceExecutorWithAuth(_authFactory.GetAuthenticator("basic", param[0], param[1]));
                    break;
                case "ntlmauth":
                    if (param.Length < 2)
                    {
                        throw new ArgumentException("Username and password must be passed as parameter 1 and 2.");
                    }
                    instance = new RestSharpServiceExecutorWithAuth(_authFactory.GetAuthenticator("ntlm", param[0], param[1]));
                    break;
                case "multipartmixed":
                    if (param.Length < 2)
                    {
                        throw new ArgumentException("Username and password must be passed as parameter 1 and 2.");
                    }
                    instance = new RestSharpMultipartMixedServiceExecutor(_authFactory.GetAuthenticator("basic", param[0], param[1]));
                    break;
            }

            return instance;
        }
    }

    /// <summary>
    /// Consists of a set of available implementations of <see cref="IWebServiceExecutor"/>.
    /// Use the Value property to get the actual string of the name for the factory.
    /// </summary>
    public class RestSharpWebServiceExecutorType
    {
        private string _value;

        /// <summary>
        /// Gets the name string of the implementation.
        /// </summary>
        public string Value { get { return _value; } }

        private RestSharpWebServiceExecutorType(string value)
        {
            _value = value;
        }

        /// <summary>
        /// Used to construct instance of <see cref="RestSharpServiceExecutor"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType Basic = new RestSharpWebServiceExecutorType("basic");
        /// <summary>
        /// Used to construct instance of <see cref="RestSharpFormServiceExecutor"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType Form = new RestSharpWebServiceExecutorType("form");
        /// <summary>
        /// Used to construct instance of <see cref="RestSharpServiceExecutorWithAuth"/> with <see cref="BearerAuthenticator"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType BearerToken = new RestSharpWebServiceExecutorType("bearer");
        /// <summary>
        /// Used to construct instance of <see cref="RestSharpServiceExecutorWithAuthBson"/> with <see cref="BearerAuthenticator"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType BearerTokenUsingBSON = new RestSharpWebServiceExecutorType("bearerbson");
        /// <summary>
        /// Used to construct instance of <see cref="RestSharpServiceExecutorWithAuth"/> with <see cref="HttpBasicAuthenticator"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType BasicAuth = new RestSharpWebServiceExecutorType("basicauth");
        /// <summary>
        /// Used to construct instance of <see cref="RestSharpMultipartMixedServiceExecutor"/> with <see cref="HttpBasicAuthenticator"/>.
        /// </summary>
        public static RestSharpWebServiceExecutorType MultipartMixed = new RestSharpWebServiceExecutorType("multipartmixed");
    }
}
