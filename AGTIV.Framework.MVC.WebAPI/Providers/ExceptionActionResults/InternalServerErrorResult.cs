using AGTIV.Framework.MVC.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace AGTIV.Framework.MVC.WebAPI.Providers.ExceptionActionResults
{
    public class InternalServerErrorResult : IHttpActionResult
    {
        private ExceptionHandlerContext _context;
        private string _correlationId;

        public InternalServerErrorResult(ExceptionHandlerContext context, string correlationId)
        {
            _context = context;
            _correlationId = correlationId;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var result = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                Content = new StringContent($"API { ConstantHelper.Error.Elmah.CorrelationId } : {_correlationId } - Some unexpected error occured."),
                ReasonPhrase = "InternalServerError"
            };

            return Task.FromResult(result);
        }
    }
}