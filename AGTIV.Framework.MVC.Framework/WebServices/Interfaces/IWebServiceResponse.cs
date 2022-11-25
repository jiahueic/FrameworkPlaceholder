using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices.Interfaces
{
    /// <summary>
    /// A container to store the web service response information.
    /// </summary>
    public interface IWebServiceResponse
    {
        /// <summary>
        /// Gets or sets the HTTP status code of the response returned by API.
        /// </summary>
        HttpStatusCode StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code description of the response returned by API.
        /// </summary>
        string StatusDescription { get; set; }

        /// <summary>
        /// Gets or sets the deserialized response content.
        /// </summary>
        string RawContent { get; set; }
    }

    /// <summary>
    /// A container to capture the web service response information, with deserialized response content.
    /// </summary>
    /// <typeparam name="T">Type to deserialize the response content into.</typeparam>
    public interface IWebServiceResponse<T> : IWebServiceResponse
    {
        /// <summary>
        /// Gets or sets the deserialized response content.
        /// </summary>
        T Data { get; set; }
    }
}
