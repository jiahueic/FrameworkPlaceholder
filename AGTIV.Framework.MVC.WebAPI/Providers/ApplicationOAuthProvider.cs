using AGTIV.Framework.MVC.Business.Authentication;
using AGTIV.Framework.MVC.Business.User;
using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Framework.Constants;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AGTIV.Framework.MVC.Framework.Authentication;

namespace AGTIV.Framework.MVC.WebAPI.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IAuthenticationComponent _authenticationComponent;
        private readonly IUserComponent _userComponent;

        public ApplicationOAuthProvider(
            IAuthenticationComponent authenticationComponent,
            IUserComponent userComponent)
        {
            _authenticationComponent = authenticationComponent;
            _userComponent = userComponent;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var userManager = context.OwinContext.GetUserManager<AppUserManager>();

            AppUser user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                // Add 401 flag to the header of the response
                context.Response.Headers.Add(ConstantHelper.Auth.OwinChallengeFlag,
                         new[] { ((int)HttpStatusCode.Unauthorized).ToString() });
                return;
            }

            ClaimsIdentity oAuthIdentity = await GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType, user);
            ClaimsIdentity cookiesIdentity = await GenerateUserIdentityAsync(userManager,
                CookieAuthenticationDefaults.AuthenticationType, user);

            AuthenticationProperties properties = CreateProperties(user.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesIdentity);
        }

        private async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager manager, string authenticationType, AppUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(user, authenticationType);

            var userProfile = _userComponent.GetByAppUserId(user.Id);

            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.FullName, userProfile.FullName));
            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.UserId, userProfile.Id.ToString()));
            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.Username, user.Email));

            // Add custom user claims here
            //userIdentity.AddClaim(new Claim("FullName", user. ))
            return userIdentity;
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            if (context.Identity.FindFirst(ClaimTypes.NameIdentifier) != null)
                context.AdditionalResponseParameters.Add(ClaimTypes.NameIdentifier, context.Identity.FindFirst(ClaimTypes.NameIdentifier).Value);
            if (context.Identity.FindFirst(ConstantHelper.Claims.UserId) != null)
                context.AdditionalResponseParameters.Add(ConstantHelper.Claims.UserId, context.Identity.FindFirst(ConstantHelper.Claims.UserId).Value);
            if (context.Identity.FindFirst(ConstantHelper.Claims.FullName) != null)
                context.AdditionalResponseParameters.Add(ConstantHelper.Claims.FullName, context.Identity.FindFirst(ConstantHelper.Claims.FullName).Value);
            if (context.Identity.FindFirst(ClaimTypes.Role) != null)
                context.AdditionalResponseParameters.Add(ConstantHelper.Claims.Roles, context.Identity.FindFirst(ClaimTypes.Role).Value);

            return Task.FromResult<object>(null);
        }

        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;

            if (context.TryGetFormCredentials(out clientId, out clientSecret))
            {
                // validate the client Id and secret against database or from configuration file.  
                var isAuthenticated = _authenticationComponent.IsAuthenticated(clientId, clientSecret);

                if (isAuthenticated)
                    context.Validated();
                else
                    context.Rejected();
            }
            else
            {
                context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
                context.Rejected();
            }
        }

        //public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    if (context.ClientId == _publicClientId)
        //    {
        //        Uri expectedRootUri = new Uri(context.Request.Uri, "/");

        //        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        //        {
        //            context.Validated();
        //        }
        //    }

        //    return Task.FromResult<object>(null);
        //}

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        private ClaimsIdentity GenerateUserIdentity(AppUserManager manager, string authenticationType, AppUser user)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = manager.CreateIdentity(user, authenticationType);

            var userProfile = _userComponent.Get(user.Id);

            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.FullName, userProfile.FullName));
            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.UserId, userProfile.Id.ToString()));
            userIdentity.AddClaim(new Claim(ConstantHelper.Claims.Username, user.Email));
            foreach (var role in userProfile.Roles)
            {
                userIdentity.AddClaim(new Claim(ConstantHelper.Claims.Roles, role));
            }

            return userIdentity;
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var userManager = context.OwinContext.GetUserManager<AppUserManager>();
            AppUser user = userManager.FindByEmail(context.Ticket.Identity.Name);

            ClaimsIdentity newIdentity = GenerateUserIdentity(userManager,
               OAuthDefaults.AuthenticationType, user);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}