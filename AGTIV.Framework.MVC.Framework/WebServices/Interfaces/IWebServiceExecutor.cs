using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.Interfaces
{
    /// <summary>
    /// Executes a web request to an URL.
    /// </summary>
    public interface IWebServiceExecutor
    {
        /// <summary>
        /// Executes a HTTP web request to the URL specified and get the result and data returned.
        /// </summary>
        /// <typeparam name="T">The type of object for result to deserialize to.</typeparam>
        /// <param name="url">Web service URL</param>
        /// <param name="method">HTTP Method to use</param>
        /// <param name="objects">Additional parameter to included in request.</param>
        /// <returns>Deserialized result returned by the web service</returns>
        IWebServiceResponse<T> ExecuteRequest<T>(string url, HttpMethod method, params object[] objects) where T : new();

        /// <summary>
        /// Executes a HTTP web request to the URL specified and get the result and data returned.
        /// </summary>
        /// <param name="url">Web service URL</param>
        /// <param name="method">HTTP Method to use</param>
        /// <param name="objects">Additional parameter to included in request.</param>
        /// <returns>Result returned by the web service</returns>
        IWebServiceResponse ExecuteRequest(string url, HttpMethod method, params object[] objects);
    }

    /// <summary>
    /// Consists of common HTTP Methods available.
    /// </summary>
    public enum HttpMethod
    {
        GET,
        POST,
        PUT,
        DELETE,
        PATCH
    };

    /// <summary>
    /// Consists of common HTTP Methods available.
    /// </summary>
    public enum HttpParameterType
    {
        RequestBody,
        QueryString,
        HttpHeader,
        GetOrPost,
        UrlSegment,
        Cookie
    };
}
