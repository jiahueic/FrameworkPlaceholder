using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Authentication
{
    public class AppClaimsPrincipal : ClaimsPrincipal
    {
        public AppClaimsPrincipal(ClaimsPrincipal principal) : base(principal)
        {

        }

        public Guid UserId
        {
            get { return Guid.Parse(FindFirst(ClaimTypes.Sid).Value); }
        }
    }
}
