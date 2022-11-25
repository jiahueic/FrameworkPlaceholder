using AGTIV.Framework.MVC.UI.ViewModel.General;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel.User
{
    public class User : BaseViewModel
    {
        public string Username { get; set; }

        [DisplayName("Full Name")]
        public string FullName { get; set; }

        [DisplayName("Mobile No")]
        public string MobileNo { get; set; }

        [DisplayName("Email Address")]
        public string EmailAddress { get; set; }

        [DisplayName("New NRIC")]
        public string NewNRIC { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("PostCode")]
        public string PostCode { get; set; }

        [DisplayName("State")]
        public string State { get; set; }

        [DisplayName("Country")]
        public string Country { get; set; }

        [DisplayName("Department")]
        public string Department { get; set; }

        [DisplayName("Manager")]
        public string Manager { get; set; }

        public Guid AppUser_Id { get; set; }

        [DisplayName("Role")]
        public string[] Roles { get; set; }

        public string RolesString { get; set; }

        public string Password { get; set; }

        [DisplayName("Calendar Profile")]
        public Guid? CalendarProfile_Id { get; set; }

        public Image Image { get; set; }
    }
}
