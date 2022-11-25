using AGTIV.Framework.MVC.Entities.Authentication;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Authentication
{
    public interface IAppUserStore : IUserStore<AppUser, Guid>
    {
    }
}
