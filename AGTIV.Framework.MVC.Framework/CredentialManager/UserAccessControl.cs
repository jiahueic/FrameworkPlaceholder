using AGTIV.Framework.MVC.Framework.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.CredentialManager
{
    public static class UserAccessControl
    {
        public static Guid GetCurrentUserId()
        {
            IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;

            string userId = claims.Where(c => c.Type == ConstantHelper.Claims.UserId)
                .Select(x => x.Value).FirstOrDefault();

            if (!Guid.TryParse(userId, out Guid userGuid))
                throw new Exception("Unable to get current user Id");

            return userGuid;
        }

        public static string GetCurrentUserFullName()
        {
            IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;

            string username = claims.Where(c => c.Type == ConstantHelper.Claims.FullName).Select(c => c.Value).FirstOrDefault();

            return username;
        }

        public static string GetCurrentUsername()
        {
            IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;

            string username = claims.Where(c => c.Type == ConstantHelper.Claims.Username).Select(c => c.Value).FirstOrDefault();

            return username;
        }

        public static bool MatchAnyRoles(params string[] roles)
        {
            IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;

            var isMatch = claims.Any(
                x => x.Type == ConstantHelper.Claims.Roles &&
                roles.Any(role => role == x.Value));

            return isMatch;
        }

        public static bool HasAnyRoles()
        {
            IEnumerable<Claim> claims = ClaimsPrincipal.Current.Claims;

            var isMatch = claims.Any(
                x => x.Type == ConstantHelper.Claims.Roles &&
                !String.IsNullOrWhiteSpace(x.Value));

            return isMatch;
        }
    }
}
