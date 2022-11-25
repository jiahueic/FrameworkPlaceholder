using AGTIV.Framework.MVC.Framework.Constants;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AGTIV.Framework.MVC.WebAPI.Providers
{
    public class CustomOwinMiddleware : OwinMiddleware
    {
        public CustomOwinMiddleware(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            await Next.Invoke(context);

            /* 
             * If current request is 400 Bad Request. Check if any authentication failed header (which will be added
             * during authentication process. If there is, set the status code to the header value (most probably will be 401)
             * and remove the custom header finally.
             * Reference: http://stackoverflow.com/questions/25032513/how-to-get-error-message-returned-by-dotnetopenauth-oauth2-on-client-side
            */
            if (context.Response.StatusCode == 400
                && context.Response.Headers.ContainsKey(
                          ConstantHelper.Auth.OwinChallengeFlag))
            {
                var headerValues = context.Response.Headers.GetValues
                      (ConstantHelper.Auth.OwinChallengeFlag);

                context.Response.StatusCode =
                       Convert.ToInt16(headerValues.FirstOrDefault());

                context.Response.Headers.Remove(
                       ConstantHelper.Auth.OwinChallengeFlag);
            }
        }
    }
}