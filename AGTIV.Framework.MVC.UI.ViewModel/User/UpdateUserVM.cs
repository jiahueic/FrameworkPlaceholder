using System.Collections.Generic;

namespace AGTIV.Framework.MVC.UI.ViewModel.User
{
    public class UpdateUserVM
    {
        public User User { get; set; }

        public IEnumerable<ViewModel.Role.Role> RoleDDL { get; set; }

        public IEnumerable<ViewModel.Calendar.CalendarProfileDdl> CalendarDdl { get; set; }

        public UpdateUserVM()
        {
            RoleDDL = new List<ViewModel.Role.Role>();
            CalendarDdl = new List<ViewModel.Calendar.CalendarProfileDdl>();
        }
    }
}
