using AGTIV.Framework.MVC.Entities.Authentication;
using AGTIV.Framework.MVC.Framework.Constants;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;

namespace AGTIV.Framework.MVC.Framework.Authentication
{
    public class AppUserStore : UserStore<AppUser, AppRole, Guid, AppUserLogin, AppUserRole, AppUserClaim>, IAppUserStore
    {
        public AppUserStore() : base(new DbContext(ConstantHelper.ConnString.Default))
        {

        }

        public AppUserStore(DbContext context) : base(context)
        {
        }
    }
}
