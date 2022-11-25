using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.Exceptions;
using AGTIV.Framework.MVC.Framework.Helper;
using Elmah;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Filter
{
    public class GlobalHandleErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled || !filterContext.HttpContext.IsCustomErrorEnabled)
                return;

            // Handle error based on HTTP status code thrown
            // http://stackoverflow.com/questions/8144695/asp-net-mvc-custom-handleerror-filter-specify-view-based-on-exception-type
            // TODO Necessary loggings
            var statusCode = (int)HttpStatusCode.InternalServerError;

            if (filterContext.Exception is ProcessException)
            {
                var exp = (ProcessException)filterContext.Exception;

                if (exp.StatusCode.HasValue)
                    statusCode = (int)exp.StatusCode;
                else if (filterContext.Exception.InnerException != null)
                    filterContext.Exception = exp.InnerException;

                if (statusCode == (int)HttpStatusCode.Unauthorized)
                {
                    //to prevent login prompt in IIS
                    // which will appear when returning 401.
                    statusCode = (int)HttpStatusCode.Forbidden;
                }
            }
            else if (filterContext.Exception is HttpException)
            {
                statusCode = ((HttpException)filterContext.Exception).GetHttpCode();

                if (statusCode == (int)HttpStatusCode.Unauthorized)
                {
                    statusCode = (int)HttpStatusCode.Forbidden;
                }
            }

            //Log any unhandled errors to ELMAH
            //ErrorSignal.FromCurrentContext().Raise(filterContext.Exception);
            var correlationId = LogHelper.LogMessage(filterContext.Exception);

            var result = CreateActionResult(filterContext, statusCode, correlationId);
            filterContext.Result = result;

            // Prepare the response code.
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = statusCode;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }

        protected virtual ActionResult CreateActionResult(ExceptionContext filterContext, int statusCode, string correlationId)
        {
            var ctx = new ControllerContext(filterContext.RequestContext, filterContext.Controller);
            var statusCodeName = ((HttpStatusCode)statusCode).ToString();

            var viewName = SelectFirstView(ctx,
                                           String.Format("~/Views/Error/{0}.cshtml", statusCodeName),
                                           "~/Views/Error/Error.cshtml",
                                           statusCodeName,
                                           "Error");

            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];
            var errorMsg = filterContext.Exception.Message.Contains(ConstantHelper.Error.Elmah.CorrelationId) ? $"Web Correlation Id : { correlationId } <br/> {filterContext.Exception.Message}" : $"Web Correlation Id : { correlationId } - {filterContext.Exception.Message}";
            var model = new HandleErrorInfo(new Exception(errorMsg), controllerName, actionName);
            ViewResultBase result = null;

            if (!filterContext.IsChildAction)
            {
                result = new ViewResult
                {
                    ViewName = viewName,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                };
            }
            else
            {
                result = new PartialViewResult
                {
                    ViewName = viewName,
                    ViewData = new ViewDataDictionary<HandleErrorInfo>(model)
                };
            }

            result.ViewBag.StatusCode = statusCode;
            return result;
        }

        protected string SelectFirstView(ControllerContext ctx, params string[] viewNames)
        {
            return viewNames.First(view => ViewExists(ctx, view));
        }

        protected bool ViewExists(ControllerContext ctx, string name)
        {
            var result = ViewEngines.Engines.FindView(ctx, name, null);
            return result.View != null;
        }

    }
}