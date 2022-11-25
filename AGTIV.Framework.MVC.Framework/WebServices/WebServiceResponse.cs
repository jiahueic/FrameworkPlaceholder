using AGTIV.Framework.MVC.Framework.WebServices.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.WebServices
{
    /// <inheritdoc/>
    public class WebServiceResponse : IWebServiceResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        /// <inheritdoc/>
        public string StatusDescription { get; set; }

        /// <inheritdoc/>
        public string RawContent { get; set; }
    }

    /// <inheritdoc/>
    public class WebServiceResponse<T> : WebServiceResponse, IWebServiceResponse<T>
    {
        /// <inheritdoc/>
        public T Data { get; set; }
    }
}
