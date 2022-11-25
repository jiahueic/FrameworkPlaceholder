using AGTIV.Framework.MVC.Framework.Constants;
using AGTIV.Framework.MVC.Framework.CredentialManager;
using AGTIV.Framework.MVC.Framework.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace AGTIV.Framework.MVC.UI.Web.Attribute
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = true)]
    public class RoleAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var claims = ((ClaimsIdentity)Thread.CurrentPrincipal.Identity);
            string[] authorisedRoles = Roles.Split(',');

            if(UserAccessControl.MatchAnyRoles(authorisedRoles))
                return true;
            else
                return false;
        }

        protected override void HandleUnauthorizedRequest(System.Web.Mvc.AuthorizationContext filterContext)
        {

            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {

                filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        controller = "Error",
                        action = "Unauthorized"
                    })
                );
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}