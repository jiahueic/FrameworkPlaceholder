using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.Role
{
    public class RoleAssignmentPVM
    {
        public Guid UserId { get; set; }

        [Required]
        public string[] SelectedRole { get; set; }

        public IEnumerable<Role> RoleDLL { get; set; }

        public RoleAssignmentPVM()
        {
            RoleDLL = new List<Role>();
        }
    }
}
