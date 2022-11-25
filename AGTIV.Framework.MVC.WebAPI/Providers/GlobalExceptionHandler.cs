using AGTIV.Framework.MVC.Framework.Helper;
using AGTIV.Framework.MVC.WebAPI.Providers.ExceptionActionResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;

namespace AGTIV.Framework.MVC.WebAPI.Providers
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            var factory = new ExceptionActionResultFactory(context.Exception.GetType(), context);

            var correlationId = LogHelper.LogMessage(context.Exception);

            context.Result = factory.CreateExceptionActionResult(correlationId);
        }
    }
}