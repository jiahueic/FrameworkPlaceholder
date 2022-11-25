using AGTIV.Framework.MVC.DTO.Role;
using AGTIV.Framework.MVC.Entities.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Business.Role
{
    public interface IRoleComponent
    {
        IEnumerable<AppRole> Get();
    }
}
