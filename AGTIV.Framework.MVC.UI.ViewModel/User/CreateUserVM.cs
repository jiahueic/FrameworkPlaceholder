using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AGTIV.Framework.MVC.UI.ViewModel.User
{
    public class CreateUserVM
    {
        public User User { get; set; }

        public IEnumerable<ViewModel.Role.Role> RoleDDL { get; set; }

        public CreateUserVM()
        {
            RoleDDL = new List<ViewModel.Role.Role>();
        }
    }
}
