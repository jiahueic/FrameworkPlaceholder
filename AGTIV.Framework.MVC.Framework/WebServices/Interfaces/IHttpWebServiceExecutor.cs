using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.Interfaces
{
    public interface IHttpWebServiceExecutor : IWebServiceExecutor
    {
        /// <summary>
        /// Executes a HTTP web request to the URL specified and get the result and data returned.
        /// </summary>
        /// <typeparam name="T">The type of object for result to deserialize to.</typeparam>
        /// <param name="url">Web service URL</param>
        /// <param name="method">HTTP Method to use</param>
        /// <param name="objects">Additional parameter to included in request.</param>
        /// <returns>Deserialized result returned by the web service</returns>
        IWebServiceResponse<T> ExecuteRequest<T>(
            string url,
            HttpMethod method,
            Dictionary<string, string> objects,
            string contentType,
            HttpParameterType paramType)
            where T : new();
    }
}
