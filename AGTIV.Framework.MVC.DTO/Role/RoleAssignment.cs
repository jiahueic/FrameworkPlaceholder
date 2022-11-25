using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.DTO.Role
{
    public class RoleAssignment
    {
        public Guid UserId { get; set; }

        public string[] SelectedRole { get; set; }
    }
}
