using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.Web.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult Index()
        {
            return GetActionResult(HttpStatusCode.InternalServerError, "Internal Server Error");
        }

        public ActionResult NotFound()
        {
            return GetActionResult(HttpStatusCode.NotFound, "Not Found");
        }

        public ActionResult SessionExpired()
        {
            return GetActionResult(HttpStatusCode.Gone, "Session Expired");
        }

        public ActionResult Forbidden()
        {
            return GetActionResult(HttpStatusCode.Forbidden, "Forbidden");
        }

        public ActionResult Unauthorized()
        {
            return GetActionResult(HttpStatusCode.Forbidden, "Unauthorized");
        }

        public ActionResult TaskNotAllowed()
        {
            return View();
        }

        private ActionResult GetActionResult(HttpStatusCode httpStatusCode, string errorMessage)
        {
            Response.StatusCode = (int)httpStatusCode;

            //Override the default behaviour of how Azure web app handle error status code
            //As we want the app to redirect to our own error page.
            Response.TrySkipIisCustomErrors = true;

            if (ControllerContext.IsChildAction)
                return PartialView();
            else if (Request.IsAjaxRequest())
                return Json(new
                {
                    errorMessage = errorMessage
                });

            return View();
        }
    }
}