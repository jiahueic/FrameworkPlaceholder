using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    /// <summary>
    /// Executes a web request to an URL using RestSharp library.
    /// </summary>
    public class RestSharpServiceExecutor : IWebServiceExecutor
    {
        public virtual IWebServiceResponse<T> ExecuteRequest<T>(string url, HttpMethod method, params object[] objects) where T : new()
        {
            IRestClient client = ConstructClient(NetHelper.GetBaseUrl(url));
            IRestRequest request = ConstructRequest(NetHelper.GetUrlPath(url), method, objects);
            IRestResponse<T> response = client.Execute<T>(request);
            ThrowExceptionIfError(response);

            return new WebServiceResponse<T>()
            {
                RawContent = response.Content,
                StatusCode = response.StatusCode,
                Data = response.Data,
                StatusDescription = response.StatusDescription
            };
        }

        public virtual IWebServiceResponse ExecuteRequest(string url, HttpMethod method, params object[] objects)
        {
            IRestClient client = ConstructClient(NetHelper.GetBaseUrl(url));
            IRestRequest request = ConstructRequest(NetHelper.GetUrlPath(url), method, objects);
            IRestResponse response = client.Execute(request);
            ThrowExceptionIfError(response);

            return new WebServiceResponse()
            {
                RawContent = response.Content,
                StatusCode = response.StatusCode,
                StatusDescription = response.StatusDescription
            };
        }

        /// <summary>
        /// Throws a WebServiceException if the request is not successful. The current implementation is coupled
        /// with Web API. So it may NOT work in other APIs.
        /// </summary>
        /// <param name="response">Response from RestSharp</param>
        /// <exception cref="WebServiceException">Thrown if request is not successful.</exception>
        protected virtual void ThrowExceptionIfError(IRestResponse response)
        {
            if (response.ErrorException != null && String.IsNullOrEmpty(response.StatusDescription))
            {
                throw new WebServiceException("Failed to connect to web service.", response.ErrorException);
            }
            else if (response.StatusCode != HttpStatusCode.OK)
            {
                // MHW API Integration failed.
                if (response.StatusCode == HttpStatusCode.NotAcceptable)
                {
                    Guid id = Guid.Empty;

                    if (Guid.TryParse(response.Content.ToString(), out id))
                    {
                        throw new WebServiceProcessException(id);
                    }
                    else
                    {
                        throw new WebServiceProcessException("Error raised in web service.");
                    }
                }

                // Business Exceptions
                if (response.StatusCode == HttpStatusCode.Conflict)
                {
                    // Parse the content manually to get the error details (if any).
                    dynamic conflictContent = null;
                    try
                    {
                        conflictContent = JObject.Parse(response.Content);
                    }
                    catch (Exception)
                    {
                        // Do nothing.
                    }

                    if (conflictContent != null && conflictContent.message != null)
                    {
                        string msg = conflictContent.message;
                        throw new WebServiceException(response.StatusCode, msg);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.LengthRequired)
                {
                    if (response.Content.ToString() == ExceptionType.Argument.ToString())
                    {
                        throw new WebServiceProcessException(response.StatusDescription) { ExceptionType = ExceptionType.Argument };
                    }
                    else
                    {
                        throw new WebServiceProcessException("Error raised in web service.");
                    }
                }

                // Parse the content manually to get the error details (if any).
                dynamic content = null;
                try
                {
                    content = JObject.Parse(response.Content);
                }
                catch (Exception)
                {
                    // Do nothing.
                }

                if (content != null && content.messageDetail != null)
                {
                    string msg = content.messageDetail;
                    throw new WebServiceException(response.StatusCode, msg);
                }
            }
        }

        /// <summary>
        /// Constructs a RestSharp client.
        /// </summary>
        /// <param name="baseUrl">Base URL of web service to connect. (Example: http://api.google.com)</param>
        /// <returns>A RestSharp client.</returns>
        protected virtual IRestClient ConstructClient(string baseUrl)
        {
            var client = new RestClient(baseUrl);
            client.RemoveHandler("application/json"); // Remove default deserializer
            client.RemoveHandler("text/json");
            client.RemoveHandler("text/x-json");
            client.RemoveHandler("*+json");

            var handler = new JsonDotNetDeserializer();
            client.AddHandler("application/json", handler); // Use custom deserializer.
            client.AddHandler("text/json", handler);
            client.AddHandler("text/x-json", handler);
            client.AddHandler("*+json", handler);

            return client;
        }

        /// <summary>
        /// Constructs a RestSharp request.
        /// </summary>
        /// <param name="baseUrl">Path of web service to request. (Example: api/Contact)</param>
        /// <returns>A RestSharp client.</returns>
        protected virtual IRestRequest ConstructRequest(string path, HttpMethod method, params object[] objects)
        {
            IRestRequest request = new RestRequest(path, MapHttpMethodToRestSharpMethod(method));
            request.RequestFormat = DataFormat.Json;
            request.Timeout = 120000;

            foreach (var obj in objects)
            {
                request.AddBody(obj);
            }
            return request;
        }

        /// <summary>
        /// Maps HttpMethod class used in interface to Method class used in RestSharp.
        /// </summary>
        /// <param name="method">HttpMethod defined in interface.</param>
        /// <returns>Mapped Method for RestSharp use.</returns>
        protected Method MapHttpMethodToRestSharpMethod(HttpMethod method)
        {
            Method rsMethod = Method.GET;
            switch (method)
            {
                case HttpMethod.GET:
                    rsMethod = RestSharp.Method.GET;
                    break;
                case HttpMethod.POST:
                    rsMethod = RestSharp.Method.POST;
                    break;
                case HttpMethod.PUT:
                    rsMethod = RestSharp.Method.PUT;
                    break;
                case HttpMethod.DELETE:
                    rsMethod = RestSharp.Method.DELETE;
                    break;
                case HttpMethod.PATCH:
                    rsMethod = RestSharp.Method.PATCH;
                    break;
            }

            return rsMethod;
        }

        /// <summary>
        /// Maps HttpParameterType class used in interface to Method class used in RestSharp.
        /// </summary>
        /// <param name="paramType"></param>
        /// <returns></returns>
        protected ParameterType MapParamTypeToRestSharpType(HttpParameterType paramType)
        {
            ParameterType type = ParameterType.QueryString;

            switch (paramType)
            {
                case HttpParameterType.Cookie:
                    type = ParameterType.Cookie;
                    break;
                case HttpParameterType.GetOrPost:
                    type = ParameterType.GetOrPost;
                    break;
                case HttpParameterType.HttpHeader:
                    type = ParameterType.HttpHeader;
                    break;
                case HttpParameterType.QueryString:
                    type = ParameterType.QueryString;
                    break;
                case HttpParameterType.RequestBody:
                    type = ParameterType.RequestBody;
                    break;
                case HttpParameterType.UrlSegment:
                    type = ParameterType.UrlSegment;
                    break;

            }

            return type;
        }

        /// <summary>
        /// Using JSON.NET to deserialize JSON.
        /// Referenced from http://stackoverflow.com/questions/18357134/how-can-i-make-restsharp-use-bson
        /// </summary>
        public class JsonDotNetDeserializer : IDeserializer
        {
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string DateFormat { get; set; }

            public T Deserialize<T>(IRestResponse response)
            {
                if (response.ErrorException == null && response.StatusCode == HttpStatusCode.OK)
                    return JsonConvert.DeserializeObject<T>(response.Content);
                else
                    return default(T);
            }
        }
    }
}
