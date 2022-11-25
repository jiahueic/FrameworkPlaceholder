using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AGTIV.Framework.MVC.Framework.Constants
{
    public partial class ConstantHelper
    {
        public class AppSetting
        {

            public const string ClientId = "ClientId";
            public const string ClientSecret = "ClientSecret";
            public const string WebApiUrl = "WebApiUrl";
            public const string WebUrl = "WebUrl";

            public const string AG_EmailDefaultCredential = "AG_EMAILDEFAULTCREDENTIAL";
            public const string AG_EmailFrom = "AG_EMAILFROM";
            public const string AG_EmailHost = "AG_EMAILHOST";
            public const string AG_EmaillPassword = "AG_EMAILPASSWORD";
            public const string AG_EmailPort = "AG_EMAILPORT";
            public const string AG_EmailUseDefaultCredential = "AG_EMAILUSEDEFAULTCREDENTIAL";
            public const string AG_EmailUsername = "AG_EMAILUSERNAME";
            public const string AG_EmailUseSSL = "AG_EMAILUSESSL";
            public const string AG_ENCKEY = "AG_ENCKEY";
        }

        public class ConnString
        {
            public const string Default = "DefaultConnection";
            public const string Elmah = "ELMAH";
        }
    }
}
