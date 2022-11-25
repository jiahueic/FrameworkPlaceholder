using AGTIV.Framework.MVC.UI.ViewModel.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.Process.Interfaces
{
    public interface IRoleProcess
    {
        IEnumerable<Role> Get();

        Guid AssignRole(RoleAssignmentPVM pVM);
    }
}
