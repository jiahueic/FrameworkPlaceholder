using AGTIV.Framework.MVC.Framework.Exceptions;
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
    public class ExceptionActionResultFactory
    {
        private readonly Type _exceptionType;
        private readonly ExceptionHandlerContext _context;

        public ExceptionActionResultFactory(
            Type exceptionType,
            ExceptionHandlerContext context)
        {
            _exceptionType = exceptionType;
            _context = context;
        }

        public IHttpActionResult CreateExceptionActionResult(string correlationId)
        {
            if (_exceptionType == typeof(ArgumentNullException))
            {
                return new ArgumentNullResult(_context, correlationId);
            }
            else if (_exceptionType == typeof(RecordNotFoundException))
            {
                return new RecordNotFoundResult(_context, correlationId);
            }
            else
            {
                return new InternalServerErrorResult(_context, correlationId);
            }
        }
    }
}