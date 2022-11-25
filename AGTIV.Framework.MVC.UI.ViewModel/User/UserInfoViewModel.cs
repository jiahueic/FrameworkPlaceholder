using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.UI.ViewModel
{
    public class UserInfoViewModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Roles { get; set; }
    }
}
